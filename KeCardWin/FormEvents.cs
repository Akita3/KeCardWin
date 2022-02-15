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

    // デリゲート：
    public delegate void DelegateFormEventsUpdateBleAddr(ulong addr);
    public delegate void DelegateFormEventsTimerProgress();


    public partial class FormEvents : Form
    {
        const int EVENT_CTRL_X = 10;
        const int EVENT_CTRL_Y = 120;
        const int EVENT_CTRL_Y_GAP = 6;

        public ApEvents apEventsEdit = new ApEvents();
        private List<CtrlEvent> ctrlEvents = new List<CtrlEvent>();
        public bool bleConnectionNeeds = false;

        static public OpenFileDialog OpenImageFileDialog()
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "画像ファイル(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "画像ファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            return ofd;
        }


        public FormEvents()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormEventSelect frm = new FormEventSelect();

            if( (frm.ShowDialog() == DialogResult.OK)
                && (frm.apEvent != null ) )
            {
                AddEvent(frm.apEvent , null);

                // コントロール位置更新
                UpdateCtrlsPos();
            }
        }

        private void FormEvents_Load(object sender, EventArgs e)
        {
            if(apEventsEdit.apEvents != null )
            {
                CtrlEvent beforeCtrlEvent = null;
                foreach (ApEvent apEvent in apEventsEdit.apEvents)
                {
                    beforeCtrlEvent = AddEvent( apEvent , apEvent.isSubEvent ? beforeCtrlEvent : null);
                }

                // コントロール位置更新
                UpdateCtrlsPos();
            }
        }

        public CtrlEvent AddEvent(ApEvent addApEvent , CtrlEvent baseCtrlEvent)
        {
            CtrlEvent ctrlEvent = new CtrlEvent();
            ctrlEvent.apEvent = addApEvent;

            // 画像を事前転送して、スイッチさせるタイプ
            if( addApEvent.isSwitchImageType )
            {
                int no = GetNextImageNo();
                if( no < 0 )
                {
                    MessageBox.Show("これ以上(E)CARD内部にイメージを保存する事ができません。");
                    return null;
                }

                ctrlEvent.apEvent.imageNo = no;
            }

            AddCtrlEvent(ctrlEvent , baseCtrlEvent);

            return ctrlEvent;
        }

        public void AddCtrlEvent(CtrlEvent addCtrlEvent , CtrlEvent baseCtrlEvent)
        {
            // 位置を設定
            // addCtrlEvent.Location = new Point(EVENT_CTRL_X, y);

            // デリゲート登録
            addCtrlEvent.delegateCtrlEventsUpButtonClick = CtrlEventsUpButtonClick;
            addCtrlEvent.delegateCtrlEventsDownButtonClick = CtrlEventsDownButtonClick;
            addCtrlEvent.delegateCtrlEventsDeleteButtonClick = CtrlEventsDeleteButtonClick;
            addCtrlEvent.delegateCtrlEventsAddSubButtonClick = CtrlEventsAddSubButtonClick;

            // フォームに追加
            this.Controls.Add(addCtrlEvent);

            // ルートイベント？
            if(baseCtrlEvent == null)
            {
                // ルートイベント：リストに追加

                ctrlEvents.Add(addCtrlEvent);
            } else
            {
                // サブイベント：リンクに追加

                baseCtrlEvent.subCtrlEvent = addCtrlEvent;
                addCtrlEvent.parentCtrlEvent = baseCtrlEvent;
            }

        }

        private void UpdateApEvents()
        {
            List<ApEvent> apEventList = new List<ApEvent>();

            foreach( CtrlEvent ctrlEvent in ctrlEvents )
            {
                CtrlEvent tg = ctrlEvent;
                while( tg != null )
                {
                    tg.UpdateApEvent();
                    apEventList.Add(tg.apEvent);

                    tg = tg.subCtrlEvent;
                }
            }

            apEventsEdit.apEvents = apEventList.ToArray();

        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            bool res = false;

            // 設定保存
            btnSave_Click(null, null);

            // 低速接続とスキャンのみ許可
            if (KeCtrl.workMode != KeCtrl.WORK_MODE.IDLE
                && KeCtrl.workMode != KeCtrl.WORK_MODE.BLE_CONN_SLOW
                && KeCtrl.workMode != KeCtrl.WORK_MODE.BLE_SCAN )
            {
                txtMsg.Text = "エラー：動作状態が異常です。";
                return;
            }

            // (E)CARDへ送信するデータがある？
            if(apEventsEdit.TxDataExists() )
            {
                // イベント送信
                res = await KeCtrl.ApEventsTransmissionSequence(FormMain.mFormMain.bleAddr, apEventsEdit, 20);

                if (res)
                {
                    txtMsg.Text = "成功：(E)CARDへのイベント送信が完了しました。";
                }
                else
                {
                    txtMsg.Text = "エラー：(E)CARDへのイベント送信に失敗しました。";
                }
            }

            // BLE接続が必要？
            bleConnectionNeeds = apEventsEdit.BleConnectionNeeds();

            // スキャンモードへ遷移
            await KeCtrl .SetScanMode();

            // 低速接続するかを聞く
            if(res && bleConnectionNeeds)
            {
                DialogResult dres = MessageBox.Show("機能を有効にするには無線接続が必要です。今すぐ無線接続(低速)しますか？", "無線接続の確認", MessageBoxButtons.YesNo);

                if( dres == DialogResult.No )
                {
                    bleConnectionNeeds = false;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateCtrlsPos()
        {
            int y = txtBleAddr.Bottom + EVENT_CTRL_Y_GAP;

            foreach( CtrlEvent ctrlEvent in ctrlEvents)
            {
                // ルートイベント
                ctrlEvent.Location = new Point(EVENT_CTRL_X, y);
                y += ctrlEvent.Height + EVENT_CTRL_Y_GAP;

                // サブイベント
                CtrlEvent subEvent = ctrlEvent.subCtrlEvent;
                while( subEvent != null )
                {
                    subEvent.Location = new Point(EVENT_CTRL_X, y);
                    y += subEvent.Height + EVENT_CTRL_Y_GAP;
                    subEvent = subEvent.subCtrlEvent;
                }
            }
        }

        private int GetNextImageNo()
        {
            bool[] v = new bool[KeBle.KE_IMAGE_NO_MAX + 1];

            // 使用しているNoを調べる
            foreach( var ctrl in ctrlEvents )
            {
                CtrlEvent tg = ctrl;

                while( tg != null )
                {
                    if (tg.apEvent.isSwitchImageType)
                    {
                        int no = tg.apEvent.imageNo;

                        if (0 <= no && no <= KeBle.KE_IMAGE_NO_MAX)
                        {
                            v[no] = true;
                        }
                    }

                    tg = tg.subCtrlEvent;
                }
            }

            // 使用していない最初の番号を返す
            for( int i = 0; i <= KeBle.KE_IMAGE_NO_MAX; i ++ )
            {
                if (v[i] == false) return i;
            }

            return -1;
        }

        public void UpdateBleAddr(ulong addr)
        {
            if( addr != 0x00 )
            {
                txtBleAddr.Text = KeBle.BleAddrToString(addr);
            }
        }

        public void TimerProgress()
        {
            barTransfer.Value = (int)(KeCtrl.txProgress * 100);
        }

        public void CtrlEventsUpButtonClick(CtrlEvent ctrlEvent)
        {
            // サブイベントは移動できない
            if (ctrlEvent.apEvent.isSubEvent) return;

            // 上へ移動させるオブジェクトのIndexを取得
            int idx = ctrlEvents.IndexOf(ctrlEvent);
            if (idx <= 0) return;

            // まず除去する
            ctrlEvents.RemoveAt(idx);

            // 一つ前へ追加する
            ctrlEvents.Insert(idx - 1, ctrlEvent);

            // コントロール位置更新
            UpdateCtrlsPos();

        }
        public void CtrlEventsDownButtonClick(CtrlEvent ctrlEvent)
        {
            // サブイベントは移動できない
            if (ctrlEvent.apEvent.isSubEvent) return;

            // 下へ移動させるオブジェクトのIndexを取得
            int idx = ctrlEvents.IndexOf(ctrlEvent);
            if (idx < 0 || ctrlEvents.Count -1 <= idx) return;

            // まず除去する
            ctrlEvents.RemoveAt(idx);

            // 一つ後へ追加する
            ctrlEvents.Insert(idx + 1, ctrlEvent);

            // コントロール位置更新
            UpdateCtrlsPos();

        }
        public void CtrlEventsDeleteButtonClick(CtrlEvent ctrlEvent)
        {

            // サブイベントを全て削除
            CtrlEvent subEvent = ctrlEvent.subCtrlEvent;
            while(subEvent != null )
            {
                this.Controls.Remove(subEvent);
                subEvent = subEvent.subCtrlEvent;
            }

            // 対象を削除
            this.Controls.Remove(ctrlEvent);

            // ルートイベント？
            if (ctrlEvent.parentCtrlEvent == null)
            {
                // ルートイベント：リストから除去
                ctrlEvents.Remove(ctrlEvent);
            }
            else
            {
                // サブイベント：親から切り離し
                ctrlEvent.parentCtrlEvent.subCtrlEvent = null;
                ctrlEvent.parentCtrlEvent = null;
            }

            // コントロール位置更新
            UpdateCtrlsPos();

        }
        public void CtrlEventsAddSubButtonClick(CtrlEvent ctrlEvent)
        {

            FormEventSelect frm = new FormEventSelect();
            frm.isSubEvent = true;

            if ((frm.ShowDialog() == DialogResult.OK)
                && (frm.apEvent != null))
            {
                AddEvent(frm.apEvent , ctrlEvent);

                // コントロール位置更新
                UpdateCtrlsPos();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // イベント情報更新
            UpdateApEvents();

            // XMLに出力
            ApEventsLib.SerializeObject(apEventsEdit);
        }
    }
}
