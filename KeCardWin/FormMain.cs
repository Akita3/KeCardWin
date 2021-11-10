using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using Microsoft.Win32;

namespace KeCardWin
{
    // クラス：FormMain
    public partial class FormMain : Form
    {
        static public FormMain mFormMain;

        // アプリケーション設定
        public static AppSetting appSetting = new AppSetting();

        // ApEvent
        ApEvents apEventsSetting = new ApEvents();

        // スクリーン用フォーム
        FormScreen formScreen;

        // ボタン押し通知用フォーム
        FormNotification formNotification;


        // イメージ
        Bitmap baseImage;   // フィルター前のイメージ
        Bitmap keImage;     // フィルター後のイメージ

        // BLEアドレス
        public ulong bleAddr;

        // スキャン結果のBLEアドレスリスト
        public List<ulong> scanBleAddrs;

        // Tab Index 
        private const int TAB_IDX_MEMO = 0;
        private const int TAB_IDX_IMAGE = 1;

        // ボイスコマンド
        KeVoiceCmd keVoiceCmd = null;


        // BLEアドレス更新
        DelegateFormEventsUpdateBleAddr delegateFormEventsUpdateBle = null;
        DelegateFormEventsTimerProgress delegateFormEventsTimerProgress = null;

        // 転送用イメージ設定
        void SetKeImage(Bitmap srcBmp)
        {
            if (srcBmp == null) return;

            // ベース画像を保存
            baseImage = (Bitmap)srcBmp.Clone();

            keImage = KeImage.ImageToGray2bitImage(baseImage, appSetting.imgFilter, appSetting.darkMode);

            picKeImage.BackgroundImage = (Bitmap)keImage.Clone();

            AppSetting.SaveBitmap(keImage, "KeImage.png");
        }

        // スクリーンが選択された
        private void FormScreen_SelectedScreen(Bitmap scBitmap)
        {
            formScreen.Hide();
            btnCapture.Text = "スクリーンキャプチャ";

            tabMain.SelectedIndex = TAB_IDX_IMAGE;

            SetKeImage(scBitmap);
        }

        // コンボボックスのBLEアドレス更新
        private void UpdateCmbBleAddr()
        {
            foreach( ulong addr in scanBleAddrs )
            {
                string text = KeBle.BleAddrToString(addr);

                int index = cmbBleAddr.Items.IndexOf(text);
                if (index < 0)
                {
                    cmbBleAddr.Items.Add(text);

                    if (cmbBleAddr.SelectedIndex < 0)
                    {
                        cmbBleAddr.SelectedIndex = 0;
                        bleAddr = addr;

                        if(delegateFormEventsUpdateBle != null )
                        {
                            delegateFormEventsUpdateBle(bleAddr);
                        }
                    }
                }
            }
        }

        // SlotNoコンボボックス初期化
        private void CtrlCmbNoInit()
        {
            for( int i = 0;i <= KeBle.KE_IMAGE_NO_MAX; i ++ )
            {
                cmbImageNo.Items.Add( "Slot " + (i + 1).ToString());
            }
            cmbImageNo.SelectedIndex = appSetting.imageNo;
        }

        private void CtrlUpdate(KeCtrl.WORK_MODE mode)
        {
            bool enabled = false;
            if( mode == KeCtrl.WORK_MODE.IMAGE_TX 
                || mode == KeCtrl.WORK_MODE.EVENT_TX
                || mode == KeCtrl.WORK_MODE.BLE_CONN_FAST_CHANGING
                || mode == KeCtrl.WORK_MODE.BLE_CONN_SLOW_CHANGING
                || mode == KeCtrl.WORK_MODE.BLE_SCAN_CHANGING
                || mode == KeCtrl.WORK_MODE.IDLE_CHANGING )
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
            // コントロール無効化
            btnTransfer.Enabled = enabled;
            btnCapture.Enabled = enabled;
            tabMain.Enabled = enabled;
            cmbBleAddr.Enabled = enabled;
            cmbImageNo.Enabled = enabled;
            btnEditEvent.Enabled = enabled;
            btnBleSlowConnect.Enabled = enabled;
            menuTest.Enabled = enabled;
        }


        // メニューフィルターをチェック
        public void CheckMenuFilter(KeImage.IMG_FILTER filter)
        {
            menuFilterOff.Checked = (filter == KeImage.IMG_FILTER.NONE);
            menuFilterDithering.Checked = (filter == KeImage.IMG_FILTER.DITHERING);

        }

        // コンストラクタ
        public FormMain()
        {
            InitializeComponent();
            FormMain.mFormMain = this;

            //スリープ・休止状態をOSから通知してもらう
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(Detect_SleepWakeup);

        }

        // Form Load
        private void FormMain_Load(object sender, EventArgs e)
        {
            formScreen = new FormScreen();
            formScreen.selectedScreen += FormScreen_SelectedScreen;

            formNotification = new FormNotification();

            picKeImage.AllowDrop = true;

            // AppSetting.saveSettings(appSetting, "AppSettings.xml");
            appSetting = AppSetting.LoadSettings("AppSettings.xml");

            // ApEvents読み込み
            apEventsSetting = ApEventsLib.DeserializeObject();

            // メニューを設定
            CheckMenuFilter(appSetting.imgFilter);

            // ダークモード設定
            menuDark.Checked = appSetting.darkMode;

            // コンボボックス初期化
            CtrlCmbNoInit();

            // メモ読み込み
            txtMemo.Text = AppSetting.LoadMemo(appSetting.backupMemoName);

            // イメージを読み込み
            Bitmap bitmap = AppSetting.LoadBitmap(appSetting.backupImageName);
            SetKeImage(bitmap);

            // 転送データサイズの設定
            KeImage.sendDataSize = appSetting.transferPacketSize;

            // ボタン押し通知のデリゲート登録
            KeCtrl.keBle.delegateKeBleRecieveMsg += BleRecieveMsg;

            // 動作モード
            txtWorkMode.Text = KeCtrl.WORK_MODE_STR[(int)KeCtrl.workMode];

        }

        // キャプチャボタンクリック
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if(formScreen.Visible == false)
            {
                btnCapture.Text = "キャンセル";

                Bitmap bitmap = ScCap.Capture(this);
                this.TopMost = true;
                this.TopMost = false;
                formScreen.SetScreenImage(bitmap);
                formScreen.Show();

                // AppSetting.SaveBitmap(bitmap, "Screen.bmp");
            }
            else
            {
                btnCapture.Text = "スクリーンキャプチャ";

                formScreen.Hide();
            }


        }

        // (キャプチャ)キャンセルボタンクリック
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        // ドラッグ＆ドロップ
        private void picKeImage_DragDrop(object sender, DragEventArgs e)
        {

            // ファイル名？
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop);
            if ( (fileName != null) 
                && (System.IO.File.Exists(fileName[0]) == true) )
            {
                Bitmap bitmap = new Bitmap(fileName[0]);
                if (bitmap == null) return;

                SetKeImage(bitmap);
                return;
            }
            // テキスト？
            string urlLink = (string)e.Data.GetData(DataFormats.Text);
            if (urlLink != null)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(urlLink, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                SetKeImage(qrCodeImage);
                return;
            }


        }

        // ドラッグEnter
        private void picKeImage_DragEnter(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop)
                || e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        // 転送ボタンクリック
        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            // アドレスが不正
            if (bleAddr == 0x0) return;

            // 転送画像
            Bitmap transferImage;

            // Memo or Image
            if ( tabMain.SelectedIndex == TAB_IDX_MEMO )
            {
                // Memo
                transferImage = txtMemo.GetBitmap();
                transferImage = KeImage.ImageToGray2bitImageNormal(transferImage);
            }
            else
            {
                // Image
                transferImage = keImage;
            }
            if (transferImage == null) return;

            // 動作モード表示変更
            txtWorkMode.Text = KeCtrl.WORK_MODE_STR[(int)KeCtrl.WORK_MODE.IMAGE_TX];

            // コントロール更新
            CtrlUpdate(KeCtrl.WORK_MODE.EVENT_TX);

            // 低速接続とスキャンのみ許可
            if (KeCtrl.workMode != KeCtrl.WORK_MODE.IDLE
                && KeCtrl.workMode != KeCtrl.WORK_MODE.BLE_CONN_SLOW
                && KeCtrl.workMode != KeCtrl.WORK_MODE.BLE_SCAN)
            {
                return;
            }

            // イメージ転送シーケンス
            await KeCtrl.ImageTransmissionSequence(bleAddr
                                                , transferImage
                                                , appSetting.imageNo
                                                , appSetting.waitAfterTransfer);

            // 遷移前の状態(スキャンモード)へ復帰
            await KeCtrl .SetScanMode();

            // コントロール有効化
            CtrlUpdate(KeCtrl.workMode);

            // 転送イメージを保存
            AppSetting.SaveBitmap(transferImage, appSetting.transferImageName);

            // 動作モード表示変更
            txtWorkMode.Text = KeCtrl.WORK_MODE_STR[(int)KeCtrl.workMode];

        }

        // プログレスバー用タイマー定期処理
        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            barTransfer.Value = (int)(KeCtrl.txProgress * 100);
            if(delegateFormEventsTimerProgress != null)
            {
                delegateFormEventsTimerProgress();
            }
        }

        // スキャン用タイマー定期処理
        private async void tmrScan_Tick(object sender, EventArgs e)
        {
            scanBleAddrs = await KeCtrl.BleScan(5);

            UpdateCmbBleAddr();

        }

        // Form Closed
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            // 設定保存
            AppSetting.SaveSettings(appSetting, "AppSettings.xml");

            // メモバックアップ
            AppSetting.SaveMemo(appSetting.backupMemoName,txtMemo.Text);

            // イメージバックアップ
            AppSetting.SaveBitmap(baseImage, appSetting.backupImageName);
        }

        // メニュー：フィルターOFF
        private void menuFilterOff_Click(object sender, EventArgs e)
        {
            appSetting.imgFilter = KeImage.IMG_FILTER.NONE;
            CheckMenuFilter(appSetting.imgFilter);
            SetKeImage(baseImage);
        }

        // メニュー：フィルターディザリング
        private void menuFilterDithering_Click(object sender, EventArgs e)
        {
            appSetting.imgFilter = KeImage.IMG_FILTER.DITHERING;
            CheckMenuFilter(appSetting.imgFilter);
            SetKeImage(baseImage);
        }

        // メニュー：フィルタースケッチ風
        private void menuFilterSketch_Click(object sender, EventArgs e)
        {
            appSetting.imgFilter = KeImage.IMG_FILTER.SKETCH;
            CheckMenuFilter(appSetting.imgFilter);
            SetKeImage(baseImage);
        }

        // メニュー：終了
        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ImageNoコンボボックス 選択変更イベントハンドラ 
        private void cmbImageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbImageNo.SelectedIndex;
            if( 0 <= index && index <= KeBle.KE_IMAGE_NO_MAX )
            {
                appSetting.imageNo = (byte)index;
            }
        }

        // メニュー 濃い
        private void menuDark_Click(object sender, EventArgs e)
        {
            // ダークモード設定
            appSetting.darkMode = appSetting.darkMode ? false : true;
            menuDark.Checked = appSetting.darkMode;
            SetKeImage(baseImage);
        }

        // Mainタブ 選択変更イベントハンドラ
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // メニュー：罫線なし
        private void menuRuledLineNone_Click(object sender, EventArgs e)
        {
            // 罫線：なし
            appSetting.ruledLineType = ExRichTextBox.RuledLineType.None;
            txtMemo.SetRuledLineType(appSetting.ruledLineType);
        }

        // メニュー：罫線 破線
        private void menuRuledLineDash_Click(object sender, EventArgs e)
        {
            // 罫線：破線
            appSetting.ruledLineType = ExRichTextBox.RuledLineType.Dash;
            txtMemo.SetRuledLineType(appSetting.ruledLineType);
        }

        // メニュー：罫線 タイトル
        private void menuRuledLineTitle_Click(object sender, EventArgs e)
        {
            // 罫線：タイトル
            appSetting.ruledLineType = ExRichTextBox.RuledLineType.Title;
            txtMemo.SetRuledLineType(appSetting.ruledLineType);
        }

        // コンテキストメニュー(Edit) Openingイベントハンドラ
        private void cmenuEdit_Opening(object sender, CancelEventArgs e)
        {
            // 選択状態
            bool selected = (txtMemo.SelectedText.Length > 0);

            cmenuEditCut.Enabled = selected;
            cmenuEditCopy.Enabled = selected;
            cmenuEditDelete.Enabled = selected;
        }

        // コンテキストメニュー 切り取り
        private void cmenuEditCut_Click(object sender, EventArgs e)
        {
            txtMemo.Cut();
        }

        // コンテキストメニュー コピー
        private void cmenuEditCopy_Click(object sender, EventArgs e)
        {
            txtMemo.Copy();
        }

        // コンテキストメニュー 貼り付け
        private void cmenuEditPaste_Click(object sender, EventArgs e)
        {
            txtMemo.PastePlainText();
        }

        // コンテキストメニュー 削除
        private void cmenuEditDelete_Click(object sender, EventArgs e)
        {
            txtMemo.SelectedText = "";
        }

        // コンテキストメニュー 全てを選択
        private void cmenuEditSelectAll_Click(object sender, EventArgs e)
        {
            txtMemo.SelectAll();
        }

        // メニュー：バージョン情報
        private void menuVersionInfo_Click(object sender, EventArgs e)
        {
            AboutBoxMain aboutBoxMain = new AboutBoxMain();
            aboutBoxMain.ShowDialog();
        }


        private void KeVoiceCmdRecognized(string word)
        {
            ApEvent ap = apEventsSetting.apEvents.FirstOrDefault(x => x.eventType == ApEvent.EVENT_TYPE.VOICE && ((ApEventVoice)x).keyword.Contains(word));
            if (ap == null) return;

            ApEventVoice apEventVoice = (ApEventVoice)ap;
            Bitmap bitmap;
            if (apEventVoice.subEventType == (int)ApEventVoice.VOICE_EVENT_TYPE.SCREEN )
            {
                // キャプチャモード
                bitmap = ScCap.Capture(this, apEventVoice.rect);
            }
            else
            {
                // ファイルモード
                bitmap = new Bitmap(apEventVoice.imagePath);
            }

            if (bitmap == null) return;
            SetKeImage(bitmap);

            // タブを切り替え
            tabMain.SelectedIndex = TAB_IDX_IMAGE;

            if ( btnTransfer.Enabled == true )
            {
                btnTransfer_Click(null, null);
            }

        }


        private void KeVoiceOnOff(bool sw)
        {
            // ON
            if(sw)
            {
                string[] keywords = apEventsSetting.GetVoiceKeywords();

                if(keywords.Count() > 0)
                {
                    keVoiceCmd = new KeVoiceCmd();
                    keVoiceCmd.Open(appSetting.keName, keywords);
                    keVoiceCmd.delegateKeVoiceCmdRecognized = KeVoiceCmdRecognized;
                } else
                {
                    sw = false;
                }
            }

            // OFF
            if(!sw)
            {
                if(keVoiceCmd != null)
                {
                    keVoiceCmd.Close();
                    keVoiceCmd = null;
                }
            }

            // 色を設定
            btnVoice.BackColor = keVoiceCmd == null ? SystemColors.Control : SystemColors.ControlDark;
        }


        private void menuSettings_Click(object sender, EventArgs e)
        {
            // 設定保存
            bool checkState = keVoiceCmd == null ? false : true;

            // 音声認識無効へ
            KeVoiceOnOff(false);

            FormSettings frm = new FormSettings();


            frm.txtWaitAfterTransfer.Text = appSetting.waitAfterTransfer.ToString();
            frm.txtPacketSize.Text = appSetting.transferPacketSize.ToString();
            frm.txtKeName.Text = appSetting.keName;

            if( frm.ShowDialog() == DialogResult.OK )
            {
                int.TryParse(frm.txtWaitAfterTransfer.Text, out appSetting.waitAfterTransfer);
                int.TryParse(frm.txtPacketSize.Text, out appSetting.transferPacketSize);
                KeImage.sendDataSize = appSetting.transferPacketSize;
                appSetting.keName = frm.txtKeName.Text;
            }

            // 設定復元
            KeVoiceOnOff(checkState);
        }

        private async void menuTest_Click(object sender, EventArgs e)
        {
            // 今のモードをチェック
            if(KeCtrl.workMode != KeCtrl.WORK_MODE.BLE_SCAN)
            {
                MessageBox.Show("無線接続を停止してください。");
                return;
            }

            FormTest frm = new FormTest();
            frm.testBleAddr = bleAddr;

            // デリゲート登録
            KeCtrl.keBle.delegateKeBleRecieveMsg = frm.BleRecieveMsg;

            // don't scan
            await KeCtrl.SetIdleMode();

            frm.ShowDialog();

            // scan モードへ
            await KeCtrl.SetScanMode();

            // デリゲート再登録
            KeCtrl.keBle.delegateKeBleRecieveMsg = BleRecieveMsg;

        }

        // パソコンがレジューム復帰した
        private void PcResume()
        {
            ApEventPc apEventPc = (ApEventPc)apEventsSetting.SearchFirst(ApEvent.EVENT_TYPE.PC, (int)ApEventPc.PC_EVENT_TYPE.BOOT);
            if (apEventPc == null) return;

            // タブを切り替え
            tabMain.SelectedIndex = TAB_IDX_IMAGE;

            // イメージを読み込み
            Bitmap bitmap = AppSetting.LoadBitmap(apEventPc.imagePath);
            if (bitmap == null) return;

            SetKeImage(bitmap);
            btnTransfer_Click(null, null);

        }


        private void Detect_SleepWakeup(object sender, PowerModeChangedEventArgs e)

        {

            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    //オペレーティング システムが中断されます。
                    // btnTransfer_Click(null, null);
                    // MessageBox.Show("中断");
                    break;

                case PowerModes.Resume:
                    PcResume();
                    //オペレーティング システムが中断状態から再開されます。
                    // MessageBox.Show("レジューム");
                    // btnTransfer_Click(null, null);
                    break;
                case PowerModes.StatusChange:
                    //電源モードのステータス通知がオペレーティング システムで発生しました。 
                    //これは、バッテリ電力が低下した、バッテリの充電中、AC 電源と
                    //バッテリの間で移行しているなど、システム電源のステータスが
                    //  変化したことを示している可能性があります。
                    break;
            }
        }

        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            // 今の状態を取得
            bool voiceState = keVoiceCmd == null ? false : true;

            FormEvents frm = new FormEvents();

            // 強制OFF
            KeVoiceOnOff(false);

            delegateFormEventsUpdateBle = frm.UpdateBleAddr;
            delegateFormEventsTimerProgress = frm.TimerProgress;
            frm.apEventsEdit = apEventsSetting;
            if(bleAddr != 0x00) frm.txtBleAddr.Text = KeBle.BleAddrToString(bleAddr);

            var dres = frm.ShowDialog();

            delegateFormEventsUpdateBle = null;
            delegateFormEventsTimerProgress = null;

            // 設定を復元
            KeVoiceOnOff(voiceState);

            if( dres == DialogResult.OK )
            {
                // BLE低速接続を行う場合
                if( frm.bleConnectionNeeds )
                {
                    btnBleSlowConnect_Click(null, null);
                }
            }

        }


        public void BleRecieveMsg(byte[] data)
        {
            this.Invoke(new DelegateKeBleRecieveMsg(this.BleRecieveMsgSafe), data);
        }

        public void BleRecieveMsgSafe(byte[] data)
        {

            if(formNotification.IsDisposed )
            {
                formNotification = new FormNotification();
            }

            ApEventButton apEventButton = (ApEventButton)apEventsSetting.SearchFirst( ApEvent.EVENT_TYPE.BUTTON,(int)ApEventButton.BUTTON_EVENT_TYPE.PUSH);

            if (apEventButton != null && formNotification.Visible == false )
            {
                formNotification.apEventButton = apEventButton;
                formNotification.Show();
            }

        }

        private async void btnBleSlowConnect_Click(object sender, EventArgs e)
        {

            if( KeCtrl.workMode == KeCtrl.WORK_MODE.BLE_CONN_SLOW)
            {
                // 現在低速接続中

                // コントロール更新
                CtrlUpdate(KeCtrl.WORK_MODE.BLE_SCAN_CHANGING);

                // 動作モード表示変更
                txtWorkMode.Text = KeCtrl.WORK_MODE_STR[(int)KeCtrl.WORK_MODE.BLE_SCAN_CHANGING];

                // スキャン状態へ
                await KeCtrl.SetScanMode();

                btnBleSlowConnect.Text = "無線常時接続";

            }
            else if (KeCtrl.workMode == KeCtrl.WORK_MODE.BLE_SCAN)
            {
                // コントロール更新
                CtrlUpdate(KeCtrl.WORK_MODE.BLE_CONN_SLOW_CHANGING);

                // 動作モード表示変更
                txtWorkMode.Text = KeCtrl.WORK_MODE_STR[(int)KeCtrl.WORK_MODE.BLE_CONN_SLOW_CHANGING];

                // Idle状態へ
                await KeCtrl.SetIdleMode();

                // 低速接続
                bool res = await KeCtrl.BleSlowConnect(bleAddr);
                if( res )
                {
                    btnBleSlowConnect.Text = "無線切断";
                } else
                {
                    // スキャン状態へ
                    await KeCtrl.SetScanMode();
                }

            } else
            {

            }

            // 動作モード表示変更
            txtWorkMode.Text = KeCtrl.WORK_MODE_STR[(int)KeCtrl.workMode];

            // コントロール更新
            CtrlUpdate(KeCtrl.workMode);

        }

        private void btnVoice_Click(object sender, EventArgs e)
        {
            KeVoiceOnOff(keVoiceCmd == null ? true : false);
        }
    }
}
