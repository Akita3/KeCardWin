using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;


namespace KeCardWin
{

    [Serializable]
    public class ApEvent
    {
        public virtual EVENT_TYPE eventType { get { return EVENT_TYPE.NONE; } }     // イベントタイプ
        public virtual bool isSwitchImageType { get { return false; } }             // イメージをスイッチするタイプ？((E)CARDにイメージをストアするタイプ？)
        public virtual bool txDataExists { get { return false; } }                  // 送信データが存在する？
        public virtual bool bleConnectionNeeds { get { return false; } }            // BLE接続が必要？
        public virtual bool canRootEvent { get { return false; } }                  // ルートイベントになれる？
        public virtual bool canSubEvent { get { return false; } }                   // サブイベントになれる？
        public int subEventType = 0;
        public string imagePath = "";
        public int imageNo = -1;
        public bool isSubEvent = false;


        // GrayImageを取得
        public Bitmap GetGray2bitImage()
        {
            // ファイル存在確認
            Bitmap bitmap = ImgLib.LoadImage(imagePath);

            // 読み込めなかった場合はサンプル
            if(bitmap == null )
            {
                bitmap = Properties.Resources.SAMPLE_IMAGE;
            }

            // 送信イメージ
            Bitmap grayImage = KeImage.ImageToGray2bitImage(bitmap, FormMain.appSetting.imgFilter, FormMain.appSetting.darkMode);

            return grayImage;
        }

        // Runbookパケット取得
        public virtual List<RunbookProcedure> getRunbookProcedure( int _no )
        {
            List<RunbookProcedure> procs = new List<RunbookProcedure>();

            RunbookProcedure proc = new RunbookProcedure(_no
                                                        , KeEvent.EVENT_TYPE.NONE
                                                        , KeSys.KESYS_STATE_ALL
                                                        , Runbook.ACTION_TYPE.NONE
                                                        , false
                                                        , false
                                                        , false);
            procs.Add(proc);

            return procs;
        }

        // イベントタイプ
        public enum EVENT_TYPE : int
        {
            NONE = 0,
            TIMER,
            BUTTON,
            PC,
            VOICE,
            INIT,
            MAX,
        }

        // イベントタイプ文字列
        public static readonly string[] EVENT_TYPE_STR =
        {
        "なし",
        "時刻・タイマー",
        "ボタン",
        "パソコン",
        "音声",
        "初期イメージ",
        };

    }

    [Serializable]
    public class ApEventTimer : ApEvent
    {
        public override EVENT_TYPE eventType { get { return EVENT_TYPE.TIMER; } }
        public override bool isSwitchImageType { get { return true; } }
        public override bool txDataExists { get { return true; } }
        public override bool bleConnectionNeeds { get { return false; } }
        public override bool canRootEvent { get { return true; } }
        public override bool canSubEvent { get { return true; } }
        public DateTime dateTime;
        public int timeout;

        public const int TIMEOUT_DEFAULT = 10 * 60;

        public enum TIMER_EVENT_TYPE : int
        {
            DATE_TIME,
            TIMER,
            MAX,
        }
        public static readonly string[] TIMER_EVENT_TYPE_STR =
        {
        "時刻",
        "タイマー",
        };

        public static readonly string[] TIMER_EVENT_TYPE_INFO =
        {
        "指定時刻に(E)CARDの表示が切り替わる",
        "指定時間後に(E)CARDの表示が切り替わる",
        };

        public ApEventTimer()
        {
            subEventType = (int)TIMER_EVENT_TYPE.DATE_TIME;
            dateTime = DateTime.Now;
            dateTime = dateTime.AddSeconds(TIMEOUT_DEFAULT);
            timeout = TIMEOUT_DEFAULT;
        }

        public int getPeriod()
        {
            int _period = 0;
            switch (subEventType)
            {
                case (int)TIMER_EVENT_TYPE.DATE_TIME:
                    TimeSpan timeSpan = dateTime - DateTime.Now;
                    int tm = 0;
                    if (timeSpan.TotalSeconds > 0) tm = (int)timeSpan.TotalSeconds;
                    _period = tm;
                    break;
                case (int)TIMER_EVENT_TYPE.TIMER:
                    _period = timeout;
                    break;
            }
            return _period;
        }


        // Runbookパケット取得
        public override List<RunbookProcedure> getRunbookProcedure(int _no)
        {
            RunbookProcedure proc = new RunbookProcedure( _no
                                                        , KeSys.KESYS_STATE_ALL
                                                        , isSubEvent
                                                        , false
                                                        , isSubEvent ? false : true
                                                        );

            proc.setTimerCond( getPeriod() );
            proc.setImageAction(imageNo);

            List<RunbookProcedure> procs = new List<RunbookProcedure>();
            procs.Add(proc);
            return procs;
        }

    }



    [Serializable]
    public class ApEventButton : ApEvent
    {
        public override EVENT_TYPE eventType { get { return EVENT_TYPE.BUTTON; } }
        public override bool isSwitchImageType { get { return true; } }
        public override bool txDataExists { get { return true; } }
        public override bool bleConnectionNeeds { get { return true; } }
        public override bool canRootEvent { get { return true; } }
        public override bool canSubEvent { get { return true; } }

        public enum BUTTON_EVENT_TYPE : int
        {
            PUSH,
            MAX,
        }
        public static string[] BUTTON_EVENT_TYPE_STR =
        {
            "ボタンプッシュ",
        };

        public static string[] BUTTON_EVENT_TYPE_INFO =
        {
            "ボタンが押された時に、通知を受ける",
        };


        public ApEventButton()
        {
            subEventType = (int)BUTTON_EVENT_TYPE.PUSH;

        }


        // Runbookパケット取得
        public override List<RunbookProcedure> getRunbookProcedure(int _no)
        {
            RunbookProcedure proc = new RunbookProcedure(_no
                                                        , KeSys.KESYS_STATE_ALL
                                                        , isSubEvent
                                                        , true
                                                        , isSubEvent ? false : true
                                                        );

            proc.setButtonCond(KeEvent.BUTTON_PUSHED.SHORT);
            proc.setNotifyAction((int)'A');

            List<RunbookProcedure> procs = new List<RunbookProcedure>();
            procs.Add(proc);
            return procs;
        }

    }


    [Serializable]
    public class ApEventPc : ApEvent
    {
        public override EVENT_TYPE eventType { get { return EVENT_TYPE.PC; } }
        public override bool isSwitchImageType { get { return true; } }
        public override bool txDataExists { get { return true; } }
        public override bool bleConnectionNeeds { get { return subEventType == (int)PC_EVENT_TYPE.POWER_OFF ? true : false; } }
        public override bool canRootEvent { get { return true; } }
        public override bool canSubEvent { get { return true; } }

        public enum PC_EVENT_TYPE : int
        {
            BOOT,
            POWER_OFF,
            MAX,
        }
        public static string[] PC_EVENT_TYPE_STR =
        {
            "パソコン起動",
            "パソコン終了",
        };

        public static string[] PC_EVENT_TYPE_INFO =
        {
            "パソコンが起動した時に、(E)CARDの表示を切り替える",
            "パソコンが終了した時に、(E)CARDの表示を切り替える",
        };

        public ApEventPc()
        {
            subEventType = (int)PC_EVENT_TYPE.BOOT;

        }


        // Runbookパケット取得
        public override List<RunbookProcedure> getRunbookProcedure(int _no)
        {
            RunbookProcedure proc = new RunbookProcedure(_no
                                                        , KeSys.KESYS_STATE_ALL
                                                        , isSubEvent
                                                        , true
                                                        , isSubEvent ? false : true
                                                        );

            switch (subEventType)
            {
                case (int)PC_EVENT_TYPE.BOOT:
                    proc.setHelloCond(0x00);
                    break;
                case (int)PC_EVENT_TYPE.POWER_OFF:
                    proc.setDisconnectCond(KeEvent.CONNECT_TYPE.SLOW, KeEvent.DISCONNECT_TYPE.ALL);
                    break;
                default:
                    break;
            }

            proc.setImageAction(imageNo);

            List<RunbookProcedure> procs = new List<RunbookProcedure>();
            procs.Add(proc);
            return procs;
        }


    }


    [Serializable]
    public class ApEventVoice : ApEvent
    {
        public override EVENT_TYPE eventType { get { return EVENT_TYPE.VOICE; } }
        public override bool isSwitchImageType { get { return false; } }
        public override bool txDataExists { get { return false; } }
        public override bool bleConnectionNeeds { get { return false; } }
        public override bool canRootEvent { get { return false; } }
        public override bool canSubEvent { get { return false; } }

        public string keyword = KEYWORD_DEFAULT;
        public Rectangle rect = new Rectangle(RECT_X , RECT_Y , RECT_W , RECT_H);

        public const string KEYWORD_DEFAULT = "たぬき";
        public const int RECT_X = 0;
        public const int RECT_Y = 0;
        public const int RECT_W = 100;
        public const int RECT_H = 100;

        public enum VOICE_EVENT_TYPE
        {
            IMAGE,
            SCREEN,
            MAX,
        }
        public static string[] VOICE_EVENT_TYPE_STR =
        {
            "画像",
            "スクリーンキャプチャ",
        };

        public static string[] VOICE_EVENT_TYPE_INFO =
        {
            "キーワードに対応する画像を(E)CARDへ転送して表示する",
            "キーワードに対応する画面範囲をキャプチャして(E)CARDへ転送して表示する",
        };

        public ApEventVoice()
        {
            subEventType = (int)VOICE_EVENT_TYPE.IMAGE;
        }

        // Runbookパケット取得
        public override List<RunbookProcedure> getRunbookProcedure(int _no)
        {
            List<RunbookProcedure> procs = new List<RunbookProcedure>();
            return procs;
        }


    }


    [Serializable]
    public class ApEventInit : ApEvent
    {
        public override EVENT_TYPE eventType { get { return EVENT_TYPE.INIT; } }
        public override bool isSwitchImageType { get { return true; } }
        public override bool txDataExists { get { return true; } }
        public override bool bleConnectionNeeds { get { return false; } }
        public override bool canRootEvent { get { return false; } }
        public override bool canSubEvent { get { return false; } }

        public enum INIT_EVENT_TYPE
        {
            NONE,
            MAX,
        }
        public static string[] INIT_EVENT_TYPE_STR =
        {
            "デフォルト",
        };

        public static string[] INIT_EVENT_TYPE_INFO =
        {
            "デフォルトで表示するイメージを指定する。",
        };

        public ApEventInit()
        {
            subEventType = (int)INIT_EVENT_TYPE.NONE;

        }

        // Runbookパケット取得
        public override List<RunbookProcedure> getRunbookProcedure(int _no)
        {
            List<RunbookProcedure> procs = new List<RunbookProcedure>();
            return procs;
        }

    }



}
