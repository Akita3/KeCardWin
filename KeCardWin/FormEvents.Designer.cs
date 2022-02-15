
namespace KeCardWin
{
    partial class FormEvents
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.barTransfer = new System.Windows.Forms.ProgressBar();
            this.txtBleAddr = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(218, 6);
            this.btnOk.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(187, 30);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "設定有効化(データ転送)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(70, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // barTransfer
            // 
            this.barTransfer.Location = new System.Drawing.Point(103, 47);
            this.barTransfer.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.barTransfer.Name = "barTransfer";
            this.barTransfer.Size = new System.Drawing.Size(136, 16);
            this.barTransfer.TabIndex = 9;
            // 
            // txtBleAddr
            // 
            this.txtBleAddr.Location = new System.Drawing.Point(6, 47);
            this.txtBleAddr.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBleAddr.Name = "txtBleAddr";
            this.txtBleAddr.ReadOnly = true;
            this.txtBleAddr.Size = new System.Drawing.Size(95, 19);
            this.txtBleAddr.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(147, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "設定保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(241, 47);
            this.txtMsg.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(166, 19);
            this.txtMsg.TabIndex = 13;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::KeCardWin.Properties.Resources.baseline_add_black_24dp1;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAdd.Location = new System.Drawing.Point(6, 6);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(28, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(415, 447);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBleAddr);
            this.Controls.Add(this.barTransfer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnAdd);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "FormEvents";
            this.Text = "イベント編集";
            this.Load += new System.EventHandler(this.FormEvents_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar barTransfer;
        public System.Windows.Forms.TextBox txtBleAddr;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtMsg;
    }
}