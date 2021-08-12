
namespace KeCardWin
{
    partial class FormBookmark
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.bookmarkCtrl1 = new KeCardWin.BookmarkCtrl();
            this.bookmarkCtrl2 = new KeCardWin.BookmarkCtrl();
            this.bookmarkCtrl3 = new KeCardWin.BookmarkCtrl();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(545, 582);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(145, 47);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(394, 582);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(145, 47);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // bookmarkCtrl1
            // 
            this.bookmarkCtrl1.Location = new System.Drawing.Point(12, 12);
            this.bookmarkCtrl1.Name = "bookmarkCtrl1";
            this.bookmarkCtrl1.Size = new System.Drawing.Size(707, 184);
            this.bookmarkCtrl1.TabIndex = 5;
            // 
            // bookmarkCtrl2
            // 
            this.bookmarkCtrl2.Location = new System.Drawing.Point(12, 202);
            this.bookmarkCtrl2.Name = "bookmarkCtrl2";
            this.bookmarkCtrl2.Size = new System.Drawing.Size(707, 184);
            this.bookmarkCtrl2.TabIndex = 6;
            // 
            // bookmarkCtrl3
            // 
            this.bookmarkCtrl3.Location = new System.Drawing.Point(12, 392);
            this.bookmarkCtrl3.Name = "bookmarkCtrl3";
            this.bookmarkCtrl3.Size = new System.Drawing.Size(707, 184);
            this.bookmarkCtrl3.TabIndex = 7;
            // 
            // FormBookmark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 659);
            this.ControlBox = false;
            this.Controls.Add(this.bookmarkCtrl3);
            this.Controls.Add(this.bookmarkCtrl2);
            this.Controls.Add(this.bookmarkCtrl1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormBookmark";
            this.Text = "お気に入り";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormBookmark_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private BookmarkCtrl bookmarkCtrl1;
        private BookmarkCtrl bookmarkCtrl2;
        private BookmarkCtrl bookmarkCtrl3;
    }
}