
namespace KeCardWin
{
    partial class FormScreen
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
            this.SuspendLayout();
            // 
            // FormScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1055, 565);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormScreen";
            this.Text = "FormScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormScreen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormScreen_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseDown);
            this.MouseLeave += new System.EventHandler(this.FormScreen_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}