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

        public const int PROC_MAX = 24;      /*手順の最大数 */
        public const int INVALID_NO = 0xFF; /* 不正No */
        public const int BUTTON_PROCEDURE_NO = 0;
        public const int HEADER_SIZE = 2;
        public const int PACKET_SIZE = 2 + 16;
        public const int COND_PACKET_SIZE = 8;
        public const int ACTION_PACKET_SIZE = 4;
        public const int PROCEDURE_NO_START = 1;

        // COND_INFO
        public const int COND_INFO_SUB_COND = 0x01;
        public const int COND_INFO_REPEAT = 0x02;
        public const int COND_INFO_ENABLE = 0x04;

        // ACTION_TYPE
        public enum ACTION_TYPE : byte {
            NONE=0,             // なし
            IMAGE,              // イメージ変更
            NOTIFY,             // 通知
            RSSI,               // RSSI
            DISCONNECT,         // BLE切断
            IDLE,               // Adv->Idle
            ADV_START,          // Idle->Adv
            P_OFF,              // P.OFF
            CANCEL,             // 条件キャンセル
            ENABLE,             // 条件有効化
            DISABLE,            // 条件無効化
            MAX,
        };

        static public string[] ACTION_TYPE_STR =
        {
            "なし",
            "イメージ変更",
            "通知",
            "RSSI",
            "BLE切断",
            "Idle",
            "AdvStart",
            "パワーOFF",
            "キャンセル",
            "Enable",
            "Disable",
        };

        const byte ACTION_IMAGE_NEXT_VALID = 0xFC;
        const byte ACTION_IMAGE_PREV_VALID = 0xFD;
        const byte ACTION_IMAGE_NEXT = 0xFE;
        const byte ACTION_IMAGE_PREV = 0xFF;

    }

    // クラス 条件
    public class RunbookCond
    {
        // パケットデータ取得
        public virtual byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            for( int i = 0 ; i < Runbook.COND_PACKET_SIZE; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        
    }

    // 条件 CONNECT
    public class RunbookCondConnect : RunbookCond
    {
        public KeEvent.CONNECT_TYPE conType;        // 接続タイプ

        // コンストラクタ
        public RunbookCondConnect()
        {
            conType = KeEvent.CONNECT_TYPE.NONE;
        }

        // コンストラクタ
        public RunbookCondConnect(KeEvent.CONNECT_TYPE _conType)
        {
            conType = _conType;
        }

        // パケットデータ取得
        public override byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)conType );
            for( int i = 0 ; i < Runbook.COND_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        
    }


    // 条件 DISCONNECT
    public class RunbookCondDisconnect : RunbookCond
    {
        public KeEvent.CONNECT_TYPE conType;        // 接続タイプ
        public KeEvent.DISCONNECT_TYPE disconType;  // 切断タイプ

        // コンストラクタ
        public RunbookCondDisconnect()
        {
            conType = KeEvent.CONNECT_TYPE.NONE;
            disconType = KeEvent.DISCONNECT_TYPE.NONE;
        }

        // コンストラクタ
        public RunbookCondDisconnect(KeEvent.CONNECT_TYPE _conType , KeEvent.DISCONNECT_TYPE _disconType)
        {
            conType = _conType;
            disconType = _disconType;
        }

        // パケットデータ取得
        public override byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)conType );
            packet.Add( (byte)disconType );
            for( int i = 0 ; i < Runbook.COND_PACKET_SIZE - 2 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        
    }


    // 条件 HELLO
    public class RunbookCondHello : RunbookCond
    {
        public int userId;                      // User Id

        // コンストラクタ
        public RunbookCondHello()
        {
            userId = 0;
        }

        // コンストラクタ
        public RunbookCondHello(int _userId)
        {
            userId = _userId;
        }

        // パケットデータ取得
        public override byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)((userId >> 0) & 0xFF) );
            packet.Add( (byte)((userId >> 8) & 0xFF) );
            for( int i = 0 ; i < Runbook.COND_PACKET_SIZE - 2 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }

    }


    // 条件 タイマー
    public class RunbookCondTimer : RunbookCond
    {
        public int period;                 // 周期(秒)
        public int start;                  // 開始時刻

        // コンストラクタ
        public RunbookCondTimer()
        {
            period = 0;
            start = 0;
        }

        // コンストラクタ
        public RunbookCondTimer(int _period )
        {
            period = _period;
            start = 0;
        }

        // パケットデータ取得
        public override byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)((period >> 0) & 0xFF) );
            packet.Add( (byte)((period >> 8) & 0xFF) );
            packet.Add( (byte)((period >> 16) & 0xFF) );
            packet.Add( (byte)((period >> 24) & 0xFF) );

            packet.Add( (byte)((start >> 0) & 0xFF) );
            packet.Add( (byte)((start >> 8) & 0xFF) );
            packet.Add( (byte)((start >> 16) & 0xFF) );
            packet.Add( (byte)((start >> 24) & 0xFF) );

            return packet.ToArray();
        }

    }

    // 条件 ボタン
    public class RunbookCondButton : RunbookCond
    {
        public KeEvent.BUTTON_PUSHED btnType;        // 押され方

        // コンストラクタ
        public RunbookCondButton()
        {
            btnType = KeEvent.BUTTON_PUSHED.NONE;
        }

        // コンストラクタ
        public RunbookCondButton(KeEvent.BUTTON_PUSHED _btnType)
        {
            btnType = _btnType;
        }

        // パケットデータ取得
        public override byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)btnType );
            for( int i = 0 ; i < Runbook.COND_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }

    }

    // 条件 RSSI
    public class RunbookCondRssi : RunbookCond
    {
        public int rssiMin;               // Min
        public int rssiMax;               // Max

        // コンストラクタ
        public RunbookCondRssi()
        {
            rssiMin = sbyte.MinValue;
            rssiMax = sbyte.MaxValue;
        }

        // コンストラクタ
        public RunbookCondRssi(int _rssiMin , int _rssiMax)
        {
            rssiMin = _rssiMin;
            rssiMax = _rssiMax;
        }

        // パケットデータ取得
        public override byte[] getCondPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)rssiMin );
            packet.Add( (byte)rssiMax );
            for( int i = 0 ; i < Runbook.COND_PACKET_SIZE - 2 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }

    }


    // アクション共通
    public class RunbookAction
    {
        // パケットデータ取得
        public virtual byte[] getActPacket()
        {
            List<byte> packet = new List<byte>();

            for( int i = 0 ; i < Runbook.ACTION_PACKET_SIZE ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        
    };


    // アクション イメージ変更
    public class RunbookActionImage : RunbookAction
    {
        public int imageNo;                // イメージNo

        // コンストラクタ
        public RunbookActionImage()
        {
            imageNo = 0;
        }

        // コンストラクタ
        public RunbookActionImage(int _imageNo)
        {
            imageNo = _imageNo;
        }

        // パケットデータ取得
        public override byte[] getActPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)imageNo );
            for( int i = 0 ; i < Runbook.ACTION_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        

    };


    // アクション 通知
    public class RunbookActionNotify : RunbookAction
    {
        public int msg;

        // コンストラクタ
        public RunbookActionNotify()
        {
            msg = 0;
        }

        // コンストラクタ
        public RunbookActionNotify(int _msg)
        {
            msg = _msg;
        }

        // パケットデータ取得
        public override byte[] getActPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)msg );
            for( int i = 0 ; i < Runbook.ACTION_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        

    };


    // アクション キャンセル
    public class RunbookActionCancel : RunbookAction
    {
        public int condNo;

        // コンストラクタ
        public RunbookActionCancel()
        {
            condNo = 0;
        }

        // コンストラクタ
        public RunbookActionCancel(int _condNo)
        {
            condNo = _condNo;
        }

        // パケットデータ取得
        public override byte[] getActPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)condNo);
            for( int i = 0 ; i < Runbook.ACTION_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        

    };

    // アクション 有効化
    public class RunbookActionEnable : RunbookAction
    {
        public int condNo;

        // コンストラクタ
        public RunbookActionEnable()
        {
            condNo = 0;
        }

        // コンストラクタ
        public RunbookActionEnable(int _condNo)
        {
            condNo = _condNo;
        }

        // パケットデータ取得
        public override byte[] getActPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)condNo);
            for( int i = 0 ; i < Runbook.ACTION_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        

    };

    // アクション 無効化
    public class RunbookActionDisable : RunbookAction
    {
        public int condNo;

        // コンストラクタ
        public RunbookActionDisable()
        {
            condNo = 0;
        }

        // コンストラクタ
        public RunbookActionDisable(int _condNo)
        {
            condNo = _condNo;
        }

        // パケットデータ取得
        public override byte[] getActPacket()
        {
            List<byte> packet = new List<byte>();

            packet.Add( (byte)condNo);
            for( int i = 0 ; i < Runbook.ACTION_PACKET_SIZE - 1 ; i ++ ) packet.Add( (byte)0x00 );

            return packet.ToArray();
        }        

    };





    // クラス 手順
    public class RunbookProcedure
    {
        public int procNo;                      // 番号
        public KeEvent.EVENT_TYPE condType;     // 条件の種類
        public int sysState;                    // システム状態
        public Runbook.ACTION_TYPE actType;     // アクションの種類
        public bool subCond;                    // サブ条件(上の条件が満たされていたら実行する条件)
        public bool repeat;                     // リピート
        public bool enable;                     // 有効・無効

        public RunbookCond cond;                // 条件
        public RunbookAction action;            // アクション

        // コンストラクタ
        public RunbookProcedure()
        {
            procNo = 0;                         // 番号
            condType = KeEvent.EVENT_TYPE.NONE; // 条件の種類
            sysState = KeSys.KESYS_STATE_ALL;   // システム状態
            actType = Runbook.ACTION_TYPE.NONE; // アクションの種類
            subCond = false;                    // サブ条件(上の条件が満たされていたら実行する条件)
            repeat = false;                     // リピート
            enable = false;                     // 有効・無効

            cond = new RunbookCond();           // 条件
            action = new RunbookAction();       // アクション

        }

        // コンストラクタ
        public RunbookProcedure(int _procNo, int _sysState, bool _subCond, bool _repeat, bool _enable )
        {
            procNo = _procNo;                   // 番号
            condType = KeEvent.EVENT_TYPE.NONE; // 条件の種類
            sysState = _sysState;               // システム状態
            actType = Runbook.ACTION_TYPE.NONE; // アクションの種類
            subCond = _subCond;                 // サブ条件(上の条件が満たされていたら実行する条件)
            repeat = _repeat;                   // リピート
            enable = _enable;                   // 有効・無効

            cond = new RunbookCond();           // 条件
            action = new RunbookAction();       // アクション
        }


        // コンストラクタ
        public RunbookProcedure( int _procNo , KeEvent.EVENT_TYPE _condType , int _sysState , Runbook.ACTION_TYPE _actType , bool _subCond , bool _repeat , bool _enable )
        {
            procNo = _procNo;                   // 番号
            condType = _condType;               // 条件の種類
            sysState = _sysState;               // システム状態
            actType = _actType;                 // アクションの種類
            subCond = _subCond;                 // サブ条件(上の条件が満たされていたら実行する条件)
            repeat = _repeat;                   // リピート
            enable = _enable;                   // 有効・無効

            // 条件
            switch( condType ) {
                case KeEvent.EVENT_TYPE.CONNECT:
                    cond = new RunbookCondConnect();           
                    break;
                case KeEvent.EVENT_TYPE.DISCONNECT:
                    cond = new RunbookCondDisconnect();           
                    break;
                case KeEvent.EVENT_TYPE.HELLO:
                    cond = new RunbookCondHello();           
                    break;
                case KeEvent.EVENT_TYPE.TIMER:
                    cond = new RunbookCondTimer();           
                    break;
                case KeEvent.EVENT_TYPE.BUTTON:
                    cond = new RunbookCondButton();           
                    break;
                case KeEvent.EVENT_TYPE.RSSI:
                    cond = new RunbookCondRssi();           
                    break;
                default:
                    cond = new RunbookCond();           
                    break;
            }

            // アクション
            switch( actType ) {
                case Runbook.ACTION_TYPE.IMAGE: // イメージ変更
                    action = new RunbookActionImage();
                    break;
                case Runbook.ACTION_TYPE.NOTIFY: // 通知
                    action = new RunbookActionNotify();
                    break;
                case Runbook.ACTION_TYPE.CANCEL: // 条件キャンセル
                    action = new RunbookActionCancel();
                    break;
                case Runbook.ACTION_TYPE.ENABLE: // 条件有効化
                    action = new RunbookActionEnable();
                    break;
                case Runbook.ACTION_TYPE.DISABLE: // 条件無効化
                    action = new RunbookActionDisable();
                    break;
                default:
                    action = new RunbookAction();
                    break;
            }


        }

        // パケットデータ取得
        public byte[] getPacket()
        {

            int info = (subCond ? Runbook.COND_INFO_SUB_COND : 0) + (repeat ? Runbook.COND_INFO_REPEAT : 0) + (enable ? Runbook.COND_INFO_ENABLE : 0);

            List<byte> packet = new List<byte>();

            // ヘッダ
            packet.Add( (byte)procNo );
            packet.Add( (byte)0x00 );

            // 共通
            packet.Add( (byte)condType );   // 条件の種類
            packet.Add( (byte)sysState );   // システム状態
            packet.Add( (byte)actType );    // アクションの種類
            packet.Add( (byte)info );       // サブ条件、リピート、有効・無効

            // 条件
            packet.AddRange( cond.getCondPacket() );

            // アクション
            packet.AddRange( action.getActPacket() );

            return packet.ToArray();
        }

        // CONNECT条件設定
        public void setConnectCond(KeEvent.CONNECT_TYPE _conType)
        {
            condType = KeEvent.EVENT_TYPE.CONNECT;
            cond = new RunbookCondConnect( _conType );
        }

        // DISCONNECT条件設定
        public void setDisconnectCond(KeEvent.CONNECT_TYPE _conType , KeEvent.DISCONNECT_TYPE _disconType)
        {
            condType = KeEvent.EVENT_TYPE.DISCONNECT;
            cond = new RunbookCondDisconnect( _conType , _disconType );
        }

        // HELLO条件設定
        public void setHelloCond(int _userId)
        {
            condType = KeEvent.EVENT_TYPE.HELLO;
            cond = new RunbookCondHello( _userId );
        }

        // タイマー条件設定
        public void setTimerCond(int _period )
        {
            condType = KeEvent.EVENT_TYPE.TIMER;
            cond = new RunbookCondTimer( _period );
        }

        // ボタン条件設定
        public void setButtonCond(KeEvent.BUTTON_PUSHED _btnType)
        {
            condType = KeEvent.EVENT_TYPE.BUTTON;
            cond = new RunbookCondButton( _btnType );
        }

        // RSSI 条件設定
        public void setRssiCond(int _rssiMin , int _rssiMax)
        {
            condType = KeEvent.EVENT_TYPE.RSSI;
            cond = new RunbookCondRssi( _rssiMin , _rssiMax );
        }

        // Image アクション設定
        public void setImageAction(int _imageNo)
        {
            actType = Runbook.ACTION_TYPE.IMAGE;
            action = new RunbookActionImage( _imageNo );
        }

        // 通知 アクション設定
        public void setNotifyAction(int _msg)
        {
            actType = Runbook.ACTION_TYPE.NOTIFY;
            action = new RunbookActionNotify( _msg );
        }

        // キャンセル アクション設定
        public void setCancelAction(int _condNo)
        {
            actType = Runbook.ACTION_TYPE.CANCEL;
            action = new RunbookActionCancel( _condNo );
        }

        // Enable アクション設定
        public void setEnableAction(int _condNo)
        {
            actType = Runbook.ACTION_TYPE.ENABLE;
            action = new RunbookActionEnable( _condNo );
        }

        // Disable アクション設定
        public void setDisableAction(int _condNo)
        {
            actType = Runbook.ACTION_TYPE.DISABLE;
            action = new RunbookActionDisable( _condNo );
        }


    };
}
