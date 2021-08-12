
namespace KeCardWin
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.barTransfer = new System.Windows.Forms.ProgressBar();
            this.tmrProgress = new System.Windows.Forms.Timer(this.components);
            this.cmbBleAddr = new System.Windows.Forms.ComboBox();
            this.tmrScan = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilterOff = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilterDithering = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilterSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDark = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMemo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRuledLineNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRuledLineDash = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRuledLineTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbImageNo = new System.Windows.Forms.ComboBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageMemo = new System.Windows.Forms.TabPage();
            this.txtMemo = new KeCardWin.ExRichTextBox();
            this.cmenuEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.btnBookmark = new System.Windows.Forms.Button();
            this.picKeImage = new System.Windows.Forms.PictureBox();
            this.menuBookmark = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.memuAddBookmark1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddBookmark2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddBookmark3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReferBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.chkVoice = new System.Windows.Forms.CheckBox();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageMemo.SuspendLayout();
            this.cmenuEdit.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKeImage)).BeginInit();
            this.menuBookmark.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCapture
            // 
            this.btnCapture.Enabled = false;
            this.btnCapture.Location = new System.Drawing.Point(223, 50);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(218, 54);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "スクリーンキャプチャ";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(447, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 54);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(12, 50);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(205, 54);
            this.btnTransfer.TabIndex = 4;
            this.btnTransfer.Text = "(E)CARDへ転送";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // barTransfer
            // 
            this.barTransfer.Location = new System.Drawing.Point(12, 610);
            this.barTransfer.Name = "barTransfer";
            this.barTransfer.Size = new System.Drawing.Size(561, 31);
            this.barTransfer.TabIndex = 5;
            // 
            // tmrProgress
            // 
            this.tmrProgress.Enabled = true;
            this.tmrProgress.Interval = 500;
            this.tmrProgress.Tick += new System.EventHandler(this.tmrProgress_Tick);
            // 
            // cmbBleAddr
            // 
            this.cmbBleAddr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBleAddr.FormattingEnabled = true;
            this.cmbBleAddr.Location = new System.Drawing.Point(12, 110);
            this.cmbBleAddr.Name = "cmbBleAddr";
            this.cmbBleAddr.Size = new System.Drawing.Size(324, 32);
            this.cmbBleAddr.TabIndex = 6;
            // 
            // tmrScan
            // 
            this.tmrScan.Enabled = true;
            this.tmrScan.Interval = 6000;
            this.tmrScan.Tick += new System.EventHandler(this.tmrScan_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.settingsSToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(588, 42);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(129, 38);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(223, 44);
            this.menuExit.Text = "終了(&E)";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // settingsSToolStripMenuItem
            // 
            this.settingsSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem,
            this.menuDark,
            this.menuMemo,
            this.menuSettings});
            this.settingsSToolStripMenuItem.Name = "settingsSToolStripMenuItem";
            this.settingsSToolStripMenuItem.Size = new System.Drawing.Size(110, 38);
            this.settingsSToolStripMenuItem.Text = "設定(&S)";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFilterOff,
            this.menuFilterDithering,
            this.menuFilterSketch});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.filterToolStripMenuItem.Text = "フィルター(&F)";
            // 
            // menuFilterOff
            // 
            this.menuFilterOff.Checked = true;
            this.menuFilterOff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuFilterOff.Name = "menuFilterOff";
            this.menuFilterOff.Size = new System.Drawing.Size(287, 44);
            this.menuFilterOff.Text = "なし(&N)";
            this.menuFilterOff.Click += new System.EventHandler(this.menuFilterOff_Click);
            // 
            // menuFilterDithering
            // 
            this.menuFilterDithering.Name = "menuFilterDithering";
            this.menuFilterDithering.Size = new System.Drawing.Size(287, 44);
            this.menuFilterDithering.Text = "ディザリング(&D)";
            this.menuFilterDithering.Click += new System.EventHandler(this.menuFilterDithering_Click);
            // 
            // menuFilterSketch
            // 
            this.menuFilterSketch.Name = "menuFilterSketch";
            this.menuFilterSketch.Size = new System.Drawing.Size(287, 44);
            this.menuFilterSketch.Text = "スケッチ風(&S)";
            this.menuFilterSketch.Click += new System.EventHandler(this.menuFilterSketch_Click);
            // 
            // menuDark
            // 
            this.menuDark.Name = "menuDark";
            this.menuDark.Size = new System.Drawing.Size(359, 44);
            this.menuDark.Text = "濃く(&D)";
            this.menuDark.Click += new System.EventHandler(this.menuDark_Click);
            // 
            // menuMemo
            // 
            this.menuMemo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRuledLineNone,
            this.menuRuledLineDash,
            this.menuRuledLineTitle});
            this.menuMemo.Name = "menuMemo";
            this.menuMemo.Size = new System.Drawing.Size(359, 44);
            this.menuMemo.Text = "メモの罫線(&L)";
            // 
            // menuRuledLineNone
            // 
            this.menuRuledLineNone.Name = "menuRuledLineNone";
            this.menuRuledLineNone.Size = new System.Drawing.Size(359, 44);
            this.menuRuledLineNone.Text = "なし(&N)";
            this.menuRuledLineNone.Click += new System.EventHandler(this.menuRuledLineNone_Click);
            // 
            // menuRuledLineDash
            // 
            this.menuRuledLineDash.Name = "menuRuledLineDash";
            this.menuRuledLineDash.Size = new System.Drawing.Size(359, 44);
            this.menuRuledLineDash.Text = "破線(&D)";
            this.menuRuledLineDash.Click += new System.EventHandler(this.menuRuledLineDash_Click);
            // 
            // menuRuledLineTitle
            // 
            this.menuRuledLineTitle.Name = "menuRuledLineTitle";
            this.menuRuledLineTitle.Size = new System.Drawing.Size(359, 44);
            this.menuRuledLineTitle.Text = "タイトル(&T)";
            this.menuRuledLineTitle.Click += new System.EventHandler(this.menuRuledLineTitle_Click);
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVersionInfo});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(124, 38);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // menuVersionInfo
            // 
            this.menuVersionInfo.Name = "menuVersionInfo";
            this.menuVersionInfo.Size = new System.Drawing.Size(312, 44);
            this.menuVersionInfo.Text = "バージョン情報(&A)";
            this.menuVersionInfo.Click += new System.EventHandler(this.menuVersionInfo_Click);
            // 
            // cmbImageNo
            // 
            this.cmbImageNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageNo.FormattingEnabled = true;
            this.cmbImageNo.Location = new System.Drawing.Point(342, 110);
            this.cmbImageNo.Name = "cmbImageNo";
            this.cmbImageNo.Size = new System.Drawing.Size(187, 32);
            this.cmbImageNo.TabIndex = 8;
            this.cmbImageNo.SelectedIndexChanged += new System.EventHandler(this.cmbImageNo_SelectedIndexChanged);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageMemo);
            this.tabMain.Controls.Add(this.tabPageImage);
            this.tabMain.Location = new System.Drawing.Point(12, 157);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(561, 447);
            this.tabMain.TabIndex = 10;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPageMemo
            // 
            this.tabPageMemo.Controls.Add(this.txtMemo);
            this.tabPageMemo.Location = new System.Drawing.Point(8, 39);
            this.tabPageMemo.Name = "tabPageMemo";
            this.tabPageMemo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMemo.Size = new System.Drawing.Size(545, 400);
            this.tabPageMemo.TabIndex = 0;
            this.tabPageMemo.Text = "メモ";
            this.tabPageMemo.UseVisualStyleBackColor = true;
            // 
            // txtMemo
            // 
            this.txtMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMemo.ContextMenuStrip = this.cmenuEdit;
            this.txtMemo.Font = new System.Drawing.Font("MS UI Gothic", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMemo.Location = new System.Drawing.Point(8, 9);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtMemo.Size = new System.Drawing.Size(528, 352);
            this.txtMemo.TabIndex = 17;
            this.txtMemo.Text = "";
            // 
            // cmenuEdit
            // 
            this.cmenuEdit.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmenuEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuEditCut,
            this.cmenuEditCopy,
            this.cmenuEditPaste,
            this.cmenuEditDelete,
            this.cmenuEditSelectAll});
            this.cmenuEdit.Name = "cmenuEdit";
            this.cmenuEdit.Size = new System.Drawing.Size(228, 194);
            this.cmenuEdit.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuEdit_Opening);
            // 
            // cmenuEditCut
            // 
            this.cmenuEditCut.Name = "cmenuEditCut";
            this.cmenuEditCut.Size = new System.Drawing.Size(227, 38);
            this.cmenuEditCut.Text = "切り取り(&T)";
            this.cmenuEditCut.Click += new System.EventHandler(this.cmenuEditCut_Click);
            // 
            // cmenuEditCopy
            // 
            this.cmenuEditCopy.Name = "cmenuEditCopy";
            this.cmenuEditCopy.Size = new System.Drawing.Size(227, 38);
            this.cmenuEditCopy.Text = "コピー(&C)";
            this.cmenuEditCopy.Click += new System.EventHandler(this.cmenuEditCopy_Click);
            // 
            // cmenuEditPaste
            // 
            this.cmenuEditPaste.Name = "cmenuEditPaste";
            this.cmenuEditPaste.Size = new System.Drawing.Size(227, 38);
            this.cmenuEditPaste.Text = "貼り付け(&P)";
            this.cmenuEditPaste.Click += new System.EventHandler(this.cmenuEditPaste_Click);
            // 
            // cmenuEditDelete
            // 
            this.cmenuEditDelete.Name = "cmenuEditDelete";
            this.cmenuEditDelete.Size = new System.Drawing.Size(227, 38);
            this.cmenuEditDelete.Text = "削除(&D)";
            this.cmenuEditDelete.Click += new System.EventHandler(this.cmenuEditDelete_Click);
            // 
            // cmenuEditSelectAll
            // 
            this.cmenuEditSelectAll.Name = "cmenuEditSelectAll";
            this.cmenuEditSelectAll.Size = new System.Drawing.Size(227, 38);
            this.cmenuEditSelectAll.Text = "全てを選択(&A)";
            this.cmenuEditSelectAll.Click += new System.EventHandler(this.cmenuEditSelectAll_Click);
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.btnBookmark);
            this.tabPageImage.Controls.Add(this.picKeImage);
            this.tabPageImage.Location = new System.Drawing.Point(8, 39);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImage.Size = new System.Drawing.Size(545, 400);
            this.tabPageImage.TabIndex = 1;
            this.tabPageImage.Text = "イメージ";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // btnBookmark
            // 
            this.btnBookmark.Image = ((System.Drawing.Image)(resources.GetObject("btnBookmark.Image")));
            this.btnBookmark.Location = new System.Drawing.Point(11, 6);
            this.btnBookmark.Name = "btnBookmark";
            this.btnBookmark.Size = new System.Drawing.Size(44, 30);
            this.btnBookmark.TabIndex = 14;
            this.btnBookmark.UseVisualStyleBackColor = true;
            this.btnBookmark.Click += new System.EventHandler(this.btnBookmark_Click);
            // 
            // picKeImage
            // 
            this.picKeImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picKeImage.Location = new System.Drawing.Point(11, 42);
            this.picKeImage.Name = "picKeImage";
            this.picKeImage.Size = new System.Drawing.Size(528, 352);
            this.picKeImage.TabIndex = 13;
            this.picKeImage.TabStop = false;
            this.picKeImage.DragDrop += new System.Windows.Forms.DragEventHandler(this.picKeImage_DragDrop);
            this.picKeImage.DragEnter += new System.Windows.Forms.DragEventHandler(this.picKeImage_DragEnter);
            // 
            // menuBookmark
            // 
            this.menuBookmark.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuBookmark.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddBookmark,
            this.menuReferBookmark});
            this.menuBookmark.Name = "menuBookmark";
            this.menuBookmark.Size = new System.Drawing.Size(261, 80);
            // 
            // menuAddBookmark
            // 
            this.menuAddBookmark.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memuAddBookmark1,
            this.menuAddBookmark2,
            this.menuAddBookmark3});
            this.menuAddBookmark.Name = "menuAddBookmark";
            this.menuAddBookmark.Size = new System.Drawing.Size(260, 38);
            this.menuAddBookmark.Text = "お気に入りへ登録";
            // 
            // memuAddBookmark1
            // 
            this.memuAddBookmark1.Name = "memuAddBookmark1";
            this.memuAddBookmark1.Size = new System.Drawing.Size(342, 44);
            this.memuAddBookmark1.Text = "お気に入り１に登録";
            this.memuAddBookmark1.Click += new System.EventHandler(this.memuAddBookmark1_Click);
            // 
            // menuAddBookmark2
            // 
            this.menuAddBookmark2.Name = "menuAddBookmark2";
            this.menuAddBookmark2.Size = new System.Drawing.Size(342, 44);
            this.menuAddBookmark2.Text = "お気に入り２に登録";
            this.menuAddBookmark2.Click += new System.EventHandler(this.menuAddBookmark2_Click);
            // 
            // menuAddBookmark3
            // 
            this.menuAddBookmark3.Name = "menuAddBookmark3";
            this.menuAddBookmark3.Size = new System.Drawing.Size(342, 44);
            this.menuAddBookmark3.Text = "お気に入り３に登録";
            this.menuAddBookmark3.Click += new System.EventHandler(this.menuAddBookmark3_Click);
            // 
            // menuReferBookmark
            // 
            this.menuReferBookmark.Name = "menuReferBookmark";
            this.menuReferBookmark.Size = new System.Drawing.Size(260, 38);
            this.menuReferBookmark.Text = "お気に入りを編集";
            this.menuReferBookmark.Click += new System.EventHandler(this.menuReferBookmark_Click);
            // 
            // chkVoice
            // 
            this.chkVoice.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkVoice.AutoSize = true;
            this.chkVoice.Image = ((System.Drawing.Image)(resources.GetObject("chkVoice.Image")));
            this.chkVoice.Location = new System.Drawing.Point(535, 110);
            this.chkVoice.Name = "chkVoice";
            this.chkVoice.Size = new System.Drawing.Size(38, 38);
            this.chkVoice.TabIndex = 12;
            this.chkVoice.UseVisualStyleBackColor = true;
            this.chkVoice.CheckedChanged += new System.EventHandler(this.chkVoice_CheckedChanged);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(359, 44);
            this.menuSettings.Text = "設定(&S)";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 651);
            this.Controls.Add(this.chkVoice);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.cmbImageNo);
            this.Controls.Add(this.cmbBleAddr);
            this.Controls.Add(this.barTransfer);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "(E)CARD for Win";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPageMemo.ResumeLayout(false);
            this.cmenuEdit.ResumeLayout(false);
            this.tabPageImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picKeImage)).EndInit();
            this.menuBookmark.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.ProgressBar barTransfer;
        private System.Windows.Forms.Timer tmrProgress;
        private System.Windows.Forms.ComboBox cmbBleAddr;
        private System.Windows.Forms.Timer tmrScan;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuFilterOff;
        private System.Windows.Forms.ToolStripMenuItem menuFilterDithering;
        private System.Windows.Forms.ToolStripMenuItem menuFilterSketch;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ComboBox cmbImageNo;
        private System.Windows.Forms.ToolStripMenuItem menuDark;
        private System.Windows.Forms.ToolStripMenuItem menuMemo;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.PictureBox picKeImage;
        private System.Windows.Forms.ToolStripMenuItem menuRuledLineNone;
        private System.Windows.Forms.ToolStripMenuItem menuRuledLineDash;
        private System.Windows.Forms.ToolStripMenuItem menuRuledLineTitle;
        private System.Windows.Forms.ContextMenuStrip cmenuEdit;
        private System.Windows.Forms.ToolStripMenuItem cmenuEditCut;
        private System.Windows.Forms.ToolStripMenuItem cmenuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem cmenuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem cmenuEditDelete;
        private System.Windows.Forms.ToolStripMenuItem cmenuEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuVersionInfo;
        private System.Windows.Forms.TabPage tabPageMemo;
        private ExRichTextBox txtMemo;
        private System.Windows.Forms.Button btnBookmark;
        private System.Windows.Forms.ContextMenuStrip menuBookmark;
        private System.Windows.Forms.ToolStripMenuItem menuAddBookmark;
        private System.Windows.Forms.ToolStripMenuItem memuAddBookmark1;
        private System.Windows.Forms.ToolStripMenuItem menuAddBookmark2;
        private System.Windows.Forms.ToolStripMenuItem menuAddBookmark3;
        private System.Windows.Forms.ToolStripMenuItem menuReferBookmark;
        private System.Windows.Forms.CheckBox chkVoice;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
    }
}

