
namespace KeCardWin
{
    partial class CtrlVoiceEvent
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblH = new System.Windows.Forms.Label();
            this.txtH = new System.Windows.Forms.TextBox();
            this.lblW = new System.Windows.Forms.Label();
            this.txtW = new System.Windows.Forms.TextBox();
            this.lblY = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.lblX = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.cmbSubEventType = new System.Windows.Forms.ComboBox();
            this.btnRange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(273, 138);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(143, 41);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.Text = "画像選択";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(269, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 24);
            this.label6.TabIndex = 37;
            this.label6.Text = "キーワード";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(380, 41);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(274, 31);
            this.txtKeyword.TabIndex = 36;
            this.txtKeyword.Text = "たぬき";
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Location = new System.Drawing.Point(589, 134);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(30, 24);
            this.lblH.TabIndex = 35;
            this.lblH.Text = "H:";
            // 
            // txtH
            // 
            this.txtH.Location = new System.Drawing.Point(624, 131);
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(100, 31);
            this.txtH.TabIndex = 34;
            this.txtH.Text = "100";
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(589, 97);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(33, 24);
            this.lblW.TabIndex = 33;
            this.lblW.Text = "W:";
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(624, 94);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(100, 31);
            this.txtW.TabIndex = 32;
            this.txtW.Text = "100";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(441, 134);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(29, 24);
            this.lblY.TabIndex = 31;
            this.lblY.Text = "Y:";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(476, 131);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 31);
            this.txtY.TabIndex = 30;
            this.txtY.Text = "0";
            // 
            // picImage
            // 
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picImage.Image = global::KeCardWin.Properties.Resources.SAMPLE_IMAGE;
            this.picImage.Location = new System.Drawing.Point(3, 3);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(264, 176);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 29;
            this.picImage.TabStop = false;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(441, 97);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(29, 24);
            this.lblX.TabIndex = 28;
            this.lblX.Text = "X:";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(476, 94);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 31);
            this.txtX.TabIndex = 27;
            this.txtX.Text = "0";
            // 
            // cmbSubEventType
            // 
            this.cmbSubEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubEventType.FormattingEnabled = true;
            this.cmbSubEventType.Location = new System.Drawing.Point(273, 3);
            this.cmbSubEventType.Name = "cmbSubEventType";
            this.cmbSubEventType.Size = new System.Drawing.Size(381, 32);
            this.cmbSubEventType.TabIndex = 40;
            this.cmbSubEventType.SelectedIndexChanged += new System.EventHandler(this.cmbSubEventType_SelectedIndexChanged);
            // 
            // btnRange
            // 
            this.btnRange.Location = new System.Drawing.Point(273, 91);
            this.btnRange.Name = "btnRange";
            this.btnRange.Size = new System.Drawing.Size(143, 41);
            this.btnRange.TabIndex = 41;
            this.btnRange.Text = "範囲選択";
            this.btnRange.UseVisualStyleBackColor = true;
            this.btnRange.Click += new System.EventHandler(this.btnRange_Click);
            // 
            // CtrlVoiceEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRange);
            this.Controls.Add(this.cmbSubEventType);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblH);
            this.Controls.Add(this.txtH);
            this.Controls.Add(this.lblW);
            this.Controls.Add(this.txtW);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.txtX);
            this.Name = "CtrlVoiceEvent";
            this.Size = new System.Drawing.Size(760, 182);
            this.Load += new System.EventHandler(this.CtrlVoiceEvent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label lblH;
        public System.Windows.Forms.TextBox txtH;
        private System.Windows.Forms.Label lblW;
        public System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.Label lblY;
        public System.Windows.Forms.TextBox txtY;
        public System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblX;
        public System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.ComboBox cmbSubEventType;
        private System.Windows.Forms.Button btnRange;
    }
}
