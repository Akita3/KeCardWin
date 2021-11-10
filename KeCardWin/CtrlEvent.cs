using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeCardWin
{

    public delegate void DelegateCtrlEventsUpButtonClick(CtrlEvent ctrlEvent);
    public delegate void DelegateCtrlEventsDownButtonClick(CtrlEvent ctrlEvent);
    public delegate void DelegateCtrlEventsDeleteButtonClick(CtrlEvent ctrlEvent);
    public delegate void DelegateCtrlEventsAddSubButtonClick(CtrlEvent ctrlEvent);


    public partial class CtrlEvent : UserControl
    {
        public int CTRL_EVENT_X = 43;
        public int CTRL_EVENT_Y = 41;

        public ApEvent apEvent = new ApEvent();

        private UserControl ctrlEvent = new UserControl();
        public CtrlEvent subCtrlEvent = null;
        public CtrlEvent parentCtrlEvent = null;


        public DelegateCtrlEventsUpButtonClick delegateCtrlEventsUpButtonClick = null;
        public DelegateCtrlEventsDownButtonClick delegateCtrlEventsDownButtonClick = null;
        public DelegateCtrlEventsDeleteButtonClick delegateCtrlEventsDeleteButtonClick = null;
        public DelegateCtrlEventsAddSubButtonClick delegateCtrlEventsAddSubButtonClick = null;


        public CtrlEvent()
        {
            InitializeComponent();
        }

        private void CtrlEvent_Load(object sender, EventArgs e)
        {
            // タイプを表示
            string text = ApEvent.EVENT_TYPE_STR[(int)apEvent.eventType];
            if( apEvent.isSubEvent )
            {
                text += "    (サブイベント)";
            }
            txtEventType.Text = text;

            // 各コントロール作成
            switch (apEvent.eventType)
            {
                case ApEvent.EVENT_TYPE.TIMER:
                    ctrlEvent = new CtrlTimerEvent();
                    ((CtrlTimerEvent)ctrlEvent).apEventTimer = (ApEventTimer)apEvent;
                    break;
                case ApEvent.EVENT_TYPE.BUTTON:
                    ctrlEvent = new CtrlButtonEvent();
                    ((CtrlButtonEvent)ctrlEvent).apEventButton = (ApEventButton)apEvent;
                    break;
                case ApEvent.EVENT_TYPE.PC:
                    ctrlEvent = new CtrlPcEvent();
                    ((CtrlPcEvent)ctrlEvent).apEventPc = (ApEventPc)apEvent;
                    break;
                case ApEvent.EVENT_TYPE.VOICE:
                    ctrlEvent = new CtrlVoiceEvent();
                    ((CtrlVoiceEvent)ctrlEvent).apEventVoice = (ApEventVoice)apEvent;
                    break;
                case ApEvent.EVENT_TYPE.INIT:
                    ctrlEvent = new CtrlInitEvent();
                    ((CtrlInitEvent)ctrlEvent).apEventInit = (ApEventInit)apEvent;
                    break;
            }

            // SubAddボタンの有効化、無効化
            btnAddSub.Visible = apEvent.canSubEvent;

            // 上下移動ボタンの有効化、無効化
            btnUp.Visible = !apEvent.isSubEvent;
            btnDown.Visible = !apEvent.isSubEvent;

            // サブイベント
            picSubEvent.Visible = apEvent.isSubEvent;

            ctrlEvent.Location = new Point(CTRL_EVENT_X, CTRL_EVENT_Y);
            this.Controls.Add(ctrlEvent);
        }

        public void UpdateApEvent()
        {

            // 各コントロール作成
            switch (apEvent.eventType)
            {
                case ApEvent.EVENT_TYPE.TIMER:
                    ((CtrlTimerEvent)ctrlEvent).UpdateApEvent();
                    apEvent = ((CtrlTimerEvent)ctrlEvent).apEventTimer;
                    break;
                case ApEvent.EVENT_TYPE.BUTTON:
                    ((CtrlButtonEvent)ctrlEvent).UpdateApEvent();
                    apEvent = ((CtrlButtonEvent)ctrlEvent).apEventButton;
                    break;
                case ApEvent.EVENT_TYPE.PC:
                    ((CtrlPcEvent)ctrlEvent).UpdateApEvent();
                    apEvent = ((CtrlPcEvent)ctrlEvent).apEventPc;
                    break;
                case ApEvent.EVENT_TYPE.VOICE:
                    ((CtrlVoiceEvent)ctrlEvent).UpdateApEvent();
                    apEvent = ((CtrlVoiceEvent)ctrlEvent).apEventVoice;
                    break;
                case ApEvent.EVENT_TYPE.INIT:
                    ((CtrlInitEvent)ctrlEvent).UpdateApEvent();
                    apEvent = ((CtrlInitEvent)ctrlEvent).apEventInit;
                    break;
            }


        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if(delegateCtrlEventsUpButtonClick != null)
            {
                delegateCtrlEventsUpButtonClick( this );
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (delegateCtrlEventsDownButtonClick != null)
            {
                delegateCtrlEventsDownButtonClick(this);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (delegateCtrlEventsDeleteButtonClick != null)
            {
                delegateCtrlEventsDeleteButtonClick(this);
            }
        }

        private void btnAddSub_Click(object sender, EventArgs e)
        {
            if (delegateCtrlEventsAddSubButtonClick != null)
            {
                delegateCtrlEventsAddSubButtonClick(this);
            }
        }
    }
}
