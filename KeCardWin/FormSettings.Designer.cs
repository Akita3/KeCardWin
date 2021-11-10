
namespace KeCardWin
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeName = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTestWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTest = new System.Windows.Forms.CheckBox();
            this.txtTestLog = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWaitAfterTransfer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPacketSize = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtKeName);
            this.groupBox1.Location = new System.Drawing.Point(12, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "音声認識";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "名前（呼びかける言葉）：";
            // 
            // txtKeName
            // 
            this.txtKeName.Location = new System.Drawing.Point(286, 37);
            this.txtKeName.Name = "txtKeName";
            this.txtKeName.Size = new System.Drawing.Size(240, 31);
            this.txtKeName.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(624, 475);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(176, 40);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(806, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(176, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtTestWord
            // 
            this.txtTestWord.Location = new System.Drawing.Point(286, 43);
            this.txtTestWord.Name = "txtTestWord";
            this.txtTestWord.Size = new System.Drawing.Size(240, 31);
            this.txtTestWord.TabIndex = 3;
            this.txtTestWord.Text = "名刺";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "テストする言葉：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTest);
            this.groupBox2.Controls.Add(this.txtTestLog);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtTestWord);
            this.groupBox2.Location = new System.Drawing.Point(12, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(970, 255);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "音声認識テスト";
            // 
            // chkTest
            // 
            this.chkTest.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTest.AutoSize = true;
            this.chkTest.Location = new System.Drawing.Point(100, 90);
            this.chkTest.Name = "chkTest";
            this.chkTest.Size = new System.Drawing.Size(169, 34);
            this.chkTest.TabIndex = 6;
            this.chkTest.Text = "音声認識テスト";
            this.chkTest.UseVisualStyleBackColor = true;
            this.chkTest.CheckedChanged += new System.EventHandler(this.chkTest_CheckedChanged);
            // 
            // txtTestLog
            // 
            this.txtTestLog.Location = new System.Drawing.Point(286, 90);
            this.txtTestLog.Multiline = true;
            this.txtTestLog.Name = "txtTestLog";
            this.txtTestLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestLog.Size = new System.Drawing.Size(663, 146);
            this.txtTestLog.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtPacketSize);
            this.groupBox3.Controls.Add(this.txtWaitAfterTransfer);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(970, 87);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "無線送信";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(258, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "データ送信後のWait[ms]：";
            // 
            // txtWaitAfterTransfer
            // 
            this.txtWaitAfterTransfer.Location = new System.Drawing.Point(286, 34);
            this.txtWaitAfterTransfer.Name = "txtWaitAfterTransfer";
            this.txtWaitAfterTransfer.Size = new System.Drawing.Size(168, 31);
            this.txtWaitAfterTransfer.TabIndex = 2;
            this.txtWaitAfterTransfer.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(501, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "パケットサイズ(16の倍数)：";
            // 
            // txtPacketSize
            // 
            this.txtPacketSize.Location = new System.Drawing.Point(762, 34);
            this.txtPacketSize.Name = "txtPacketSize";
            this.txtPacketSize.Size = new System.Drawing.Size(187, 31);
            this.txtPacketSize.TabIndex = 2;
            this.txtPacketSize.Text = "128";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 535);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkTest;
        public System.Windows.Forms.TextBox txtKeName;
        public System.Windows.Forms.TextBox txtTestWord;
        private System.Windows.Forms.TextBox txtTestLog;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtWaitAfterTransfer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPacketSize;
    }
}