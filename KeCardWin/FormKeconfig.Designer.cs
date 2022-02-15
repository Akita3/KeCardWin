
namespace KeCardWin
{
    partial class FormKeconfig
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
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdvSlowEnable = new System.Windows.Forms.TextBox();
            this.txtAdvSlowDuration = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtAdvSlowInterval = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConnSlowConnSupTimeout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConnSlowSlaveLatency = new System.Windows.Forms.TextBox();
            this.txtConnSlowMaxConnInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConnSlowMinConnInterval = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAutoDisconTimeFast = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDebugPrintRam = new System.Windows.Forms.TextBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReadDataIvl = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBleTxPower = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCheckValue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtImagesEnable = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRssiIvl = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAutoDisconTimeSlow = new System.Windows.Forms.TextBox();
            this.btnReadKeconf = new System.Windows.Forms.Button();
            this.barTransfer = new System.Windows.Forms.ProgressBar();
            this.btnWriteKeconf = new System.Windows.Forms.Button();
            this.txtBleAddr = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox9.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label1);
            this.groupBox9.Controls.Add(this.txtAdvSlowEnable);
            this.groupBox9.Controls.Add(this.txtAdvSlowDuration);
            this.groupBox9.Controls.Add(this.label34);
            this.groupBox9.Controls.Add(this.txtAdvSlowInterval);
            this.groupBox9.Controls.Add(this.label35);
            this.groupBox9.Location = new System.Drawing.Point(7, 44);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox9.Size = new System.Drawing.Size(397, 176);
            this.groupBox9.TabIndex = 18;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "adv_slow";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "enable";
            // 
            // txtAdvSlowEnable
            // 
            this.txtAdvSlowEnable.Location = new System.Drawing.Point(254, 32);
            this.txtAdvSlowEnable.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtAdvSlowEnable.Name = "txtAdvSlowEnable";
            this.txtAdvSlowEnable.Size = new System.Drawing.Size(108, 31);
            this.txtAdvSlowEnable.TabIndex = 15;
            this.txtAdvSlowEnable.Text = "1";
            // 
            // txtAdvSlowDuration
            // 
            this.txtAdvSlowDuration.Location = new System.Drawing.Point(254, 124);
            this.txtAdvSlowDuration.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtAdvSlowDuration.Name = "txtAdvSlowDuration";
            this.txtAdvSlowDuration.Size = new System.Drawing.Size(108, 31);
            this.txtAdvSlowDuration.TabIndex = 14;
            this.txtAdvSlowDuration.Text = "7200";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(150, 84);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(82, 24);
            this.label34.TabIndex = 12;
            this.label34.Text = "interval";
            // 
            // txtAdvSlowInterval
            // 
            this.txtAdvSlowInterval.Location = new System.Drawing.Point(254, 78);
            this.txtAdvSlowInterval.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtAdvSlowInterval.Name = "txtAdvSlowInterval";
            this.txtAdvSlowInterval.Size = new System.Drawing.Size(108, 31);
            this.txtAdvSlowInterval.TabIndex = 11;
            this.txtAdvSlowInterval.Text = "3200";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(143, 130);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(90, 24);
            this.label35.TabIndex = 13;
            this.label35.Text = "duration";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtConnSlowConnSupTimeout);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtConnSlowSlaveLatency);
            this.groupBox2.Controls.Add(this.txtConnSlowMaxConnInterval);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtConnSlowMinConnInterval);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(7, 226);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox2.Size = new System.Drawing.Size(397, 232);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "conn_slow";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 178);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 24);
            this.label5.TabIndex = 18;
            this.label5.Text = "conn_sup_timeout";
            // 
            // txtConnSlowConnSupTimeout
            // 
            this.txtConnSlowConnSupTimeout.Location = new System.Drawing.Point(254, 172);
            this.txtConnSlowConnSupTimeout.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtConnSlowConnSupTimeout.Name = "txtConnSlowConnSupTimeout";
            this.txtConnSlowConnSupTimeout.Size = new System.Drawing.Size(108, 31);
            this.txtConnSlowConnSupTimeout.TabIndex = 17;
            this.txtConnSlowConnSupTimeout.Text = "3200";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 132);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "slave_latency";
            // 
            // txtConnSlowSlaveLatency
            // 
            this.txtConnSlowSlaveLatency.Location = new System.Drawing.Point(254, 126);
            this.txtConnSlowSlaveLatency.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtConnSlowSlaveLatency.Name = "txtConnSlowSlaveLatency";
            this.txtConnSlowSlaveLatency.Size = new System.Drawing.Size(108, 31);
            this.txtConnSlowSlaveLatency.TabIndex = 15;
            this.txtConnSlowSlaveLatency.Text = "0";
            // 
            // txtConnSlowMaxConnInterval
            // 
            this.txtConnSlowMaxConnInterval.Location = new System.Drawing.Point(254, 80);
            this.txtConnSlowMaxConnInterval.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtConnSlowMaxConnInterval.Name = "txtConnSlowMaxConnInterval";
            this.txtConnSlowMaxConnInterval.Size = new System.Drawing.Size(108, 31);
            this.txtConnSlowMaxConnInterval.TabIndex = 14;
            this.txtConnSlowMaxConnInterval.Text = "3200";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 24);
            this.label7.TabIndex = 12;
            this.label7.Text = "min_conn_interval";
            // 
            // txtConnSlowMinConnInterval
            // 
            this.txtConnSlowMinConnInterval.Location = new System.Drawing.Point(254, 34);
            this.txtConnSlowMinConnInterval.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtConnSlowMinConnInterval.Name = "txtConnSlowMinConnInterval";
            this.txtConnSlowMinConnInterval.Size = new System.Drawing.Size(108, 31);
            this.txtConnSlowMinConnInterval.TabIndex = 11;
            this.txtConnSlowMinConnInterval.Text = "1600";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 86);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 24);
            this.label8.TabIndex = 13;
            this.label8.Text = "max_conn_interval";
            // 
            // txtAutoDisconTimeFast
            // 
            this.txtAutoDisconTimeFast.Location = new System.Drawing.Point(739, 32);
            this.txtAutoDisconTimeFast.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtAutoDisconTimeFast.Name = "txtAutoDisconTimeFast";
            this.txtAutoDisconTimeFast.Size = new System.Drawing.Size(108, 31);
            this.txtAutoDisconTimeFast.TabIndex = 19;
            this.txtAutoDisconTimeFast.Text = "180";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(483, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(218, 24);
            this.label9.TabIndex = 19;
            this.label9.Text = "auto_discon_time_fast";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtDebugPrintRam);
            this.groupBox3.Controls.Add(this.btnDefault);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtReadDataIvl);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtBleTxPower);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtCheckValue);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtImagesEnable);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtRssiIvl);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtAutoDisconTimeSlow);
            this.groupBox3.Controls.Add(this.txtAutoDisconTimeFast);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.groupBox9);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(13, 60);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox3.Size = new System.Drawing.Size(917, 466);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "keconf";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(546, 314);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 24);
            this.label3.TabIndex = 35;
            this.label3.Text = "debug_print_ram";
            // 
            // txtDebugPrintRam
            // 
            this.txtDebugPrintRam.Location = new System.Drawing.Point(739, 308);
            this.txtDebugPrintRam.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtDebugPrintRam.Name = "txtDebugPrintRam";
            this.txtDebugPrintRam.Size = new System.Drawing.Size(108, 31);
            this.txtDebugPrintRam.TabIndex = 34;
            this.txtDebugPrintRam.Text = "0";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(741, 420);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(169, 40);
            this.btnDefault.TabIndex = 33;
            this.btnDefault.Text = "デフォルト";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(576, 176);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "read_data_ivl";
            // 
            // txtReadDataIvl
            // 
            this.txtReadDataIvl.Location = new System.Drawing.Point(739, 170);
            this.txtReadDataIvl.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtReadDataIvl.Name = "txtReadDataIvl";
            this.txtReadDataIvl.Size = new System.Drawing.Size(108, 31);
            this.txtReadDataIvl.TabIndex = 31;
            this.txtReadDataIvl.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(583, 268);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 24);
            this.label14.TabIndex = 30;
            this.label14.Text = "ble_tx_power";
            // 
            // txtBleTxPower
            // 
            this.txtBleTxPower.Location = new System.Drawing.Point(739, 262);
            this.txtBleTxPower.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtBleTxPower.Name = "txtBleTxPower";
            this.txtBleTxPower.Size = new System.Drawing.Size(108, 31);
            this.txtBleTxPower.TabIndex = 29;
            this.txtBleTxPower.Text = "-128";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(592, 360);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 24);
            this.label13.TabIndex = 28;
            this.label13.Text = "check_value";
            // 
            // txtCheckValue
            // 
            this.txtCheckValue.Location = new System.Drawing.Point(741, 354);
            this.txtCheckValue.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtCheckValue.Name = "txtCheckValue";
            this.txtCheckValue.ReadOnly = true;
            this.txtCheckValue.Size = new System.Drawing.Size(108, 31);
            this.txtCheckValue.TabIndex = 27;
            this.txtCheckValue.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(561, 222);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 24);
            this.label12.TabIndex = 26;
            this.label12.Text = "images_enable";
            // 
            // txtImagesEnable
            // 
            this.txtImagesEnable.Location = new System.Drawing.Point(739, 216);
            this.txtImagesEnable.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtImagesEnable.Name = "txtImagesEnable";
            this.txtImagesEnable.Size = new System.Drawing.Size(108, 31);
            this.txtImagesEnable.TabIndex = 25;
            this.txtImagesEnable.Text = "7";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(639, 130);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 24);
            this.label11.TabIndex = 24;
            this.label11.Text = "rssi_ivl";
            // 
            // txtRssiIvl
            // 
            this.txtRssiIvl.Location = new System.Drawing.Point(739, 124);
            this.txtRssiIvl.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtRssiIvl.Name = "txtRssiIvl";
            this.txtRssiIvl.Size = new System.Drawing.Size(108, 31);
            this.txtRssiIvl.TabIndex = 23;
            this.txtRssiIvl.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(472, 84);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(225, 24);
            this.label10.TabIndex = 22;
            this.label10.Text = "auto_discon_time_slow";
            // 
            // txtAutoDisconTimeSlow
            // 
            this.txtAutoDisconTimeSlow.Location = new System.Drawing.Point(739, 78);
            this.txtAutoDisconTimeSlow.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtAutoDisconTimeSlow.Name = "txtAutoDisconTimeSlow";
            this.txtAutoDisconTimeSlow.Size = new System.Drawing.Size(108, 31);
            this.txtAutoDisconTimeSlow.TabIndex = 21;
            this.txtAutoDisconTimeSlow.Text = "28800";
            // 
            // btnReadKeconf
            // 
            this.btnReadKeconf.Location = new System.Drawing.Point(251, 12);
            this.btnReadKeconf.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnReadKeconf.Name = "btnReadKeconf";
            this.btnReadKeconf.Size = new System.Drawing.Size(134, 40);
            this.btnReadKeconf.TabIndex = 22;
            this.btnReadKeconf.Text = "読み込み";
            this.btnReadKeconf.UseVisualStyleBackColor = true;
            this.btnReadKeconf.Click += new System.EventHandler(this.btnReadKeconf_Click);
            // 
            // barTransfer
            // 
            this.barTransfer.Location = new System.Drawing.Point(533, 22);
            this.barTransfer.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.barTransfer.Name = "barTransfer";
            this.barTransfer.Size = new System.Drawing.Size(397, 32);
            this.barTransfer.TabIndex = 23;
            // 
            // btnWriteKeconf
            // 
            this.btnWriteKeconf.Location = new System.Drawing.Point(392, 12);
            this.btnWriteKeconf.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnWriteKeconf.Name = "btnWriteKeconf";
            this.btnWriteKeconf.Size = new System.Drawing.Size(134, 40);
            this.btnWriteKeconf.TabIndex = 24;
            this.btnWriteKeconf.Text = "書き込み";
            this.btnWriteKeconf.UseVisualStyleBackColor = true;
            this.btnWriteKeconf.Click += new System.EventHandler(this.btnWriteKeconf_Click);
            // 
            // txtBleAddr
            // 
            this.txtBleAddr.Location = new System.Drawing.Point(13, 12);
            this.txtBleAddr.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtBleAddr.Name = "txtBleAddr";
            this.txtBleAddr.ReadOnly = true;
            this.txtBleAddr.Size = new System.Drawing.Size(225, 31);
            this.txtBleAddr.TabIndex = 25;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(752, 540);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(176, 40);
            this.btnOk.TabIndex = 26;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // FormKeconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 596);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBleAddr);
            this.Controls.Add(this.btnWriteKeconf);
            this.Controls.Add(this.barTransfer);
            this.Controls.Add(this.btnReadKeconf);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MaximizeBox = false;
            this.Name = "FormKeconfig";
            this.Text = "本体設定";
            this.Load += new System.EventHandler(this.FormKeconfig_Load);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txtAdvSlowDuration;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtAdvSlowInterval;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtConnSlowConnSupTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConnSlowSlaveLatency;
        private System.Windows.Forms.TextBox txtConnSlowMaxConnInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConnSlowMinConnInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAutoDisconTimeFast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAutoDisconTimeSlow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRssiIvl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtImagesEnable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCheckValue;
        private System.Windows.Forms.Button btnReadKeconf;
        private System.Windows.Forms.ProgressBar barTransfer;
        private System.Windows.Forms.Button btnWriteKeconf;
        private System.Windows.Forms.TextBox txtBleAddr;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBleTxPower;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdvSlowEnable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReadDataIvl;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDebugPrintRam;
    }
}