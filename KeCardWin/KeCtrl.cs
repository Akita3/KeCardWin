using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Drawing;

namespace KeCardWin
{

    // クラス：KeCtrl ((E)CARD制御用のクラス)
    public class KeCtrl
    {
        // メンバ
        static public KeBle keBle = new KeBle();

        public static double txProgress = 0.0;      // 進捗状況

        public const int DISCONNECTED_WAIT = 10;          // 切断後のWait[sec]
        public const int CONNECTED_WAIT = 100;             // 接続後のWait[ms]
        public const int SLOW_CMD_WAIT = 1000;             // 接続後のWait[ms]

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

            if (res) res = await keBle.SendCommand(KeBle.KE_CMD_ERASE_FLASH);

            if (res) res = await keBle.WaitStatus(KeBle.KE_STATUS_FLASH_ERASE_SUCCESS, 20);

            txProgress = TX_PROGRESS_FLASHED;

            if (res) res = await TransferImage(image, TX_PROGRESS_FINISHED - TX_PROGRESS_TX_START, waitAfterTransfer);

            if (res) res = await keBle.SendCommand(KeBle.KE_CMD_DISPLAY);

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


            if (res) res = await keBle.SendTimeCmd();

            byte ano = 0;
            byte cno = 0;
            double per = (EVENT_PROGRESS_FINISHED - EVENT_PROGRESS_CONNECTED) / apEvents.apEvents.Count();


            // 条件、アクション
            foreach (var apEvent in apEvents.apEvents)
            {
                if (!res) break;

                // Runbookのパケット取得
                List<Runbook.CondPacket> condPackets = new List<Runbook.CondPacket>();
                List<Runbook.ActionPacket> actionPackets = new List<Runbook.ActionPacket>();
                apEvent.GetRunbookPackets(ref cno, ref ano, ref condPackets, ref actionPackets);

                // 条件パケット送信
                foreach (var condPacket in condPackets)
                {
                    if (res) res = await keBle.SendCondCmd(condPacket);
                }

                // アクションパケット送信
                foreach (var actionPacket in actionPackets)
                {
                    if (res) res = await keBle.SendActCmd(actionPacket);
                }

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
                    if (res) res = await keBle.SendCommand(KeBle.KE_CMD_ERASE_FLASH);

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

                    if (res) res = await keBle.SendCommand(KeBle.KE_CMD_DISPLAY);
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
                List<ulong> devices = await keBle.ScanDevice(sec);
                return devices;
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

            // 進捗：40％
            txProgress = 0.4;

            // Timeコマンド送信
            if (res) res = await keBle.SendTimeCmd();

            // Slowコマンド送信
            if (res) res = await KeCtrl.keBle.SendCommand(KeBle.KE_CMD_SLOW_MODE);
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

            if (res) res = await keBle.SendCommand(KeBle.KE_CMD_DISPLAY);

            // モード設定
            workMode = beforeMode;

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
