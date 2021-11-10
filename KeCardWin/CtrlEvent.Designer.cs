
namespace KeCardWin
{
    partial class CtrlEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlEvent));
            this.txtEventType = new System.Windows.Forms.TextBox();
            this.picSubEvent = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnAddSub = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSubEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEventType
            // 
            this.txtEventType.Location = new System.Drawing.Point(84, 3);
            this.txtEventType.Name = "txtEventType";
            this.txtEventType.ReadOnly = true;
            this.txtEventType.Size = new System.Drawing.Size(543, 31);
            this.txtEventType.TabIndex = 26;
            // 
            // picSubEvent
            // 
            this.picSubEvent.Image = global::KeCardWin.Properties.Resources.baseline_subdirectory_arrow_right_black_24dp;
            this.picSubEvent.Location = new System.Drawing.Point(54, 7);
            this.picSubEvent.Name = "picSubEvent";
            this.picSubEvent.Size = new System.Drawing.Size(24, 24);
            this.picSubEvent.TabIndex = 31;
            this.picSubEvent.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(3, 97);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(34, 34);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(3, 57);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(34, 34);
            this.btnUp.TabIndex = 29;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(3, 177);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(34, 34);
            this.btnDown.TabIndex = 28;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnAddSub
            // 
            this.btnAddSub.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSub.Image")));
            this.btnAddSub.Location = new System.Drawing.Point(3, 137);
            this.btnAddSub.Name = "btnAddSub";
            this.btnAddSub.Size = new System.Drawing.Size(34, 34);
            this.btnAddSub.TabIndex = 27;
            this.btnAddSub.UseVisualStyleBackColor = true;
            this.btnAddSub.Click += new System.EventHandler(this.btnAddSub_Click);
            // 
            // CtrlEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picSubEvent);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnAddSub);
            this.Controls.Add(this.txtEventType);
            this.Name = "CtrlEvent";
            this.Size = new System.Drawing.Size(815, 229);
            this.Load += new System.EventHandler(this.CtrlEvent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSubEvent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtEventType;
        private System.Windows.Forms.Button btnAddSub;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.PictureBox picSubEvent;
    }
}
