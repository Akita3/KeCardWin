
namespace KeCardWin
{
    partial class FormEventSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEventSelect));
            this.btnAddTimerEvent = new System.Windows.Forms.Button();
            this.btnButtonEvent = new System.Windows.Forms.Button();
            this.btnAddPcEvent = new System.Windows.Forms.Button();
            this.btnAddVoiceEvent = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddTimerEvent
            // 
            this.btnAddTimerEvent.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAddTimerEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTimerEvent.Image")));
            this.btnAddTimerEvent.Location = new System.Drawing.Point(12, 98);
            this.btnAddTimerEvent.Name = "btnAddTimerEvent";
            this.btnAddTimerEvent.Size = new System.Drawing.Size(858, 80);
            this.btnAddTimerEvent.TabIndex = 0;
            this.btnAddTimerEvent.Text = "時計・タイマー\r\n指定した時刻に(E)CARDの表示を切り替えたい場合";
            this.btnAddTimerEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddTimerEvent.UseVisualStyleBackColor = true;
            this.btnAddTimerEvent.Click += new System.EventHandler(this.btnAddTimerEvent_Click);
            // 
            // btnButtonEvent
            // 
            this.btnButtonEvent.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnButtonEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnButtonEvent.Image")));
            this.btnButtonEvent.Location = new System.Drawing.Point(12, 184);
            this.btnButtonEvent.Name = "btnButtonEvent";
            this.btnButtonEvent.Size = new System.Drawing.Size(858, 80);
            this.btnButtonEvent.TabIndex = 2;
            this.btnButtonEvent.Text = "ボタン\r\n(E)CARDのボタンが押された時に、通知を受けたい場合";
            this.btnButtonEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnButtonEvent.UseVisualStyleBackColor = true;
            this.btnButtonEvent.Click += new System.EventHandler(this.btnButtonEvent_Click);
            // 
            // btnAddPcEvent
            // 
            this.btnAddPcEvent.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAddPcEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPcEvent.Image")));
            this.btnAddPcEvent.Location = new System.Drawing.Point(12, 270);
            this.btnAddPcEvent.Name = "btnAddPcEvent";
            this.btnAddPcEvent.Size = new System.Drawing.Size(858, 80);
            this.btnAddPcEvent.TabIndex = 3;
            this.btnAddPcEvent.Text = "パソコン\r\nパソコンが起動、終了した時に(E)CARDの表示を切り替えたい場合";
            this.btnAddPcEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddPcEvent.UseVisualStyleBackColor = true;
            this.btnAddPcEvent.Click += new System.EventHandler(this.btnAddPcEvent_Click);
            // 
            // btnAddVoiceEvent
            // 
            this.btnAddVoiceEvent.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAddVoiceEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVoiceEvent.Image")));
            this.btnAddVoiceEvent.Location = new System.Drawing.Point(12, 356);
            this.btnAddVoiceEvent.Name = "btnAddVoiceEvent";
            this.btnAddVoiceEvent.Size = new System.Drawing.Size(858, 80);
            this.btnAddVoiceEvent.TabIndex = 4;
            this.btnAddVoiceEvent.Text = "音声\r\n音声で(E)CARDの表示を切り替えたい場合";
            this.btnAddVoiceEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddVoiceEvent.UseVisualStyleBackColor = true;
            this.btnAddVoiceEvent.Click += new System.EventHandler(this.btnAddVoiceEvent_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(12, 476);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(168, 57);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnInit.Image = ((System.Drawing.Image)(resources.GetObject("btnInit.Image")));
            this.btnInit.Location = new System.Drawing.Point(12, 12);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(858, 80);
            this.btnInit.TabIndex = 6;
            this.btnInit.Text = "デフォルトイメージ\r\n最初に表示されるイメージを設定したい場合";
            this.btnInit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // FormEventSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 545);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddVoiceEvent);
            this.Controls.Add(this.btnAddPcEvent);
            this.Controls.Add(this.btnButtonEvent);
            this.Controls.Add(this.btnAddTimerEvent);
            this.Name = "FormEventSelect";
            this.Text = "イベント（大項目）";
            this.Load += new System.EventHandler(this.FormEventSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddTimerEvent;
        private System.Windows.Forms.Button btnButtonEvent;
        private System.Windows.Forms.Button btnAddPcEvent;
        private System.Windows.Forms.Button btnAddVoiceEvent;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnInit;
    }
}