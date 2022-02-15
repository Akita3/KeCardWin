using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace KeCardWin
{
    public partial class FormTerminal : Form
    {

        string uartRcvBuffer = "";
        FormWatch formWatch;

        private void SetDefaultComPort()
        {
            string[] serialNames = SerialPort.GetPortNames();

            string before = "";     // 前回選択したポート

            cmbPort.Items.Clear();

            // 前回の選択ポートがあるか？
            foreach (string name in serialNames)
            {
                cmbPort.Items.Add(name);
                if (FormMain.appSetting.debugComPort == name)
                {
                    before = name;
                }
            }
            if (before != "")
            {
                // 前回選択したポートがあるなら、それを選択
                cmbPort.Text = before;
                if (FormMain.appSetting.debugComAutoConnect)
                {
                    chkConnect.Checked = true;
                }
            }
            else
            {
                if (cmbPort.Items.Count > 0)
                {
                    cmbPort.SelectedIndex = 0;
                }
            }
        }


        public FormTerminal()
        {
            InitializeComponent();
        }

        private void FormTerminal_Load(object sender, EventArgs e)
        {
            SetDefaultComPort();

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int size = serialPort1.BytesToRead;
                byte[] data = new byte[size];
                serialPort1.Read(data, 0, size);

                string txt = TextConv.Hex2Text(data);

                BeginInvoke(new Delegate_SerialDataRcv(SerialDataRcv), new Object[] { txt });
            }
            catch { }

        }


        private void SerialDataRcv(string uartText)
        {
            txtLog.AppendText(uartText);

            uartRcvBuffer += uartText;

            while (true)
            {
                int pos = uartRcvBuffer.IndexOf("\n");
                if (pos < 0) break;

                string line = uartRcvBuffer.Substring(0, pos);

                SerialDataRcvLine(line.Trim());

                uartRcvBuffer = uartRcvBuffer.Substring(pos + 1);
            }

        }

        private void SerialDataRcvLine(string line)
        {
            foreach (var k in Common.ramAll.all.Keys)
            {
                // キーがある？
                if (line.IndexOf(k) >= 0)
                {
                    string[] data = line.Split(':');
                    if (data.Length == 2)
                    {
                        if (formWatch != null && formWatch.IsDisposed == false)
                        {
                            formWatch.UpdateGridView(k, data[1].Trim(),false);
                        }
                    }

                }
            }

        }


        private delegate void Delegate_SerialDataRcv(string data);

        private void chkConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = cmbPort.Text;
                    serialPort1.Open();

                    // コントロール有効化・無効化
                    chkConnect.Text = "Disconnect";
                    cmbPort.Enabled = false;
                    btnRefresh.Enabled = false;

                    // 設定保存
                    FormMain.appSetting.debugComPort = cmbPort.SelectedItem.ToString();
                    AppSetting.SaveSettings(FormMain.appSetting, "AppSettings.xml");

                }
                catch { }
            }
            else
            {
                try
                {
                    // コントロール有効化・無効化
                    chkConnect.Text = "Connect";
                    cmbPort.Enabled = true;
                    btnRefresh.Enabled = true;

                    serialPort1.Close();
                }
                catch { }
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string[] serialNames = SerialPort.GetPortNames();

            cmbPort.Items.Clear();

            foreach (string name in serialNames)
            {
                cmbPort.Items.Add(name);
            }

        }

        private void btnRamWatch_CheckedChanged(object sender, EventArgs e)
        {
            if(formWatch == null || formWatch.IsDisposed )
            {
                formWatch = new FormWatch();
            }

            formWatch.Show();
        }
    }
}
