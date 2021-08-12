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

namespace KeCardWin
{
    // クラス：FormMain
    public partial class FormMain : Form
    {
        // アプリケーション設定
        AppSetting appSetting = new AppSetting();

        // スクリーン用フォーム
        FormScreen formScreen;

        // イメージ
        Bitmap baseImage;   // フィルター前のイメージ
        Bitmap keImage;     // フィルター後のイメージ

        // BLEアドレス
        ulong bleAddr;

        // スキャン結果のBLEアドレスリスト
        List<ulong> scanBleAddrs;

        // Tab Index 
        private const int TAB_IDX_MEMO = 0;
        private const int TAB_IDX_IMAGE = 1;

        // ブックマーク情報
        Bookmarks bookmarks = new Bookmarks();
        Bookmark bookmarkTemp = new Bookmark();

        // ボイスコマンド
        KeVoiceCmd keVoiceCmd = null;


        // 転送用イメージ設定
        void SetKeImage(Bitmap srcBmp)
        {
            if (srcBmp == null) return;


            // ベース画像を保存
            baseImage = (Bitmap)srcBmp.Clone();


            Bitmap calcImage;
            // 濃いめ？
            if ( appSetting.darkMode )
            {
                calcImage = ImgLib.Darken(baseImage, 64);
            } else
            {
                calcImage = (Bitmap)baseImage.Clone();
            }


            switch (appSetting.imgFilter)
            {
                default:
                case AppSetting.IMG_FILTER.NONE:
                    keImage = KeImage.ImageToGray2bitImageNormal(calcImage);
                    break;
                case AppSetting.IMG_FILTER.DITHERING:
                    keImage = KeImage.ImageToGray2bitImageDithering(calcImage);
                    break;
                case AppSetting.IMG_FILTER.SKETCH:
                    var sketchImg = KeImage.NormalSketch(calcImage);
                    var th1 = 64 + 64;
                    var th2 = 160 + 32;
                    var th3 = 224;
                    keImage = KeImage.ImageToGray2bitImageNormal(sketchImg,th1 , th2 , th3);
                    break;
            }

            picKeImage.BackgroundImage = (Bitmap)keImage.Clone();

            AppSetting.SaveBitmap(keImage, "KeImage.png");
        }

        // スクリーンが選択された
        private void FormScreen_SelectedScreen(Bitmap scBitmap)
        {
            btnCancel.Enabled = false;
            formScreen.Hide();

            SetKeImage(scBitmap);

            bookmarkTemp.rect = formScreen.selectedRect;
            bookmarkTemp.bitmap = KeImage.FitImageSize(scBitmap);
        }

        // コンボボックスのBLEアドレス更新
        private void UpdateCmbBleAddr()
        {
            cmbBleAddr.Items.Clear();

            int selectIndex = -1;

            for(int i = 0; i < scanBleAddrs.Count; i ++ )
            {
                cmbBleAddr.Items.Add(KeBle.BleAddrToString(scanBleAddrs[i]));
                if (bleAddr == scanBleAddrs[i]) selectIndex = i;
            }

            if (selectIndex >= 0)
            {
                cmbBleAddr.SelectedIndex = selectIndex;
            } else
            {
                if(scanBleAddrs.Count > 0)
                {
                    cmbBleAddr.SelectedIndex = 0;
                    bleAddr = scanBleAddrs[0];
                } else
                {
                    bleAddr = 0x0;
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

        // メニューフィルターをチェック
        public void CheckMenuFilter(AppSetting.IMG_FILTER filter)
        {
            menuFilterOff.Checked = (filter == AppSetting.IMG_FILTER.NONE);
            menuFilterDithering.Checked = (filter == AppSetting.IMG_FILTER.DITHERING);

        }

        // コンストラクタ
        public FormMain()
        {
            InitializeComponent();
        }

        // Form Load
        private void FormMain_Load(object sender, EventArgs e)
        {
            formScreen = new FormScreen();
            formScreen.selectedScreen += FormScreen_SelectedScreen;

            picKeImage.AllowDrop = true;

            // AppSetting.saveSettings(appSetting, "AppSettings.xml");
            appSetting = AppSetting.LoadSettings("AppSettings.xml");

            // ブックマーク読み込み
            bookmarks = appSetting.GetBookmarks();

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

        }

        // キャプチャボタンクリック
        private void btnCapture_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = true;

            Bitmap bitmap = ScCap.Capture(this);
            this.TopMost = true;
            formScreen.SetScreenImage(bitmap);
            formScreen.Show();

            // AppSetting.SaveBitmap(bitmap, "Screen.bmp");

        }

        // (キャプチャ)キャンセルボタンクリック
        private void btnCancel_Click(object sender, EventArgs e)
        {
            formScreen.Hide();
            btnCancel.Enabled = false;

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

            // コントロール無効化
            btnCapture.Enabled = false;
            tabMain.Enabled = false;
            cmbBleAddr.Enabled = false;
            cmbImageNo.Enabled = false;

            // イメージ転送シーケンス
            await KeCtrl.DataTransmissionSequence(bleAddr
                                                , transferImage
                                                , appSetting.imageNo
                                                , appSetting.waitAfterTransfer);

            // コントロール有効化
            btnCapture.Enabled = tabMain.SelectedIndex == TAB_IDX_IMAGE ? true : false;
            tabMain.Enabled = true;
            cmbBleAddr.Enabled = true;
            cmbImageNo.Enabled = true;

            // 転送イメージを保存
            AppSetting.SaveBitmap(transferImage, appSetting.transferImageName);

        }

        // プログレスバー用タイマー定期処理
        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            barTransfer.Value = (int)(KeCtrl.txProgress * 100);
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
            appSetting.imgFilter = AppSetting.IMG_FILTER.NONE;
            CheckMenuFilter(appSetting.imgFilter);
            SetKeImage(baseImage);
        }

        // メニュー：フィルターディザリング
        private void menuFilterDithering_Click(object sender, EventArgs e)
        {
            appSetting.imgFilter = AppSetting.IMG_FILTER.DITHERING;
            CheckMenuFilter(appSetting.imgFilter);
            SetKeImage(baseImage);
        }

        // メニュー：フィルタースケッチ風
        private void menuFilterSketch_Click(object sender, EventArgs e)
        {
            appSetting.imgFilter = AppSetting.IMG_FILTER.SKETCH;
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
            btnCapture.Enabled = (tabMain.SelectedIndex == TAB_IDX_IMAGE ? true : false);
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

        private void btnBookmark_Click(object sender, EventArgs e)
        {
            menuBookmark.Show(tabPageImage, new Point(btnBookmark.Left + btnBookmark.Width, btnBookmark.Top));
        }

        private void menuEditBookmark(Bookmarks b)
        {
            FormBookmark frm = new FormBookmark();
            frm.bookmarks = b;

            if( frm.ShowDialog() == DialogResult.OK )
            {
                // 今の状態を取得
                bool voiceState = chkVoice.Checked;
                // 強制OFF
                KeVoiceOnOff(voiceState);

                bookmarks = frm.bookmarks;
                appSetting.SetBookmarks(bookmarks);
                // 設定を復元
                KeVoiceOnOff(voiceState);
            }
        }

        private void memuAddBookmark1_Click(object sender, EventArgs e)
        {
            Bookmarks tmp = bookmarks.DeepCopy();
            tmp.bookmarks[0] = bookmarkTemp.DeepCopy();
            menuEditBookmark(tmp);
        }

        private void menuAddBookmark2_Click(object sender, EventArgs e)
        {
            Bookmarks tmp = bookmarks.DeepCopy();
            tmp.bookmarks[1] = bookmarkTemp.DeepCopy();
            menuEditBookmark(tmp);
        }

        private void menuAddBookmark3_Click(object sender, EventArgs e)
        {
            Bookmarks tmp = bookmarks.DeepCopy();
            tmp.bookmarks[2] = bookmarkTemp.DeepCopy();
            menuEditBookmark(tmp);
        }

        private void menuReferBookmark_Click(object sender, EventArgs e)
        {
            Bookmarks tmp = bookmarks.DeepCopy();
            menuEditBookmark(tmp);
        }

        private void KeVoiceCmdRecognized(string word)
        {
            // キーワードから対象のブックマークを検索
            Bookmark b = bookmarks.bookmarks.FirstOrDefault(x => x.keyword.Contains(word));
            if (b == null) return;

            Bitmap bitmap;
            if ( b.mode == Bookmark.Mode.CAPTURE )
            {
                // キャプチャモード
                bitmap = ScCap.Capture(this, b.rect);
            }
            else
            {
                // ファイルモード
                bitmap = new Bitmap(b.bitmap);
            }

            if (bitmap == null) return;
            SetKeImage(bitmap);

            if( btnTransfer.Enabled == true )
            {
                btnTransfer_Click(null, null);
            }

        }


        private void KeVoiceOnOff(bool sw)
        {
            if(sw)
            {
                keVoiceCmd = new KeVoiceCmd();
                keVoiceCmd.Open(appSetting.keName , bookmarks.GetKeywords());
                keVoiceCmd.delegateKeVoiceCmdRecognized = KeVoiceCmdRecognized;
            }
            else
            {
                if(keVoiceCmd != null)
                {
                    keVoiceCmd.Close();
                    keVoiceCmd = null;
                }
            }
        }

        private void chkVoice_CheckedChanged(object sender, EventArgs e)
        {
            KeVoiceOnOff(chkVoice.Checked);
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            // 設定保存
            bool checkState = chkVoice.Checked;

            // 音声認識無効へ
            KeVoiceOnOff(false);

            FormSettings frm = new FormSettings();

            frm.txtWaitAfterTransfer.Text = appSetting.waitAfterTransfer.ToString();
            frm.txtKeName.Text = appSetting.keName;

            if( frm.ShowDialog() == DialogResult.OK )
            {
                int.TryParse(frm.txtWaitAfterTransfer.Text, out appSetting.waitAfterTransfer);
                appSetting.keName = frm.txtKeName.Text;
            }

            // 設定復元
            KeVoiceOnOff(checkState);
        }
    }
}
