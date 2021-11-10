
namespace KeCardWin
{
    partial class FormTest
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
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.bleTestDisconnect = new System.Windows.Forms.Button();
            this.txtTestLog = new System.Windows.Forms.TextBox();
            this.btnTestLogClear = new System.Windows.Forms.Button();
            this.btnTestTime = new System.Windows.Forms.Button();
            this.grpTestCmd = new System.Windows.Forms.GroupBox();
            this.btnCmdSlowMode = new System.Windows.Forms.Button();
            this.btnCmdFastMode = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpTimer = new System.Windows.Forms.GroupBox();
            this.txtTimerPeriod = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.grpHello = new System.Windows.Forms.GroupBox();
            this.txtHelloRssiMax = new System.Windows.Forms.TextBox();
            this.txtHelloRssiMin = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtHelloUserId = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpDisconnect = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDisconnectType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDisconConnectType = new System.Windows.Forms.ComboBox();
            this.grpConnect = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbConnectType = new System.Windows.Forms.ComboBox();
            this.btnCondSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCondHexData = new System.Windows.Forms.TextBox();
            this.chkCondEnable = new System.Windows.Forms.CheckBox();
            this.chkCondRepeat = new System.Windows.Forms.CheckBox();
            this.chkSubCond = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCondActNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCondType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCondNo = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnActSend = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.grpNotify = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNotifyMsg = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtActHexData = new System.Windows.Forms.TextBox();
            this.grpImage = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtImageNo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbActType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbActNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRcvDateTime = new System.Windows.Forms.TextBox();
            this.txtRcvMsg = new System.Windows.Forms.TextBox();
            this.btnReadKecMode = new System.Windows.Forms.Button();
            this.grpTestCmd.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpTimer.SuspendLayout();
            this.grpHello.SuspendLayout();
            this.grpDisconnect.SuspendLayout();
            this.grpConnect.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpNotify.SuspendLayout();
            this.grpImage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Location = new System.Drawing.Point(12, 12);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(172, 47);
            this.btnTestConnect.TabIndex = 0;
            this.btnTestConnect.Text = "BLE接続";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // bleTestDisconnect
            // 
            this.bleTestDisconnect.Location = new System.Drawing.Point(190, 12);
            this.bleTestDisconnect.Name = "bleTestDisconnect";
            this.bleTestDisconnect.Size = new System.Drawing.Size(172, 47);
            this.bleTestDisconnect.TabIndex = 1;
            this.bleTestDisconnect.Text = "BLE切断";
            this.bleTestDisconnect.UseVisualStyleBackColor = true;
            this.bleTestDisconnect.Click += new System.EventHandler(this.bleTestDisconnect_Click);
            // 
            // txtTestLog
            // 
            this.txtTestLog.Location = new System.Drawing.Point(1033, 12);
            this.txtTestLog.Multiline = true;
            this.txtTestLog.Name = "txtTestLog";
            this.txtTestLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestLog.Size = new System.Drawing.Size(426, 206);
            this.txtTestLog.TabIndex = 2;
            // 
            // btnTestLogClear
            // 
            this.btnTestLogClear.Location = new System.Drawing.Point(1348, 1005);
            this.btnTestLogClear.Name = "btnTestLogClear";
            this.btnTestLogClear.Size = new System.Drawing.Size(81, 42);
            this.btnTestLogClear.TabIndex = 3;
            this.btnTestLogClear.Text = "Clear";
            this.btnTestLogClear.UseVisualStyleBackColor = true;
            this.btnTestLogClear.Click += new System.EventHandler(this.btnTestLogClear_Click);
            // 
            // btnTestTime
            // 
            this.btnTestTime.Location = new System.Drawing.Point(27, 44);
            this.btnTestTime.Name = "btnTestTime";
            this.btnTestTime.Size = new System.Drawing.Size(145, 39);
            this.btnTestTime.TabIndex = 4;
            this.btnTestTime.Text = "Time";
            this.btnTestTime.UseVisualStyleBackColor = true;
            this.btnTestTime.Click += new System.EventHandler(this.btnTestTime_Click);
            // 
            // grpTestCmd
            // 
            this.grpTestCmd.Controls.Add(this.btnCmdSlowMode);
            this.grpTestCmd.Controls.Add(this.btnCmdFastMode);
            this.grpTestCmd.Controls.Add(this.btnTestTime);
            this.grpTestCmd.Location = new System.Drawing.Point(12, 78);
            this.grpTestCmd.Name = "grpTestCmd";
            this.grpTestCmd.Size = new System.Drawing.Size(540, 155);
            this.grpTestCmd.TabIndex = 5;
            this.grpTestCmd.TabStop = false;
            this.grpTestCmd.Text = "コマンド";
            // 
            // btnCmdSlowMode
            // 
            this.btnCmdSlowMode.Location = new System.Drawing.Point(178, 89);
            this.btnCmdSlowMode.Name = "btnCmdSlowMode";
            this.btnCmdSlowMode.Size = new System.Drawing.Size(145, 39);
            this.btnCmdSlowMode.TabIndex = 6;
            this.btnCmdSlowMode.Text = "SlowMode";
            this.btnCmdSlowMode.UseVisualStyleBackColor = true;
            this.btnCmdSlowMode.Click += new System.EventHandler(this.btnCmdSlowMode_Click);
            // 
            // btnCmdFastMode
            // 
            this.btnCmdFastMode.Location = new System.Drawing.Point(178, 44);
            this.btnCmdFastMode.Name = "btnCmdFastMode";
            this.btnCmdFastMode.Size = new System.Drawing.Size(145, 39);
            this.btnCmdFastMode.TabIndex = 5;
            this.btnCmdFastMode.Text = "FastMode";
            this.btnCmdFastMode.UseVisualStyleBackColor = true;
            this.btnCmdFastMode.Click += new System.EventHandler(this.btnCmdFastMode_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpTimer);
            this.groupBox1.Controls.Add(this.grpHello);
            this.groupBox1.Controls.Add(this.grpDisconnect);
            this.groupBox1.Controls.Add(this.grpConnect);
            this.groupBox1.Controls.Add(this.btnCondSend);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCondHexData);
            this.groupBox1.Controls.Add(this.chkCondEnable);
            this.groupBox1.Controls.Add(this.chkCondRepeat);
            this.groupBox1.Controls.Add(this.chkSubCond);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCondActNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbCondType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbCondNo);
            this.groupBox1.Location = new System.Drawing.Point(11, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1436, 437);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            // 
            // grpTimer
            // 
            this.grpTimer.Controls.Add(this.txtTimerPeriod);
            this.grpTimer.Controls.Add(this.label17);
            this.grpTimer.Location = new System.Drawing.Point(398, 181);
            this.grpTimer.Name = "grpTimer";
            this.grpTimer.Size = new System.Drawing.Size(387, 183);
            this.grpTimer.TabIndex = 22;
            this.grpTimer.TabStop = false;
            this.grpTimer.Text = "タイマー";
            // 
            // txtTimerPeriod
            // 
            this.txtTimerPeriod.Location = new System.Drawing.Point(130, 39);
            this.txtTimerPeriod.Name = "txtTimerPeriod";
            this.txtTimerPeriod.Size = new System.Drawing.Size(184, 31);
            this.txtTimerPeriod.TabIndex = 14;
            this.txtTimerPeriod.Text = "20";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(27, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 24);
            this.label17.TabIndex = 14;
            this.label17.Text = "周期[s]：";
            // 
            // grpHello
            // 
            this.grpHello.Controls.Add(this.txtHelloRssiMax);
            this.grpHello.Controls.Add(this.txtHelloRssiMin);
            this.grpHello.Controls.Add(this.label13);
            this.grpHello.Controls.Add(this.label11);
            this.grpHello.Controls.Add(this.txtHelloUserId);
            this.grpHello.Controls.Add(this.label12);
            this.grpHello.Location = new System.Drawing.Point(28, 181);
            this.grpHello.Name = "grpHello";
            this.grpHello.Size = new System.Drawing.Size(364, 183);
            this.grpHello.TabIndex = 18;
            this.grpHello.TabStop = false;
            this.grpHello.Text = "HELLO";
            // 
            // txtHelloRssiMax
            // 
            this.txtHelloRssiMax.Location = new System.Drawing.Point(153, 113);
            this.txtHelloRssiMax.Name = "txtHelloRssiMax";
            this.txtHelloRssiMax.Size = new System.Drawing.Size(184, 31);
            this.txtHelloRssiMax.TabIndex = 20;
            // 
            // txtHelloRssiMin
            // 
            this.txtHelloRssiMin.Location = new System.Drawing.Point(153, 76);
            this.txtHelloRssiMin.Name = "txtHelloRssiMin";
            this.txtHelloRssiMin.Size = new System.Drawing.Size(184, 31);
            this.txtHelloRssiMin.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 116);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 24);
            this.label13.TabIndex = 18;
            this.label13.Text = "RSSI(Max)：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 24);
            this.label11.TabIndex = 17;
            this.label11.Text = "RSSI(Min)：";
            // 
            // txtHelloUserId
            // 
            this.txtHelloUserId.Location = new System.Drawing.Point(153, 39);
            this.txtHelloUserId.Name = "txtHelloUserId";
            this.txtHelloUserId.Size = new System.Drawing.Size(184, 31);
            this.txtHelloUserId.TabIndex = 14;
            this.txtHelloUserId.Text = "00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(57, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 24);
            this.label12.TabIndex = 14;
            this.label12.Text = "UserID：";
            // 
            // grpDisconnect
            // 
            this.grpDisconnect.Controls.Add(this.label10);
            this.grpDisconnect.Controls.Add(this.txtDisconnectType);
            this.grpDisconnect.Controls.Add(this.label7);
            this.grpDisconnect.Controls.Add(this.label9);
            this.grpDisconnect.Controls.Add(this.cmbDisconConnectType);
            this.grpDisconnect.Location = new System.Drawing.Point(885, 30);
            this.grpDisconnect.Name = "grpDisconnect";
            this.grpDisconnect.Size = new System.Drawing.Size(484, 182);
            this.grpDisconnect.TabIndex = 18;
            this.grpDisconnect.TabStop = false;
            this.grpDisconnect.Text = "BLE切断";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(414, 24);
            this.label10.TabIndex = 21;
            this.label10.Text = "0x01:(E)CARD , 0x02:App , 0x04:Timeout";
            // 
            // txtDisconnectType
            // 
            this.txtDisconnectType.Location = new System.Drawing.Point(153, 84);
            this.txtDisconnectType.Name = "txtDisconnectType";
            this.txtDisconnectType.Size = new System.Drawing.Size(184, 31);
            this.txtDisconnectType.TabIndex = 20;
            this.txtDisconnectType.Text = "0F";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 24);
            this.label7.TabIndex = 19;
            this.label7.Text = "切断タイプ：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 24);
            this.label9.TabIndex = 17;
            this.label9.Text = "接続タイプ：";
            // 
            // cmbDisconConnectType
            // 
            this.cmbDisconConnectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisconConnectType.FormattingEnabled = true;
            this.cmbDisconConnectType.Location = new System.Drawing.Point(153, 41);
            this.cmbDisconConnectType.Name = "cmbDisconConnectType";
            this.cmbDisconConnectType.Size = new System.Drawing.Size(184, 32);
            this.cmbDisconConnectType.TabIndex = 16;
            // 
            // grpConnect
            // 
            this.grpConnect.Controls.Add(this.label8);
            this.grpConnect.Controls.Add(this.cmbConnectType);
            this.grpConnect.Location = new System.Drawing.Point(515, 30);
            this.grpConnect.Name = "grpConnect";
            this.grpConnect.Size = new System.Drawing.Size(364, 135);
            this.grpConnect.TabIndex = 13;
            this.grpConnect.TabStop = false;
            this.grpConnect.Text = "BLE接続";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "接続タイプ：";
            // 
            // cmbConnectType
            // 
            this.cmbConnectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectType.FormattingEnabled = true;
            this.cmbConnectType.Location = new System.Drawing.Point(151, 43);
            this.cmbConnectType.Name = "cmbConnectType";
            this.cmbConnectType.Size = new System.Drawing.Size(184, 32);
            this.cmbConnectType.TabIndex = 16;
            // 
            // btnCondSend
            // 
            this.btnCondSend.Location = new System.Drawing.Point(29, 377);
            this.btnCondSend.Name = "btnCondSend";
            this.btnCondSend.Size = new System.Drawing.Size(154, 41);
            this.btnCondSend.TabIndex = 12;
            this.btnCondSend.Text = "送信";
            this.btnCondSend.UseVisualStyleBackColor = true;
            this.btnCondSend.Click += new System.EventHandler(this.btnCondSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(843, 385);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "HEX：";
            // 
            // txtCondHexData
            // 
            this.txtCondHexData.Location = new System.Drawing.Point(914, 382);
            this.txtCondHexData.Name = "txtCondHexData";
            this.txtCondHexData.ReadOnly = true;
            this.txtCondHexData.Size = new System.Drawing.Size(504, 31);
            this.txtCondHexData.TabIndex = 10;
            // 
            // chkCondEnable
            // 
            this.chkCondEnable.AutoSize = true;
            this.chkCondEnable.Checked = true;
            this.chkCondEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCondEnable.Location = new System.Drawing.Point(344, 109);
            this.chkCondEnable.Name = "chkCondEnable";
            this.chkCondEnable.Size = new System.Drawing.Size(150, 28);
            this.chkCondEnable.TabIndex = 9;
            this.chkCondEnable.Text = "有効・無効";
            this.chkCondEnable.UseVisualStyleBackColor = true;
            // 
            // chkCondRepeat
            // 
            this.chkCondRepeat.AutoSize = true;
            this.chkCondRepeat.Location = new System.Drawing.Point(344, 75);
            this.chkCondRepeat.Name = "chkCondRepeat";
            this.chkCondRepeat.Size = new System.Drawing.Size(111, 28);
            this.chkCondRepeat.TabIndex = 8;
            this.chkCondRepeat.Text = "リピート";
            this.chkCondRepeat.UseVisualStyleBackColor = true;
            // 
            // chkSubCond
            // 
            this.chkSubCond.AutoSize = true;
            this.chkSubCond.Location = new System.Drawing.Point(344, 41);
            this.chkSubCond.Name = "chkSubCond";
            this.chkSubCond.Size = new System.Drawing.Size(128, 28);
            this.chkSubCond.TabIndex = 7;
            this.chkSubCond.Text = "サブ条件";
            this.chkSubCond.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "アクションNo：";
            // 
            // cmbCondActNo
            // 
            this.cmbCondActNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondActNo.FormattingEnabled = true;
            this.cmbCondActNo.Location = new System.Drawing.Point(165, 114);
            this.cmbCondActNo.Name = "cmbCondActNo";
            this.cmbCondActNo.Size = new System.Drawing.Size(146, 32);
            this.cmbCondActNo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "条件タイプ：";
            // 
            // cmbCondType
            // 
            this.cmbCondType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondType.FormattingEnabled = true;
            this.cmbCondType.Location = new System.Drawing.Point(165, 76);
            this.cmbCondType.Name = "cmbCondType";
            this.cmbCondType.Size = new System.Drawing.Size(146, 32);
            this.cmbCondType.TabIndex = 3;
            this.cmbCondType.SelectedIndexChanged += new System.EventHandler(this.cmbCondType_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "条件No：";
            // 
            // cmbCondNo
            // 
            this.cmbCondNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondNo.FormattingEnabled = true;
            this.cmbCondNo.Location = new System.Drawing.Point(165, 38);
            this.cmbCondNo.Name = "cmbCondNo";
            this.cmbCondNo.Size = new System.Drawing.Size(146, 32);
            this.cmbCondNo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnActSend);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.grpNotify);
            this.groupBox2.Controls.Add(this.txtActHexData);
            this.groupBox2.Controls.Add(this.grpImage);
            this.groupBox2.Controls.Add(this.cmbActType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbActNo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(11, 682);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1436, 283);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "アクション";
            // 
            // btnActSend
            // 
            this.btnActSend.Location = new System.Drawing.Point(28, 221);
            this.btnActSend.Name = "btnActSend";
            this.btnActSend.Size = new System.Drawing.Size(152, 41);
            this.btnActSend.TabIndex = 25;
            this.btnActSend.Text = "送信";
            this.btnActSend.UseVisualStyleBackColor = true;
            this.btnActSend.Click += new System.EventHandler(this.btnActSend_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(843, 229);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 24);
            this.label21.TabIndex = 24;
            this.label21.Text = "HEX：";
            // 
            // grpNotify
            // 
            this.grpNotify.Controls.Add(this.label23);
            this.grpNotify.Controls.Add(this.txtNotifyMsg);
            this.grpNotify.Controls.Add(this.label24);
            this.grpNotify.Location = new System.Drawing.Point(921, 30);
            this.grpNotify.Name = "grpNotify";
            this.grpNotify.Size = new System.Drawing.Size(400, 171);
            this.grpNotify.TabIndex = 18;
            this.grpNotify.TabStop = false;
            this.grpNotify.Text = "通知";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(70, 87);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(69, 24);
            this.label23.TabIndex = 15;
            this.label23.Text = "ex：41";
            // 
            // txtNotifyMsg
            // 
            this.txtNotifyMsg.Location = new System.Drawing.Point(74, 39);
            this.txtNotifyMsg.Name = "txtNotifyMsg";
            this.txtNotifyMsg.Size = new System.Drawing.Size(302, 31);
            this.txtNotifyMsg.TabIndex = 14;
            this.txtNotifyMsg.Text = "41";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 44);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(62, 24);
            this.label24.TabIndex = 14;
            this.label24.Text = "Msg：";
            // 
            // txtActHexData
            // 
            this.txtActHexData.Location = new System.Drawing.Point(914, 226);
            this.txtActHexData.Name = "txtActHexData";
            this.txtActHexData.ReadOnly = true;
            this.txtActHexData.Size = new System.Drawing.Size(516, 31);
            this.txtActHexData.TabIndex = 23;
            // 
            // grpImage
            // 
            this.grpImage.Controls.Add(this.label20);
            this.grpImage.Controls.Add(this.label19);
            this.grpImage.Controls.Add(this.label14);
            this.grpImage.Controls.Add(this.txtImageNo);
            this.grpImage.Controls.Add(this.label18);
            this.grpImage.Location = new System.Drawing.Point(515, 30);
            this.grpImage.Name = "grpImage";
            this.grpImage.Size = new System.Drawing.Size(400, 171);
            this.grpImage.TabIndex = 16;
            this.grpImage.TabStop = false;
            this.grpImage.Text = "イメージ変更";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(59, 123);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(305, 24);
            this.label20.TabIndex = 17;
            this.label20.Text = "Next valid：FC , Prev valid：FD";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(159, 99);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(205, 24);
            this.label19.TabIndex = 16;
            this.label19.Text = "Next：FE , Prev：FF ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(228, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 24);
            this.label14.TabIndex = 15;
            this.label14.Text = "ex : 00 (No1)";
            // 
            // txtImageNo
            // 
            this.txtImageNo.Location = new System.Drawing.Point(180, 41);
            this.txtImageNo.Name = "txtImageNo";
            this.txtImageNo.Size = new System.Drawing.Size(184, 31);
            this.txtImageNo.TabIndex = 14;
            this.txtImageNo.Text = "00";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(57, 42);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(123, 24);
            this.label18.TabIndex = 14;
            this.label18.Text = "イメージNo：";
            // 
            // cmbActType
            // 
            this.cmbActType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActType.FormattingEnabled = true;
            this.cmbActType.Location = new System.Drawing.Point(189, 68);
            this.cmbActType.Name = "cmbActType";
            this.cmbActType.Size = new System.Drawing.Size(192, 32);
            this.cmbActType.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 24);
            this.label6.TabIndex = 14;
            this.label6.Text = "アクションタイプ：";
            // 
            // cmbActNo
            // 
            this.cmbActNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActNo.FormattingEnabled = true;
            this.cmbActNo.Location = new System.Drawing.Point(189, 30);
            this.cmbActNo.Name = "cmbActNo";
            this.cmbActNo.Size = new System.Drawing.Size(192, 32);
            this.cmbActNo.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "アクションNo：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtRcvDateTime);
            this.groupBox3.Controls.Add(this.txtRcvMsg);
            this.groupBox3.Location = new System.Drawing.Point(558, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(391, 128);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Recieve";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 24);
            this.label16.TabIndex = 3;
            this.label16.Text = "日時：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 24);
            this.label15.TabIndex = 2;
            this.label15.Text = "Msg：";
            // 
            // txtRcvDateTime
            // 
            this.txtRcvDateTime.Location = new System.Drawing.Point(94, 78);
            this.txtRcvDateTime.Name = "txtRcvDateTime";
            this.txtRcvDateTime.ReadOnly = true;
            this.txtRcvDateTime.Size = new System.Drawing.Size(258, 31);
            this.txtRcvDateTime.TabIndex = 1;
            // 
            // txtRcvMsg
            // 
            this.txtRcvMsg.Location = new System.Drawing.Point(94, 41);
            this.txtRcvMsg.Name = "txtRcvMsg";
            this.txtRcvMsg.ReadOnly = true;
            this.txtRcvMsg.Size = new System.Drawing.Size(258, 31);
            this.txtRcvMsg.TabIndex = 0;
            // 
            // btnReadKecMode
            // 
            this.btnReadKecMode.Location = new System.Drawing.Point(534, 12);
            this.btnReadKecMode.Name = "btnReadKecMode";
            this.btnReadKecMode.Size = new System.Drawing.Size(189, 47);
            this.btnReadKecMode.TabIndex = 9;
            this.btnReadKecMode.Text = "Mode読み込み";
            this.btnReadKecMode.UseVisualStyleBackColor = true;
            this.btnReadKecMode.Click += new System.EventHandler(this.btnReadKecMode_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 1068);
            this.Controls.Add(this.btnReadKecMode);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpTestCmd);
            this.Controls.Add(this.btnTestLogClear);
            this.Controls.Add(this.txtTestLog);
            this.Controls.Add(this.bleTestDisconnect);
            this.Controls.Add(this.btnTestConnect);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.grpTestCmd.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTimer.ResumeLayout(false);
            this.grpTimer.PerformLayout();
            this.grpHello.ResumeLayout(false);
            this.grpHello.PerformLayout();
            this.grpDisconnect.ResumeLayout(false);
            this.grpDisconnect.PerformLayout();
            this.grpConnect.ResumeLayout(false);
            this.grpConnect.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpNotify.ResumeLayout(false);
            this.grpNotify.PerformLayout();
            this.grpImage.ResumeLayout(false);
            this.grpImage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.Button bleTestDisconnect;
        private System.Windows.Forms.TextBox txtTestLog;
        private System.Windows.Forms.Button btnTestLogClear;
        private System.Windows.Forms.Button btnTestTime;
        private System.Windows.Forms.GroupBox grpTestCmd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbCondNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCondEnable;
        private System.Windows.Forms.CheckBox chkCondRepeat;
        private System.Windows.Forms.CheckBox chkSubCond;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCondActNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCondType;
        private System.Windows.Forms.Button btnCondSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCondHexData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbActType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbActNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpConnect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbConnectType;
        private System.Windows.Forms.GroupBox grpDisconnect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbDisconConnectType;
        private System.Windows.Forms.GroupBox grpHello;
        private System.Windows.Forms.TextBox txtHelloRssiMax;
        private System.Windows.Forms.TextBox txtHelloRssiMin;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtHelloUserId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpTimer;
        private System.Windows.Forms.TextBox txtTimerPeriod;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox grpImage;
        private System.Windows.Forms.TextBox txtImageNo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox grpNotify;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNotifyMsg;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnActSend;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtActHexData;
        private System.Windows.Forms.Button btnCmdSlowMode;
        private System.Windows.Forms.Button btnCmdFastMode;
        private System.Windows.Forms.TextBox txtDisconnectType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRcvDateTime;
        private System.Windows.Forms.TextBox txtRcvMsg;
        private System.Windows.Forms.Button btnReadKecMode;
    }
}