using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class KeRes
    {

        public const int RES_NONE = 0x00;          // なし
        public const int RES_NOTIFY = 0x01;        // Notify
        public const int RES_RSSI = 0x02;          // RSSI

        // 位置
        public const int RES_TYPE_POS = 0;         // タイプ位置
        public const int RES_DATA_POS = 1;         // データ位置

        // 長さ
        public const int RES_NOTIFY_LEN = 2;       // Notify
        public const int RES_RSSI_LEN = 2;         // RSSI
    }
}
