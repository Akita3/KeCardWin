using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class KeEvent
    {

        // 条件の種類
        public enum EVENT_TYPE : byte
        {
            NONE = 0,          // なし
            CONNECT,          // BLE接続
            DISCONNECT,       // BLE切断
            HELLO,            // Helloコマンド
            TIMER,            // タイマー
            BUTTON,           // ボタン押下
            RSSI,             // RSSI
            MAX,              // 最大
        };

        static public string[] EVENT_TYPE_STR =
        {
            "なし",
            "BLE接続",
            "BLE切断",
            "Helloコマンド",
            "タイマー",
            "ボタン押下",
            "RSSI",
        };


        // BLE接続の種類
        public enum CONNECT_TYPE : byte
        {
            NONE = 0x00,      // なし
            FAST = 0x01,      // 高速
            SLOW = 0x02,      // 低速
            BOTH = 0x03,      // 両方
            MAX  = 0x04,      // 最大
        };

        static public string[] CONNECT_TYPE_STR =
        {
            "なし",
            "高速",
            "低速",
            "両方",
        };

        // BLE切断の理由
        public enum DISCONNECT_TYPE : byte
        {
            NONE = 0x00,        // なし
            LOCAL = 0x01,       // 自分から切断
            REMOTE = 0x02,      // 相手から切断
            TIMEOUT = 0x04,     // タイムアウト
            UNKNOWN = 0x08,     // 不明
            ALL = 0x0F,         // 全て
        };


        public enum BUTTON_PUSHED : byte
        {
            NONE = 0x00,        // なし
            SHORT = 0x01,       // 短押し
            LONG = 0x02,        // 長押し
            BOTH = 0x03,        // 両方
            MAX = 0x04,         // 最大
        };

    }
}
