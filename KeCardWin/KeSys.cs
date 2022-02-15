using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class KeSys
    {
        
        // システム状態
        public const int KESYS_STATE_INIT           = 0x01;     // 初期状態
        public const int KESYS_STATE_IDLE           = 0x02;     // アイドル状態
        public const int KESYS_STATE_ADV_FAST       = 0x04;     // 高速アドバタイジング
        public const int KESYS_STATE_ADV_SLOW       = 0x08;     // 低速アドバタイジング
        public const int KESYS_STATE_ADV_FOR_SLOW   = 0x10;     // 低速接続のアドバタイジング
        public const int KESYS_STATE_CONN_FAST      = 0x20;     // BLE接続(高速)
        public const int KESYS_STATE_CONN_SLOW      = 0x40;     // BLE接続(低速)
        public const int KESYS_STATE_ALL            = 0xFF;     // 全て


    }
}
