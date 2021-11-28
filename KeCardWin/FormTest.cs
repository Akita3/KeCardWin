using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace KeCardWin
{
    public partial class FormTest : Form
    {
        public ulong testBleAddr = 0;

        public FormTest()
        {
            InitializeComponent();
        }

        // コントロール初期化
        private void CtrlInit()
        {
            // 条件No
            for( int i = 0; i < Runbook.RUNBOOK_COND_MAX; i ++ )
            {
                cmbCondNo.Items.Add(i.ToString());
            }
            cmbCondNo.SelectedIndex = 0;

            // 条件の種類
            for( int i = 0; i < (int)KeEvent.EVENT_TYPE.MAX; i ++ )
            {
                cmbCondType.Items.Add(KeEvent.EVENT_TYPE_STR[i]);
            }
            cmbCondType.SelectedIndex = (int)KeEvent.EVENT_TYPE.TIMER;

            // アクションNo
            for (int i = 0; i < Runbook.RUNBOOK_ACTION_MAX; i++)
            {
                cmbCondActNo.Items.Add(i.ToString());
                cmbActNo.Items.Add(i.ToString());
            }
            cmbCondActNo.SelectedIndex = 0;
            cmbActNo.SelectedIndex = 0;

            // BLE接続の種類
            for (int i = 0; i < (int)KeEvent.CONNECT_TYPE.MAX; i++)
            {
                cmbConnectType.Items.Add(KeEvent.CONNECT_TYPE_STR[i]);
                cmbDisconConnectType.Items.Add(KeEvent.CONNECT_TYPE_STR[i]);
            }
            cmbConnectType.SelectedIndex = (int)KeEvent.CONNECT_TYPE.BOTH;
            cmbDisconConnectType.SelectedIndex = (int)KeEvent.CONNECT_TYPE.BOTH;

            // アクションタイプ
            for (int i = 0; i < (int)Runbook.ACTION_TYPE.MAX; i++)
            {
                cmbActType.Items.Add(Runbook.ACTION_TYPE_STR[i]);
            }
            cmbActType.SelectedIndex = (int)Runbook.ACTION_TYPE.IMAGE;


        }


        // コントロール更新
        void CtrlUpdate()
        {
            GroupBox[] grpConds =
            {
                null,           // なし
                grpConnect,     // BLE接続
                grpDisconnect,  // BLE切断
                grpHello,       // Helloコマンド
                grpTimer,       // タイマー
                null,           // ボタン押下
                grpRssi,        // RSSI
            };

            for( int i = 0; i < (int)KeEvent.EVENT_TYPE.MAX; i ++ )
            {
                bool b = (i == cmbCondType.SelectedIndex);
                if (grpConds[i] != null) grpConds[i].Enabled = b;
            }

        }



        private void FormTest_Load(object sender, EventArgs e)
        {
            // コントロール初期化
            CtrlInit();

            // コントロール更新
            CtrlUpdate();

        }

        private async void TestBleSendCmd(byte[] bytes)
        {
            bool b = await KeCtrl.keBle.SendCommand(bytes);

            string text = BitConverter.ToString(bytes).Replace("-", string.Empty);
            txtTestLog.AppendText("BLE SendCmd : " + text + "\r\n");

            string msg = b ? "SendCommand成功\r\n" : "SendCommand失敗\r\n";
            txtTestLog.AppendText(msg);

        }

        private async void btnTestConnect_Click(object sender, EventArgs e)
        {
            bool b = await KeCtrl.keBle.ConnectKeCard(testBleAddr);
            string msg = b ? "BLE接続成功\r\n" : "BLE接続失敗\r\n";
            txtTestLog.AppendText(msg);
        }

        private async void bleTestDisconnect_Click(object sender, EventArgs e)
        {
            KeCtrl.keBle.DisconnectKeCard();
            await KeCtrl.keBle.WaitDisconnect(KeCtrl.DISCONNECTED_WAIT);
        }

        private void btnTestLogClear_Click(object sender, EventArgs e)
        {
            txtTestLog.Text = "";
        }

        private async void btnTestTime_Click(object sender, EventArgs e)
        {
            byte[] cmd = KeBle.GetTimeCmdPacket();

            TestBleSendCmd(cmd);
        }

        private Runbook.CondPacket GetCondPacket()
        {
            Runbook.CondPacket cond = new Runbook.CondPacket();

            cond.common.SetAll(
                    (byte)cmbCondNo.SelectedIndex,
                    (KeEvent.EVENT_TYPE)cmbCondType.SelectedIndex,
                    (byte)cmbCondActNo.SelectedIndex,
                    chkSubCond.Checked,
                    chkCondRepeat.Checked,
                    chkCondEnable.Checked
                );

            switch((KeEvent.EVENT_TYPE)cmbCondType.SelectedIndex)
            {
                case KeEvent.EVENT_TYPE.CONNECT:
                    cond.data.connect.con_type = (byte)cmbConnectType.SelectedIndex;
                    break;
                case KeEvent.EVENT_TYPE.DISCONNECT:
                    cond.data.disconnect.con_type = (byte)cmbDisconConnectType.SelectedIndex;
                    byte.TryParse(txtDisconnectType.Text, System.Globalization.NumberStyles.HexNumber, null, out cond.data.disconnect.discon_type);
                    break;
                case KeEvent.EVENT_TYPE.HELLO:
                    ushort.TryParse(txtHelloUserId.Text, System.Globalization.NumberStyles.HexNumber, null, out cond.data.hello.user_id);
                    break;
                case KeEvent.EVENT_TYPE.TIMER:
                    int.TryParse(txtTimerPeriod.Text, out cond.data.timer.period);
                    break;
                case KeEvent.EVENT_TYPE.BUTTON:
                    break;
                case KeEvent.EVENT_TYPE.RSSI:
                    sbyte.TryParse(txtRssiMin.Text, System.Globalization.NumberStyles.Integer, null, out cond.data.rssi.rssi_min);
                    sbyte.TryParse(txtRssiMax.Text, System.Globalization.NumberStyles.Integer, null, out cond.data.rssi.rssi_max);
                    break;
            }

            return cond;
        }

        private Runbook.ActionPacket GetActionPacket()
        {
            Runbook.ActionPacket act = new Runbook.ActionPacket();

            act.common.act_no = (byte)cmbActNo.SelectedIndex;
            act.common.act_type = (Runbook.ACTION_TYPE)cmbActType.SelectedIndex;

            switch((Runbook.ACTION_TYPE)cmbActType.SelectedIndex)
            {
                case Runbook.ACTION_TYPE.IMAGE:
                    byte.TryParse(txtImageNo.Text, System.Globalization.NumberStyles.HexNumber, null, out act.data.image.image_no);
                    break;
                case Runbook.ACTION_TYPE.NOTIFY:
                    byte.TryParse(txtNotifyMsg.Text, System.Globalization.NumberStyles.HexNumber, null, out act.data.notify.msg);
                    break;
            }

            return act;
        }



        private void btnCondSend_Click(object sender, EventArgs e)
        {
            Runbook.CondPacket cond = GetCondPacket();

            byte[] packet = KeBle.GetCondCmdPacket(cond);

            string text = BitConverter.ToString(packet).Replace("-", string.Empty);

            txtCondHexData.Text = text;

            TestBleSendCmd(packet);

        }

        private void btnActSend_Click(object sender, EventArgs e)
        {
            Runbook.ActionPacket act = GetActionPacket();

            byte[] packet = KeBle.GetActCmdPacket(act);

            string text = BitConverter.ToString(packet).Replace("-", string.Empty);

            txtActHexData.Text = text;

            TestBleSendCmd(packet);
        }

        private void btnCmdFastMode_Click(object sender, EventArgs e)
        {
            TestBleSendCmd( new byte[1] { KeBle.KE_CMD_FAST_MODE } );
        }

        private void btnCmdSlowMode_Click(object sender, EventArgs e)
        {
            TestBleSendCmd(new byte[1] { KeBle.KE_CMD_SLOW_MODE });
        }

        private void cmbCondType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // コントロール更新
            CtrlUpdate();
        }


        public void BleRecieveMsg(byte[] data)
        {
            this.Invoke(new DelegateKeBleRecieveMsg(this.BleRecieveMsgSafe) , data );
        }

        public void BleRecieveMsgSafe(byte[] data)
        {
            // 生データ
            string txt = "";
            foreach( byte v in data )
            {
                txt += v.ToString("X02") + " ";
            }
            txtRcvData.Text = txt;
            txtRcvDateTime.Text = DateTime.Now.ToString();

            // タイプ別
            if( (data.Length == KeRes.RES_NOTIFY_LEN) && (data[KeRes.RES_TYPE_POS] == KeRes.RES_NOTIFY) )
            {
                txtRcvMsg.Text = data[KeRes.RES_DATA_POS].ToString("X02");
            } else if ((data.Length == KeRes.RES_RSSI_LEN) && (data[KeRes.RES_TYPE_POS] == KeRes.RES_RSSI))
            {
                sbyte val = (sbyte)data[KeRes.RES_DATA_POS];
                txtRcvRssi.Text = val.ToString();
            }

        }

        private async void btnReadKecMode_Click(object sender, EventArgs e)
        {
            bool fastMode = await KeCtrl.keBle.ReadMode();

            txtTestLog.AppendText("KecMode : " + (fastMode ? "FastMode(1)" : "SlowMode(0)") + "\r\n" );

        }

        private void btnCmdRssiOnNotify_Click(object sender, EventArgs e)
        {
            byte[] cmd = KeBle.GetRssiCmdPacket( true , true );
            TestBleSendCmd(cmd);
        }

        private void btnCmdRssiOn_Click(object sender, EventArgs e)
        {
            byte[] cmd = KeBle.GetRssiCmdPacket(true, false);
            TestBleSendCmd(cmd);
        }

        private void btnCmdRssiOff_Click(object sender, EventArgs e)
        {
            byte[] cmd = KeBle.GetRssiCmdPacket(false, false);
            TestBleSendCmd(cmd);
        }
    }
}
