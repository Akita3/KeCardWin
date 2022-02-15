using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KeCardWin
{

    // クラス：KeCtrl ((E)CARD制御用のクラス)
    public class KeCtrl
    {
        // メンバ
        static public KeBle keBle = new KeBle();

        public static double txProgress = 0.0;      // 進捗状況

        public const int DISCONNECTED_WAIT = 10;          // 切断後のWait[sec]
        public const int CONNECTED_WAIT = 1000;             // 接続後のWait[ms]
        public const int SLOW_CMD_WAIT = 15;             // 接続後のWait[ms]
        public const int TIME_CMD_WAIT = 15;               // TimeCmd後のWait
        public const int CLEAR_CMD_WAIT = 15;              // ClearCmd後のWait


        // 進捗用の定数
        const double TX_PROGRESS_START = 0.1;
        const double TX_PROGRESS_CONNECTED = 0.2;
        const double TX_PROGRESS_FLASHED = 0.3;
        const double TX_PROGRESS_TX_START = TX_PROGRESS_FLASHED;
        const double TX_PROGRESS_FINISHED = 1.0;

        const double EVENT_PROGRESS_START = 0.1;
        const double EVENT_PROGRESS_CONNECTED = 0.2;
        const double EVENT_PROGRESS_FINISHED = 1.0;


        // 動作モード
        public enum WORK_MODE
        {
            IDLE=0,             // アイドル状態
            BLE_SCAN,           // スキャン
            BLE_CONN_FAST,      // 接続(高速)
            BLE_CONN_SLOW,      // 接続(低速)
                                // ↑安定状態
            IDLE_CHANGING,      // アイドル状態へ遷移中
            BLE_SCAN_CHANGING,  // スキャンへ遷移中
            BLE_CONN_FAST_CHANGING,  // 接続(高速)へ遷移中
            BLE_CONN_SLOW_CHANGING,  // 接続(低速)へ遷移中
            IMAGE_TX,           // イメージ転送中
            EVENT_TX,           // イベント転送中
            SWITCH_IMAGE,       // イメージ切り替え
            READ_KECONFIG,      // keconfig読み込み
            WRITE_KECONFIG,     // keconfig書き込み
        };

        static public WORK_MODE workMode = WORK_MODE.BLE_SCAN;          // 動作モード

        static public string[] WORK_MODE_STR =
        {
            "アイドル",
            "スキャン",
            "接続(高速)",
            "接続(低速)",
            "アイドル[遷移中]",
            "スキャン[遷移中]",
            "接続(高速)[遷移中]",
            "接続(低速)[遷移中]",
            "イメージ転送",
            "イベント転送",
            "イメージ切替",
        };


        static private async Task<bool> TransferImage(Bitmap image , double progressRange , int waitAfterTransfer = 0)
        {
            KeImage keImage = new KeImage();


            double finishProgress = txProgress + progressRange;

            bool r = keImage.SetImage(image);
            if (r)
            {
                int sendNoMax = keImage.GetSendNoMax();
                double step = progressRange / sendNoMax;

                for (int i = 0; i < sendNoMax; i++)
                {

                    byte[] data = keImage.GetSendPacket();
                    await keBle.SendImageData(data);
                    if (waitAfterTransfer != 0) await Task.Delay(waitAfterTransfer);
                    txProgress += step;
                }
            }

            txProgress = finishProgress;

            return r;
        }



        // データ送信シーケンス
        static public async Task<bool> ImageTransmissionSequence(ulong addr , Bitmap image , byte no , int waitAfterTransfer = 0)
        {
            bool res = true;

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.IMAGE_TX;

            txProgress = TX_PROGRESS_START;

            // BLE停止制御（失敗したら何もしない）
            res = await StopBle(beforeMode);


            Debug.WriteLine("device:" + addr.ToString("X06"));

            if( res ) res = await keBle.ConnectKeCard(addr);
            txProgress = TX_PROGRESS_CONNECTED;


            if (res) res = await keBle.SelectImage(no);

            if (res) res = await keBle.SendCommand(Cmd.CMD_ERASE_FLASH);

            if (res) res = await keBle.WaitStatus(KeBle.KE_STATUS_FLASH_ERASE_SUCCESS, 20);

            txProgress = TX_PROGRESS_FLASHED;

            if (res) res = await TransferImage(image, TX_PROGRESS_FINISHED - TX_PROGRESS_TX_START, waitAfterTransfer);

            if (res) res = await keBle.SendCommand(Cmd.CMD_DISPLAY);

            keBle.DisconnectKeCard();
            await keBle.WaitDisconnect(DISCONNECTED_WAIT);

            txProgress = TX_PROGRESS_FINISHED;

            // モード設定
            workMode = WORK_MODE.IDLE;

            return res;
        }


        // データ送信シーケンス
        static public async Task<bool> ApEventsTransmissionSequence(ulong addr, ApEvents apEvents, int waitAfterTransfer = 20)
        {
            if (apEvents.apEvents == null) return false;

            bool res = true;

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.EVENT_TX;

            txProgress = EVENT_PROGRESS_START;

            // BLE停止制御（失敗したら何もしない）
            if (res) res = await StopBle(beforeMode);


            // BLE接続
            if(res) res = await keBle.ConnectKeCard(addr);

            if (res) await Task.Delay(CONNECTED_WAIT);

            txProgress = EVENT_PROGRESS_CONNECTED;

            // Ver確認
            if (res) res = keBle.ver2Flag == true ? true : false;

            // 時間コマンド送信
            if (res) res = await keBle.SendTimeCmd();
            if (res) await Task.Delay(TIME_CMD_WAIT);
            if (res) res = await keBle.WaitStatus(KeSts.KESTS_TIME | KeSts.KESTS_RESULT_SUCCESS, 10);

            // クリアコマンド送信
            if (res) res = await keBle.SendClearCmd(KeKey.CLEAR_TYPE_RUNBOOK);
            if (res) await Task.Delay(CLEAR_CMD_WAIT);


            int procNo = Runbook.PROCEDURE_NO_START;
            double per = (EVENT_PROGRESS_FINISHED - EVENT_PROGRESS_CONNECTED) / apEvents.apEvents.Count();


            // 条件、アクション
            foreach (var apEvent in apEvents.apEvents)
            {
                if (!res) break;

                // Runbookのパケット取得
                var procs = apEvent.getRunbookProcedure(procNo);

                // 手順パケット送信
                foreach (var proc in procs)
                {
                    if (res) res = await keBle.SendProcedureCmd(proc);
                }

                // 番号更新
                procNo += procs.Count();

            }


            // イメージ
            foreach ( var apEvent in apEvents.apEvents )
            {
                if (!res) break;

                // 画像No
                int no = apEvent.imageNo;

                if( 0 <= no && no <= KeBle.KE_IMAGE_NO_MAX)
                {
                    // 画像No選択
                    if (res) res = await keBle.SelectImage((byte)no);

                    // 消去
                    if (res) res = await keBle.SendCommand(Cmd.CMD_ERASE_FLASH);

                    // 消去完了待ち
                    if (res) res = await keBle.WaitStatus(KeBle.KE_STATUS_FLASH_ERASE_SUCCESS, 20);

                    // イメージ
                    Bitmap image = apEvent.GetGray2bitImage();

                    if(image != null )
                    {
                        // イメージ転送
                        if (res) await TransferImage(image, per, waitAfterTransfer);
                    }

                }

            }

            // 初期イメージあり？
            ApEventInit apEventInit = (ApEventInit)apEvents.apEvents.FirstOrDefault(x => x.eventType == ApEvent.EVENT_TYPE.INIT);
            if(apEventInit != null )
            {
                if( 0 <= apEventInit.imageNo && apEventInit.imageNo <= KeBle.KE_IMAGE_NO_MAX)
                {
                    if (res) res = await keBle.SelectImage((byte)apEventInit.imageNo);

                    if (res) res = await keBle.SendCommand(Cmd.CMD_DISPLAY);
                }
            }

            keBle.DisconnectKeCard();
            await keBle.WaitDisconnect(DISCONNECTED_WAIT);

            txProgress = EVENT_PROGRESS_FINISHED;

            // モード設定
            workMode = WORK_MODE.IDLE;

            return res;
        }


        // BLEスキャン
        static public async Task<List<ulong>> BleScan(int sec)
        {
            if (workMode == WORK_MODE.BLE_SCAN)
            {
                return await keBle.ScanDevice(sec);
            }
            return new List<ulong>();
        }


        // BLE低速接続
        static public async Task<bool> BleSlowConnect(ulong addr)
        {
            bool res = true;

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.BLE_CONN_SLOW_CHANGING;

            // 進捗：10％
            txProgress = 0.1;

            // BLE停止制御（失敗したら何もしない）
            if (res) res = await StopBle(beforeMode);

            // 進捗：20％
            txProgress = 0.2;

            // BLE接続
            if (res) res = await keBle.ConnectKeCard(addr);
            if (res) await Task.Delay(CONNECTED_WAIT);

            // Ver確認
            if (res) res = keBle.ver2Flag == true ? true : false;

            // 進捗：40％
            txProgress = 0.4;

            // Timeコマンド送信
            if (res) res = await keBle.SendTimeCmd();

            // Slowコマンド送信
            if (res) res = await KeCtrl.keBle.SendModeChangeCmd(false);
            if (res) await Task.Delay(SLOW_CMD_WAIT);

            // 進捗：50％
            txProgress = 0.5;

            // 一旦切断
            if (res)
            {
                keBle.DisconnectKeCard();
                await keBle.WaitDisconnect(DISCONNECTED_WAIT);
            }

            // 進捗：80％
            txProgress = 0.8;

            // 再度BLE接続
            if (res) res = await keBle.ConnectKeCard(addr);
            if (res) await Task.Delay(CONNECTED_WAIT);

            // 進捗：100％
            txProgress = 1.0;

            // 失敗した時の後処理
            if (!res)
            {
                keBle.DisconnectKeCard();
                await keBle.WaitDisconnect(DISCONNECTED_WAIT);
            }


            // モード設定
            workMode = res ? WORK_MODE.BLE_CONN_SLOW : WORK_MODE.IDLE;

            return res;
        }


        // BLE低速接続
        static public async Task<bool> BleFastConnect(ulong addr)
        {
            bool res = true;

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.BLE_CONN_SLOW_CHANGING;

            // 進捗：10％
            txProgress = 0.1;

            // BLE停止制御（失敗したら何もしない）
            if (res) res = await StopBle(beforeMode);

            // モード設定
            workMode = WORK_MODE.BLE_CONN_FAST_CHANGING;

            // 進捗：30％
            txProgress = 0.3;

            // BLE接続
            if( res ) res = await keBle.ConnectKeCard(addr);
            await Task.Delay(CONNECTED_WAIT);

            // 進捗：100％
            txProgress = 1.0;

            // 失敗した時の後処理
            if (!res)
            {
                keBle.DisconnectKeCard();
                await keBle.WaitDisconnect(DISCONNECTED_WAIT);
            }

            // モード設定
            workMode = res ? WORK_MODE.BLE_CONN_FAST : WORK_MODE.IDLE;

            return res;

        }


        // イメージ切替
        static public async Task<bool> SwitchImage(byte no)
        {
            bool res = true;
            // 接続中ではない？
            if (!keBle.hasConnected) return false;
            if(workMode != WORK_MODE.BLE_CONN_FAST
                && workMode != WORK_MODE.BLE_CONN_SLOW)
            {
                return false;
            }

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.SWITCH_IMAGE;

            if (res) res = await keBle.SelectImage(no);

            if (res) res = await keBle.SendCommand(Cmd.CMD_DISPLAY);

            // モード設定
            workMode = beforeMode;

            return res;
        }


        // keconfig読み込み
        static public async Task<Tuple<bool,KeConfig>> ReadKeConfig(ulong addr)
        {
            bool res = true;
            KeConfig keConfig = new KeConfig();

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.READ_KECONFIG;

            txProgress = 0.1;

            // BLE停止制御（失敗したら何もしない）
            res = await StopBle(beforeMode);

            txProgress = 0.2;

            Debug.WriteLine("device:" + addr.ToString("X06"));

            if (res) res = await keBle.ConnectKeCard(addr);
            txProgress = TX_PROGRESS_CONNECTED;

            txProgress = 0.4;

            // Ver確認
            if (res) res = keBle.ver2Flag == true ? true : false;


            if (res)
            {
                var r = await keBle.SendRcvReadDataCmd(KeConfigDef.DATA_ADDR, (UInt16)(Marshal.SizeOf(typeof(KeConfig))));

                res = r.Item1;

                if(res)
                {
                    StructLib.BytesToStruct<KeConfig>(r.Item2, out keConfig);
                }

                txProgress = 0.8;

            }

            keBle.DisconnectKeCard();
            await keBle.WaitDisconnect(DISCONNECTED_WAIT);

            txProgress = 1.0;

            // モード設定
            workMode = WORK_MODE.IDLE;

            return new Tuple<bool, KeConfig>( res , keConfig);

        }


        // keconfig読み込み
        static public async Task<bool> WriteKeConfig(ulong addr , KeConfig keConfig)
        {
            bool res = true;

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.WRITE_KECONFIG;

            txProgress = 0.1;

            // BLE停止制御（失敗したら何もしない）
            res = await StopBle(beforeMode);
            txProgress = 0.2;


            Debug.WriteLine("device:" + addr.ToString("X06"));

            // BLE接続
            if (res) res = await keBle.ConnectKeCard(addr);
            txProgress = TX_PROGRESS_CONNECTED;
            txProgress = 0.4;

            // Ver確認
            if (res) res = keBle.ver2Flag == true ? true : false;

            // Erase
            if (res) res = await keBle.SendEraseDataCmd(KeConfigDef.DATA_ADDR, 1);

            txProgress = 0.7;

            // Write
            if (res)
            {
                byte[] data = StructLib.StructToBytes<KeConfig>(keConfig);

                res = await keBle.SendWriteDataCmd(KeConfigDef.DATA_ADDR, data );

                txProgress = 0.9;

            }

            // BLE切断
            keBle.DisconnectKeCard();
            await keBle.WaitDisconnect(DISCONNECTED_WAIT);

            txProgress = 1.0;

            // モード設定
            workMode = WORK_MODE.IDLE;

            return res;
        }

        // BLEスキャンモード開始
        static public async Task<bool> SetScanMode()
        {
            bool res = true;

            // モード設定
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.BLE_CONN_SLOW_CHANGING;

            // 進捗：10％
            txProgress = 0.1;


            // BLE停止制御（失敗したら何もしない）
            if (res) res = await StopBle(beforeMode);


            // 進捗：100％
            txProgress = 1.0;

            // モード設定
            workMode = res ? WORK_MODE.BLE_SCAN : WORK_MODE.IDLE;

            return res;
        }


        // BLE制御停止
        static private async Task<bool> StopBle(WORK_MODE mode)
        {

            // 現在のモード
            switch (mode)
            {
                case WORK_MODE.IDLE:               // アイドル状態
                    break;
                case WORK_MODE.BLE_SCAN:           // スキャン
                    keBle.StopScan();
                    break;
                case WORK_MODE.BLE_CONN_FAST:      // 接続(高速)
                case WORK_MODE.BLE_CONN_SLOW:      // 接続(低速)
                    keBle.DisconnectKeCard();
                    await keBle.WaitDisconnect(DISCONNECTED_WAIT);
                    break;
                case WORK_MODE.IMAGE_TX:
                case WORK_MODE.EVENT_TX:
                    return false;
                    break;
            }

            return true;
        }


        // IDLEモードへ遷移
        static public async Task SetIdleMode()
        {
            WORK_MODE beforeMode = workMode;
            workMode = WORK_MODE.IDLE_CHANGING;

            // 進捗：10％
            txProgress = 0.1;

            bool res = await StopBle(beforeMode);

            // 進捗：100％
            txProgress = 1.0;

            workMode = WORK_MODE.IDLE;

        }


    }


}
