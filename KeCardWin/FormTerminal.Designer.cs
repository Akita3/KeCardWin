
namespace KeCardWin
{
    partial class FormTerminal
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.SplitContainer splitContainer1;
            this.btnRamWatch = new System.Windows.Forms.CheckBox();
            this.chkConnect = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.btnRamWatch);
            splitContainer1.Panel1.Controls.Add(this.chkConnect);
            splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            splitContainer1.Panel1.Controls.Add(this.cmbPort);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.txtLog);
            splitContainer1.Size = new System.Drawing.Size(1075, 1410);
            splitContainer1.SplitterDistance = 102;
            splitContainer1.TabIndex = 0;
            // 
            // btnRamWatch
            // 
            this.btnRamWatch.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRamWatch.AutoSize = true;
            this.btnRamWatch.Location = new System.Drawing.Point(438, 13);
            this.btnRamWatch.Name = "btnRamWatch";
            this.btnRamWatch.Size = new System.Drawing.Size(125, 34);
            this.btnRamWatch.TabIndex = 8;
            this.btnRamWatch.Text = "RamWatch";
            this.btnRamWatch.UseVisualStyleBackColor = true;
            this.btnRamWatch.CheckedChanged += new System.EventHandler(this.btnRamWatch_CheckedChanged);
            // 
            // chkConnect
            // 
            this.chkConnect.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkConnect.AutoSize = true;
            this.chkConnect.Location = new System.Drawing.Point(235, 13);
            this.chkConnect.Name = "chkConnect";
            this.chkConnect.Size = new System.Drawing.Size(68, 34);
            this.chkConnect.TabIndex = 6;
            this.chkConnect.Text = "接続";
            this.chkConnect.UseVisualStyleBackColor = true;
            this.chkConnect.CheckedChanged += new System.EventHandler(this.chkConnect_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 32);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(43, 13);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(186, 32);
            this.cmbPort.TabIndex = 5;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1075, 1304);
            this.txtLog.TabIndex = 4;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 230400;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // FormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 1410);
            this.Controls.Add(splitContainer1);
            this.Name = "FormTerminal";
            this.Text = "ターミナル";
            this.Load += new System.EventHandler(this.FormTerminal_Load);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkConnect;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.CheckBox btnRamWatch;
        private System.Windows.Forms.TextBox txtLog;
        private System.IO.Ports.SerialPort serialPort1;
    }
}