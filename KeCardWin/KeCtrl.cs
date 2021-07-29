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
        static KeBle keBle = new KeBle();

        static bool dontScanFlag = false;       // スキャン禁止フラグ
        public static double txProgress = 0.0;  // 進捗状況

        // 進捗用の定数
        const double TX_PROGRESS_START = 0.0;
        const double TX_PROGRESS_SCAN_STOPPED = 0.05;
        const double TX_PROGRESS_CONNECTED = 0.1;
        const double TX_PROGRESS_FLASHED = 0.2;
        const double TX_PROGRESS_TX_START = TX_PROGRESS_FLASHED;
        const double TX_PROGRESS_FINISHED = 1.0;

        // データ送信シーケンス
        static public async Task<bool> DataTransmissionSequence(ulong addr , Bitmap image , byte no)
        {
            bool res = false;

            // don't scan
            dontScanFlag = true;

            txProgress = TX_PROGRESS_START;

            // stop scan
            keBle.StopScan();
            txProgress = TX_PROGRESS_SCAN_STOPPED;

            Debug.WriteLine("device:" + addr.ToString("X06"));

            await keBle.ConnectKeCard(addr);
            txProgress = TX_PROGRESS_CONNECTED;


            await keBle.SelectImage(no);

            await keBle.SendCommand(KeBle.KE_CMD_ERASE_FLASH);

            await keBle.WaitStatus(KeBle.KE_STATUS_FLASH_ERASE_SUCCESS, 20);
            txProgress = TX_PROGRESS_FLASHED;

            KeImage keImage = new KeImage();

            bool r = keImage.SetImage(image);
            if (r)
            {

                int sendNoMax = keImage.GetSendNoMax();
                for (int i = 0; i < sendNoMax; i++)
                {

                    byte[] data = keImage.GetSendPacket();
                    await keBle.SendImageData(data);
                    txProgress = TX_PROGRESS_TX_START + (TX_PROGRESS_FINISHED - TX_PROGRESS_TX_START) * i / sendNoMax;
                }
            }
            await keBle.SendCommand(KeBle.KE_CMD_DISPLAY);

            keBle.DisconnectKeCard();
            txProgress = TX_PROGRESS_FINISHED;

            res = true;

            // scan OK
            dontScanFlag = false;

            return res;
        }

        // BLEスキャン
        static public async Task<List<ulong>> BleScan(int sec)
        {
            if (dontScanFlag == false)
            {
                List<ulong> devices = await keBle.ScanDevice(sec);
                return devices;
            }
            return new List<ulong>();
        }

    }


}
