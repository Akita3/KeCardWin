
namespace KeCardWin
{
    partial class CtrlTimerEvent
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pkrTime = new System.Windows.Forms.DateTimePicker();
            this.pkrDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSubEventType = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImageNo = new System.Windows.Forms.TextBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pkrTime
            // 
            this.pkrTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.pkrTime.Location = new System.Drawing.Point(125, 49);
            this.pkrTime.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pkrTime.Name = "pkrTime";
            this.pkrTime.ShowUpDown = true;
            this.pkrTime.Size = new System.Drawing.Size(111, 19);
            this.pkrTime.TabIndex = 16;
            this.pkrTime.ValueChanged += new System.EventHandler(this.pkrTime_ValueChanged);
            // 
            // pkrDate
            // 
            this.pkrDate.Location = new System.Drawing.Point(125, 26);
            this.pkrDate.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pkrDate.Name = "pkrDate";
            this.pkrDate.Size = new System.Drawing.Size(111, 19);
            this.pkrDate.TabIndex = 18;
            this.pkrDate.ValueChanged += new System.EventHandler(this.pkrDate_ValueChanged);
            // 
            // cmbSubEventType
            // 
            this.cmbSubEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubEventType.FormattingEnabled = true;
            this.cmbSubEventType.Location = new System.Drawing.Point(126, 2);
            this.cmbSubEventType.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cmbSubEventType.Name = "cmbSubEventType";
            this.cmbSubEventType.Size = new System.Drawing.Size(178, 20);
            this.cmbSubEventType.TabIndex = 41;
            this.cmbSubEventType.SelectedIndexChanged += new System.EventHandler(this.cmbSubEventType_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(125, 72);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(59, 20);
            this.btnSelect.TabIndex = 42;
            this.btnSelect.Text = "画像選択";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "ImageNo:";
            // 
            // txtImageNo
            // 
            this.txtImageNo.Location = new System.Drawing.Point(309, 73);
            this.txtImageNo.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtImageNo.Name = "txtImageNo";
            this.txtImageNo.ReadOnly = true;
            this.txtImageNo.Size = new System.Drawing.Size(41, 19);
            this.txtImageNo.TabIndex = 46;
            // 
            // picImage
            // 
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picImage.Image = global::KeCardWin.Properties.Resources.SAMPLE_IMAGE;
            this.picImage.Location = new System.Drawing.Point(1, 2);
            this.picImage.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(122, 88);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 15;
            this.picImage.TabStop = false;
            // 
            // CtrlTimerEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtImageNo);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.cmbSubEventType);
            this.Controls.Add(this.pkrDate);
            this.Controls.Add(this.pkrTime);
            this.Controls.Add(this.picImage);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "CtrlTimerEvent";
            this.Size = new System.Drawing.Size(351, 96);
            this.Load += new System.EventHandler(this.CtrlTimerEvent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.DateTimePicker pkrTime;
        private System.Windows.Forms.DateTimePicker pkrDate;
        private System.Windows.Forms.ComboBox cmbSubEventType;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtImageNo;
    }
}
