using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KeCardWin
{
    public class Runbook
    {

        public const int RUNBOOK_COND_MAX = 8;      /* 条件最大数 taira */
        public const int RUNBOOK_ACTION_MAX = 4;    /* アクション最大数 taira */
        public const int RUNBOOK_INVALID_NO = 0xFF; /* 不正No */



        public enum COND_INFO : byte
        {
            SUB_COND = 0x01,
            REPEAT_ = 0x02,
            ENABLE = 0x04,
        }

        public static byte GetCondInfo(bool _subCond , bool _repeat , bool _enable)
        {
            int value = (_subCond ? (int)COND_INFO.SUB_COND : 0 ) + (_subCond ? (int)COND_INFO.REPEAT_ : 0) + (_subCond ? (int)COND_INFO.ENABLE  : 0);
            return (byte)value;
        }

        // 条件 共通
        public struct CondCommon
        {
            public byte cond_no;                // 条件の番号
            public KeEvent.EVENT_TYPE cond_type;// 条件の種類
            public byte action_no;              // アクションNo
            public byte cond_info;             // 
            // sub_cond : 1;           // サブ条件(上の条件が満たされていたら実行する条件)
            // repeat : 1;             // リピート
            // enable : 1;             // 有効・無効

            public void SetAll(byte _cond_no, KeEvent.EVENT_TYPE _cond_type, byte _action_no, bool _sub_cond, bool _repeat, bool _enable)
            {
                cond_no = _cond_no;
                cond_type = _cond_type;
                action_no = _action_no;
                cond_info = 0x00;
                if (_sub_cond) cond_info |= (byte)COND_INFO.SUB_COND;
                if (_repeat) cond_info |= (byte)COND_INFO.REPEAT_;
                if (_enable) cond_info |= (byte)COND_INFO.ENABLE;
            }
        };


        // 条件 CONNECT
        public struct CondConnect
        {
            public byte con_type;               // 接続タイプ
        };


        // 条件 DISCONNECT
        public struct CondDisconnect
        {
            public byte con_type;               // 接続タイプ
            public byte discon_type;            // 切断タイプ
        };


        // 条件 HELLO
        public struct CondHello
        {
            public ushort user_id;                // User Id
        };


        // 条件 タイマー
        public struct CondTimer
        {
            public int period;                 // 周期(秒)
            public int start;                  // 開始時刻
        };

        // 条件 ボタン
        public struct CondButton{
            public byte btn_type;                // 押され方
        };

        // 条件 RSSI
        public struct CondRssi
        {
            public sbyte rssi_min;               // Min
            public sbyte rssi_max;               // Max
        };



        // 条件 データ
        [StructLayout(LayoutKind.Explicit)]
        public struct CondData
        {
            [FieldOffset(0)]
            public CondConnect connect;
            [FieldOffset(0)]
            public CondDisconnect disconnect;
            [FieldOffset(0)]
            public CondHello hello;
            [FieldOffset(0)]
            public CondTimer timer;
            [FieldOffset(0)]
            public CondButton button;
            [FieldOffset(0)]
            public CondRssi rssi;
        };


        // 条件 ALL
        public struct CondPacket
        {
            public CondCommon common;
            public CondData data;

            public static byte[] ToBytes(CondPacket obj)
            {
                int size = Marshal.SizeOf(typeof(CondPacket));
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(obj, ptr, false);

                byte[] bytes = new byte[size];
                Marshal.Copy(ptr, bytes, 0, size);
                Marshal.FreeHGlobal(ptr);
                return bytes;
            }
        };


        // アクションの種類
        public enum ACTION_TYPE : byte
        {
            NONE = 0,              // なし
            IMAGE,               // イメージ変更
            NOTIFY,              // 通知
            DISCONNECT,          // BLE切断
            MAX,
        };

        static public string[] ACTION_TYPE_STR =
        {
            "なし",
            "イメージ変更",
            "通知",
            "BLE切断",
        };


        const byte RUNBOOK_ACTION_IMAGE_NEXT_VALID = 0xFC;
        const byte RUNBOOK_ACTION_IMAGE_PREV_VALID = 0xFD;
        const byte RUNBOOK_ACTION_IMAGE_NEXT = 0xFE;
        const byte RUNBOOK_ACTION_IMAGE_PREV = 0xFF;


        // アクション共通
        public struct ActionCommon
        {
            public byte act_no;                 // アクションの番号
            public ACTION_TYPE act_type;        // アクションの種類
            public ushort dummy;                  // ダミー(4byteアライメントの為)
        };


        // アクション イメージ変更
        public struct ActionImage
        {
            public byte image_no;                   // イメージNo
            public byte dummy1;
            public byte dummy2;
            public byte dummy3;
        };


        // アクション 通知
        public struct ActionNotify
        {
            public byte msg;
            public byte dummy1;
            public byte dummy2;
            public byte dummy3;
        };


        // アクション データ
        [StructLayout(LayoutKind.Explicit)]
        public struct ActionData
        {
            [FieldOffset(0)]
            public ActionImage image;           // イメージ切り替え
            [FieldOffset(0)]
            public ActionNotify notify;         // 通知
        };

        // アクション ALL
        public struct ActionPacket
        {
            public ActionCommon common;         // 共通
            public ActionData data;             // データ

            public static byte[] ToBytes(ActionPacket obj)
            {
                int size = Marshal.SizeOf(typeof(ActionPacket));
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(obj, ptr, false);

                byte[] bytes = new byte[size];
                Marshal.Copy(ptr, bytes, 0, size);
                Marshal.FreeHGlobal(ptr);
                return bytes;
            }

        };


    }
}
