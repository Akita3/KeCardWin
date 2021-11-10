
namespace KeCardWin
{
    partial class FormNotification
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
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnSwitchImage = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(441, 292);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(166, 60);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "(E)CARDのボタンが押されました！";
            // 
            // picImage
            // 
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picImage.Image = global::KeCardWin.Properties.Resources.SAMPLE_IMAGE;
            this.picImage.Location = new System.Drawing.Point(12, 61);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(264, 176);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 21;
            this.picImage.TabStop = false;
            // 
            // btnSwitchImage
            // 
            this.btnSwitchImage.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSwitchImage.Location = new System.Drawing.Point(282, 80);
            this.btnSwitchImage.Name = "btnSwitchImage";
            this.btnSwitchImage.Size = new System.Drawing.Size(229, 60);
            this.btnSwitchImage.TabIndex = 22;
            this.btnSwitchImage.Text = "表示を切り替える";
            this.btnSwitchImage.UseVisualStyleBackColor = true;
            this.btnSwitchImage.Click += new System.EventHandler(this.btnSwitchImage_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(12, 240);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(540, 24);
            this.lblMsg.TabIndex = 23;
            this.lblMsg.Text = "(E)CARDのイメージが切り替わるまで少しお待ちください。";
            this.lblMsg.Visible = false;
            // 
            // FormNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 364);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnSwitchImage);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Name = "FormNotification";
            this.Text = "通知";
            this.Load += new System.EventHandler(this.FormNotification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnSwitchImage;
        private System.Windows.Forms.Label lblMsg;
    }
}