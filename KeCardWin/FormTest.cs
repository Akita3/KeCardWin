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
        FormWatch formWatch;

        ReadDataInfo readDataInfo = new ReadDataInfo();

        // INFO
        Dictionary<byte, String> infoList = new Dictionary<byte, string>()
        {
            {0x01 , "keconf" },
            {0x02 , "kesys" },
            {0x03 , "keevent" },
            {0x04 , "kekey" },
            {0x10 , "procs1" },
            {0x11 , "procs2" },
        };

        // CLEAR
        Dictionary<byte, String> clearList = new Dictionary<byte, string>()
        {
            {0x01 , "Runbook" },
            {0x40 , "Reset" },
            {0x80 , "P.OFF" },
        };



        public FormTest()
        {
            InitializeComponent();
        }

        // コントロール初期化
        private void CtrlInit()
        {
            // 条件No
            for( int i = 0; i < Runbook.PROC_MAX; i ++ )
            {
                cmbCondNo.Items.Add(i.ToString());
            }
            cmbCondNo.SelectedIndex = 1;

            // 条件の種類
            for( int i = 0; i < (int)KeEvent.EVENT_TYPE.MAX; i ++ )
            {
                cmbCondType.Items.Add(KeEvent.EVENT_TYPE_STR[i]);
            }
            cmbCondType.SelectedIndex = (int)KeEvent.EVENT_TYPE.TIMER;

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

            // INFO
            foreach(var info in infoList.Keys)
            {
                cmbInfo.Items.Add(infoList[info]);
            }
            cmbInfo.SelectedIndex = 0;

            // CLEAR
            foreach (var c in clearList.Keys)
            {
                cmbClear.Items.Add(clearList[c]);
            }
            cmbClear.SelectedIndex = 0;


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

        private async Task TestBleSendCmd(byte[] bytes)
        {
            bool b = await KeCtrl.keBle.SendCommand(bytes);

            string text = BitConverter.ToString(bytes).Replace("-", string.Empty);
            txtTestLog.AppendText("BLE SendCmd : " + text + "\r\n");

            string msg = b ? "SendCommand成功\r\n" : "SendCommand失敗\r\n";
            txtTestLog.AppendText(msg);

        }

        private async void btnTestConnect_Click(object sender, EventArgs e)
        {
            if(KeCtrl.keBle.hasConnected)
            {
                txtTestLog.AppendText("既にBLE接続中です。");
                return;
            }
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

        private RunbookProcedure getRunbookProcedure()
        {
            RunbookProcedure proc = new RunbookProcedure(cmbCondNo.SelectedIndex
                                                        , (KeEvent.EVENT_TYPE)cmbCondType.SelectedIndex
                                                        , KeSys.KESYS_STATE_ALL
                                                        , (Runbook.ACTION_TYPE)cmbActType.SelectedIndex
                                                        , chkSubCond.Checked
                                                        , chkCondRepeat.Checked
                                                        , chkCondEnable.Checked);

            switch ((KeEvent.EVENT_TYPE)cmbCondType.SelectedIndex)
            {
                case KeEvent.EVENT_TYPE.CONNECT:
                    proc.setConnectCond( (KeEvent.CONNECT_TYPE)cmbConnectType.SelectedIndex);
                    break;
                case KeEvent.EVENT_TYPE.DISCONNECT:
                    int disconType = 0;
                    int.TryParse(txtDisconnectType.Text, System.Globalization.NumberStyles.HexNumber, null, out disconType);
                    proc.setDisconnectCond((KeEvent.CONNECT_TYPE)cmbDisconConnectType.SelectedIndex
                                           , (KeEvent.DISCONNECT_TYPE)disconType);
                    break;
                case KeEvent.EVENT_TYPE.HELLO:
                    int userId;
                    int.TryParse(txtHelloUserId.Text, System.Globalization.NumberStyles.HexNumber, null, out userId);
                    proc.setHelloCond(userId);
                    break;
                case KeEvent.EVENT_TYPE.TIMER:
                    int period;
                    int.TryParse(txtTimerPeriod.Text, out period);
                    proc.setTimerCond(period);
                    break;
                case KeEvent.EVENT_TYPE.BUTTON:
                    proc.setButtonCond(KeEvent.BUTTON_PUSHED.SHORT);
                    break;
                case KeEvent.EVENT_TYPE.RSSI:
                    int rssiMin, rssiMax;
                    int.TryParse(txtRssiMin.Text, System.Globalization.NumberStyles.Integer, null, out rssiMin);
                    int.TryParse(txtRssiMax.Text, System.Globalization.NumberStyles.Integer, null, out rssiMax);
                    proc.setRssiCond(rssiMin, rssiMax);
                    break;
            }

            switch ((Runbook.ACTION_TYPE)cmbActType.SelectedIndex)
            {
                case Runbook.ACTION_TYPE.IMAGE:
                    int imageNo;
                    int.TryParse(txtImageNo.Text, System.Globalization.NumberStyles.HexNumber, null, out imageNo);
                    proc.setImageAction(imageNo);
                    break;
                case Runbook.ACTION_TYPE.NOTIFY:
                    int msg;
                    int.TryParse(txtNotifyMsg.Text, System.Globalization.NumberStyles.HexNumber, null, out msg);
                    proc.setNotifyAction(msg);
                    break;
            }

            return proc;

        }


        private void btnCondSend_Click(object sender, EventArgs e)
        {
            RunbookProcedure proc = getRunbookProcedure();

            byte[] packet = KeBle.getProcedurePacket(proc);

            string text = BitConverter.ToString(packet).Replace("-", string.Empty);

            txtCondHexData.Text = text;

            TestBleSendCmd(packet);

        }

        private void btnCmdFastMode_Click(object sender, EventArgs e)
        {
            byte[] packet = KeBle.GetModeChangeCmdPacket(true);

            TestBleSendCmd(packet);
        }

        private void btnCmdSlowMode_Click(object sender, EventArgs e)
        {
            byte[] packet = KeBle.GetModeChangeCmdPacket(false);

            TestBleSendCmd(packet);
        }

        private void cmbCondType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // コントロール更新
            CtrlUpdate();
        }


        public void BleRecieveData(byte[] data)
        {
            this.Invoke(new DelegateKeBleRecieveData(this.BleRecieveMsgSafe) , data );
        }

        public void BleRecieveMsgSafe(byte[] data)
        {
            // データサイズがゼロ
            if (data.Length == 0) return;

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
                txtRcvMsg.Text = data[KeRes.RES_NOTIFY_DATA_POS].ToString("X02");
            } else if ((data.Length == KeRes.RES_RSSI_LEN) && (data[KeRes.RES_TYPE_POS] == KeRes.RES_RSSI))
            {
                sbyte val = (sbyte)data[KeRes.RES_RSSI_DATA_POS];
                txtRcvRssi.Text = val.ToString();
            } else if ( data[KeRes.RES_TYPE_POS] == KeRes.RES_INFO )
            {
                byte key = data[KeRes.RES_INFO_TYPE_POS];
                string strData = (txt.Replace(" ", "")).Substring(KeRes.RES_INFO_DATA_POS * 2);
                if ( infoList.ContainsKey(key) )
                {
                    if ( (formWatch != null) && (formWatch.IsDisposed == false) )
                    {
                        formWatch.UpdateGridView(infoList[key], strData,true);
                    }
                }
            }
            if ((data.Length >= KeRes.RES_DATA_HEADER_LEN) && (data[KeRes.RES_TYPE_POS] == KeRes.RES_DATA))
            {
                RecieveReadDataUpdate(data.Skip(KeRes.RES_DATA_HEADER_LEN).ToArray());
                NextLongDataRead();
            }

        }

        private async void btnReadCharAll_Click(object sender, EventArgs e)
        {
            bool fastMode = await KeCtrl.keBle.ReadMode();
            int imageNo = await KeCtrl.keBle.ReadImageNo();
            byte status = await KeCtrl.keBle.ReadStatus();

            txtCharStatus.Text = status.ToString("X02");
            txtCharImageNo.Text = imageNo.ToString();
            txtCharMode.Text = fastMode ? "FastMode(1)" : "SlowMode(0)";

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

        private void btnCmdEraseFlash_Click(object sender, EventArgs e)
        {
            TestBleSendCmd(new byte[] { Cmd.CMD_ERASE_FLASH });
        }

        private async void btnSelectImage_Click(object sender, EventArgs e)
        {

            int no = 0;
            int.TryParse(txtSelectImageNo.Text, out no);
            // 画像No選択
            await KeCtrl.keBle.SelectImage((byte)no);

        }

        private void btnCmdDisplay_Click(object sender, EventArgs e)
        {
            TestBleSendCmd(new byte[] { Cmd.CMD_DISPLAY });
        }

        private void btnCmdInfo_Click(object sender, EventArgs e)
        {
            
            var v = infoList.FirstOrDefault(c => c.Value == cmbInfo.Text);
            if (v.Value == null) return;
            
            byte[] cmd = KeBle.GetInfoCmdPacket(v.Key);
            TestBleSendCmd(cmd);

        }

        private void btnRamWatch_CheckedChanged(object sender, EventArgs e)
        {
            if (formWatch == null || formWatch.IsDisposed)
            {
                formWatch = new FormWatch();
            }

            formWatch.Show();
        }

        private void btnCmdRunbookOff_Click(object sender, EventArgs e)
        {
            byte[] cmd = KeBle.GetRunbookCmdPacket(false);
            TestBleSendCmd(cmd);
        }

        private void btnCmdRunbookOn_Click(object sender, EventArgs e)
        {
            byte[] cmd = KeBle.GetRunbookCmdPacket(true);
            TestBleSendCmd(cmd);
        }

        private void btnCmdHello_Click(object sender, EventArgs e)
        {
            ushort userId = 0x00;
            ushort.TryParse(txtCmdHelloUserId.Text, System.Globalization.NumberStyles.HexNumber,null,out userId);
            byte[] cmd = KeBle.GetHelloCmdPacket(userId);
            TestBleSendCmd(cmd);
            
        }

        private void RecieveReadDataInit()
        {
            txtRecieveReadData.Text = "";
        }

        private void RecieveReadDataUpdate(byte[] data)
        {
            string txt = "";

            UInt32 addr = readDataInfo.readAddr;
            readDataInfo.recieveSize += data.Length;

            for ( int i = 0;i < data.Length; i ++ )
            {
                if( i % 16 == 0 )
                {
                    txt += "\r\n" + addr.ToString("X05") + " ";
                    addr += 16;
                }
                txt += data[i].ToString("X02") + " ";
            }

            txtRecieveReadData.Text += txt;
            txtRecieveReadDataSize.Text = readDataInfo.recieveSize.ToString();
        }

        private void btnCmdEraseData_Click(object sender, EventArgs e)
        {
            UInt32 addr = 0;
            ushort length = 0;

            UInt32.TryParse(txtCmdEraseDataAddress.Text, System.Globalization.NumberStyles.HexNumber, null, out addr);
            ushort.TryParse(txtCmdEraseDataLength.Text, System.Globalization.NumberStyles.Number, null, out length);

            byte[] cmd = KeBle.GetEraseDataCmdPacket(addr, length);
            TestBleSendCmd(cmd);
        }

        private void btnCmdReadData_Click(object sender, EventArgs e)
        {
            // 受信情報初期化
            RecieveReadDataInit();

            UInt32 addr = 0;
            ushort length = 0;

            UInt32.TryParse(txtCmdReadDataAddress.Text, System.Globalization.NumberStyles.HexNumber, null, out addr);
            ushort.TryParse(txtCmdReadDataLength.Text, System.Globalization.NumberStyles.Number, null, out length);

            // 読み込み情報設定
            readDataInfo = new ReadDataInfo(addr, length, false);

            byte[] cmd = KeBle.GetReadDataCmdPacket(addr, length);
            TestBleSendCmd(cmd);
        }

        private void btnCmdWriteData_Click(object sender, EventArgs e)
        {
            UInt32 addr = 0;

            UInt32.TryParse(txtCmdWriteDataAddress.Text, System.Globalization.NumberStyles.HexNumber, null, out addr);
            byte[] raw = Encoding.UTF8.GetBytes(txtCmdWriteDataText.Text);

            int len = 4 * ((raw.Length / 4) + (raw.Length % 4 == 0 ? 0 : 1));
            byte[] data = new byte[len];
            Array.Copy(raw, data, raw.Length);

            byte[] cmd = KeBle.GetWriteDataCmdPacket(addr, data);
            TestBleSendCmd(cmd);
        }

        private void btnCmdLongReadData_Click(object sender, EventArgs e)
        {
            // 受信情報初期化
            RecieveReadDataInit();

            UInt32 addr = 0;
            ushort length = 0;

            // LongReadData情報取得
            UInt32.TryParse(txtCmdLongReadDataAddress.Text, System.Globalization.NumberStyles.HexNumber, null, out addr);
            ushort.TryParse(txtCmdLongReadDataLength.Text, System.Globalization.NumberStyles.Number, null, out length);

            // 読み込み情報設定
            readDataInfo = new ReadDataInfo(addr, length, true);

            byte[] cmd = KeBle.GetReadDataCmdPacket(readDataInfo.readAddr, (ushort)readDataInfo.readSize);
            TestBleSendCmd(cmd);
        }

        private void NextLongDataRead()
        {
            if (!readDataInfo.longRead) return;
            readDataInfo.Next();

            byte[] cmd = KeBle.GetReadDataCmdPacket(readDataInfo.readAddr, (ushort)readDataInfo.readSize);
            TestBleSendCmd(cmd);

        }

        private async void btnCmdLongWriteData_Click(object sender, EventArgs e)
        {
            const int DATA_MAX = 128;
            UInt32 addr = 0;
            ushort length = 0;
            byte value = 0x00;

            // LongWriteData情報取得
            UInt32.TryParse(txtCmdLongWriteDataAddress.Text, System.Globalization.NumberStyles.HexNumber, null, out addr);
            ushort.TryParse(txtCmdLongWriteDataLength.Text, System.Globalization.NumberStyles.Number, null, out length);
            byte.TryParse(txtCmdLongWriteDataValue.Text, System.Globalization.NumberStyles.HexNumber, null, out value);

            // データがなくなるまで送信
            while(length > 0)
            {
                int sz = length > DATA_MAX ? DATA_MAX : length;

                byte[] data = Enumerable.Repeat(value, sz).ToArray();

                byte[] cmd = KeBle.GetWriteDataCmdPacket(addr, data);
                await TestBleSendCmd(cmd);
                await Task.Delay(250);

                addr += (UInt32)sz;
                length -= (ushort)sz;
            }
        }

        private async void btnFreeCmd_Click(object sender, EventArgs e)
        {
            string txt = txtFreeCmd.Text;
            List<byte> data = new List<byte>();

            while( txt.Length > 0 )
            {
                string t = txt.Substring(0, 2);
                txt = txt.Substring(2);
                byte v = 0x00;
                byte.TryParse(t, System.Globalization.NumberStyles.HexNumber, null, out v);
                data.Add(v);
            }

            // コマンド送信
            await TestBleSendCmd(data.ToArray());

        }

        private void btnCmdClear_Click(object sender, EventArgs e)
        {
            var v = clearList.FirstOrDefault(c => c.Value == cmbClear.Text);
            if (v.Value == null) return;

            byte[] cmd = KeBle.GetClearCmdPacket(v.Key);
            TestBleSendCmd(cmd);

        }

        private async void btnSubCondTest1_Click(object sender, EventArgs e)
        {
            byte[] packet;
            RunbookProcedure proc;

            for ( int i = 1;i <= 3; i ++ )
            {
                proc = new RunbookProcedure( i
                                            , KeSys.KESYS_STATE_ALL
                                            , i == 1 ? false : true
                                            , true
                                            , i == 1 ? true : false );

                proc.setHelloCond(0x10);
                proc.setImageAction(i - 1);

                packet = KeBle.getProcedurePacket(proc);
                await TestBleSendCmd(packet);

            }

            // キャンセル条件
            proc = new RunbookProcedure(4
                                    , KeSys.KESYS_STATE_ALL
                                    , false
                                    , true
                                    , true);

            proc.setHelloCond(0x20);
            proc.setCancelAction(1);

            packet = KeBle.getProcedurePacket(proc);
            await TestBleSendCmd(packet);


            txtTestLog.AppendText("サブ条件テスト送信完了");
        }

        private async void btnTestCaseAttendance_Click(object sender, EventArgs e)
        {
            byte[] packet;

            // 条件1
            RunbookProcedure proc = new RunbookProcedure( 1
                                                , KeSys.KESYS_STATE_ALL
                                                , false
                                                , true
                                                , true);
            proc.setConnectCond(KeEvent.CONNECT_TYPE.BOTH);
            proc.setImageAction(0);
            packet = KeBle.getProcedurePacket(proc);
            await TestBleSendCmd(packet);


            // 条件2
            proc = new RunbookProcedure(2
                                                , KeSys.KESYS_STATE_ALL
                                                , false
                                                , true
                                                , true);
            proc.setDisconnectCond(KeEvent.CONNECT_TYPE.BOTH , KeEvent.DISCONNECT_TYPE.ALL);
            proc.setImageAction(1);
            packet = KeBle.getProcedurePacket(proc);
            await TestBleSendCmd(packet);


            // 条件3
            proc = new RunbookProcedure(3
                                                , KeSys.KESYS_STATE_ALL
                                                , true
                                                , true
                                                , false);
            proc.setTimerCond(30);
            proc.setImageAction(2);
            packet = KeBle.getProcedurePacket(proc);
            await TestBleSendCmd(packet);



            // 条件4
            proc = new RunbookProcedure(4
                                                , KeSys.KESYS_STATE_ALL
                                                , false
                                                , true
                                                , true);
            proc.setConnectCond(KeEvent.CONNECT_TYPE.BOTH);
            proc.setCancelAction(2);
            packet = KeBle.getProcedurePacket(proc);
            await TestBleSendCmd(packet);

            txtTestLog.AppendText("送信完了");
        }

        private async void btnTestIdleAdvStart_Click(object sender, EventArgs e)
        {

            byte[] packet1 , packet2 , packet3;
            RunbookProcedure proc1 = new RunbookProcedure(1
                                                , KeEvent.EVENT_TYPE.HELLO
                                                , KeSys.KESYS_STATE_ALL
                                                , Runbook.ACTION_TYPE.DISCONNECT
                                                , false
                                                , true
                                                , true);

            packet1 = KeBle.getProcedurePacket(proc1);
            await TestBleSendCmd(packet1);

            RunbookProcedure proc2 = new RunbookProcedure(2
                                                , KeEvent.EVENT_TYPE.TIMER
                                                , KeSys.KESYS_STATE_ALL
                                                , Runbook.ACTION_TYPE.IDLE
                                                , true
                                                , true
                                                , true);

            proc2.setTimerCond(30);

            packet2 = KeBle.getProcedurePacket(proc2);
            await TestBleSendCmd(packet2);


            RunbookProcedure proc3 = new RunbookProcedure(3
                                                , KeEvent.EVENT_TYPE.TIMER
                                                , KeSys.KESYS_STATE_ALL
                                                , Runbook.ACTION_TYPE.ADV_START
                                                , true
                                                , true
                                                , true);

            proc3.setTimerCond(30);

            packet3 = KeBle.getProcedurePacket(proc3);
            await TestBleSendCmd(packet3);



            txtTestLog.AppendText("Idle + AdvStart 送信完了");

        }

        private async void btnEnableDisableTest_Click(object sender, EventArgs e)
        {
            byte[] packet;
            RunbookProcedure proc;

            for (int i = 1; i <= 6; i++)
            {

                proc = new RunbookProcedure(i
                                            , KeSys.KESYS_STATE_ALL
                                            , false
                                            , true
                                            , (i == 1 || i == 2) ? false : true);

                switch( i )
                {
                    case 1:
                        proc.setHelloCond(0x01);
                        proc.setImageAction(0);
                        break;
                    case 2:
                        proc.setHelloCond(0x01);
                        proc.setImageAction(1);
                        break;
                    case 3:
                        proc.setHelloCond(0x10);
                        proc.setEnableAction(1);
                        break;
                    case 4:
                        proc.setHelloCond(0x20);
                        proc.setEnableAction(2);
                        break;
                    case 5:
                        proc.setHelloCond(0x30);
                        proc.setDisableAction(1);
                        break;
                    case 6:
                        proc.setHelloCond(0x40);
                        proc.setDisableAction(2);
                        break;
                }

                packet = KeBle.getProcedurePacket(proc);
                await TestBleSendCmd(packet);

            }

            txtTestLog.AppendText("ENABLE DISABLE テスト送信完了");

        }

        private async void btnTestAllProcedure_Click(object sender, EventArgs e)
        {

            byte[] packet;
            RunbookProcedure proc;

            for (int i = 1; i < Runbook.PROC_MAX; i++)
            {
                proc = new RunbookProcedure(i
                                            , KeSys.KESYS_STATE_ALL
                                            , true
                                            , true
                                            , false);

                proc.setTimerCond(10);
                proc.setImageAction( i % 3);

                packet = KeBle.getProcedurePacket(proc);
                await TestBleSendCmd(packet);

            }

            txtTestLog.AppendText("All Procedure テスト送信完了");
            

        }
    }


}
