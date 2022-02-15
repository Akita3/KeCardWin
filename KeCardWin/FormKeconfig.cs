using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeCardWin
{
    public partial class FormKeconfig : Form
    {
        public ulong bleAddrKeconfig = 0;


        public void UpdateBleAddr(ulong addr)
        {
            if (addr != 0x00)
            {
                txtBleAddr.Text = KeBle.BleAddrToString(addr);
                bleAddrKeconfig = addr;
            }
        }

        public void TimerProgress()
        {
            barTransfer.Value = (int)(KeCtrl.txProgress * 100);
        }


        public FormKeconfig()
        {
            InitializeComponent();
        }


        private void FormKeconfig_Load(object sender, EventArgs e)
        {

            // デフォルト設定
            btnDefault_Click(null, null);

        }

        private void KeConfig2CtrlParam( KeConfig keConfig )
        {
            // adv_slow
            txtAdvSlowEnable.Text = keConfig.advSlowEnable.ToString();
            txtAdvSlowInterval.Text = keConfig.advSlowInterval.ToString();
            txtAdvSlowDuration.Text = keConfig.advSlowDuration.ToString();
            // conn_slow
            txtConnSlowMinConnInterval.Text = keConfig.connSlowMinConnInterval.ToString();
            txtConnSlowMaxConnInterval.Text = keConfig.connSlowMaxConnInterval.ToString();
            txtConnSlowSlaveLatency.Text = keConfig.connSlowSlaveLatency.ToString();
            txtConnSlowConnSupTimeout.Text = keConfig.connSlowConnSupTimeout.ToString();
            // auto_discon_time_fast
            txtAutoDisconTimeFast.Text = keConfig.autoDisconTimeFast.ToString();
            // auto_discon_time_slow
            txtAutoDisconTimeSlow.Text = keConfig.autoDisconTimeSlow.ToString();
            // rssi_ivl
            txtRssiIvl.Text = keConfig.rssiIvl.ToString();
            // read_data_ivl
            txtReadDataIvl.Text = keConfig.readDataIvl.ToString();
            // images_enable
            txtImagesEnable.Text = keConfig.imagesEnable.ToString();
            // ble_tx_power
            txtBleTxPower.Text = keConfig.bleTxPower.ToString();
            // debug_print_ram
            txtDebugPrintRam.Text = keConfig.debugPrintRam.ToString();
            // check_value
            txtCheckValue.Text = keConfig.checkValue.ToString("X02");


        }


        private KeConfig CtrlParam2KeConfig()
        {
            KeConfig keConfig = new KeConfig();
            KeConfDef.SetDefaultKeConfig(ref keConfig);

            // adv_slow
            keConfig.advSlowEnable = txtAdvSlowEnable.Text == "0" ? (byte)0x00 : (byte)0x01;
            UInt16.TryParse(txtAdvSlowInterval.Text, out keConfig.advSlowInterval);
            UInt32.TryParse(txtAdvSlowDuration.Text, out keConfig.advSlowDuration);

            // conn_slow
            UInt16.TryParse(txtConnSlowMinConnInterval.Text, out keConfig.connSlowMinConnInterval);
            UInt16.TryParse(txtConnSlowMaxConnInterval.Text, out keConfig.connSlowMaxConnInterval);
            UInt16.TryParse(txtConnSlowSlaveLatency.Text, out keConfig.connSlowSlaveLatency);
            UInt16.TryParse(txtConnSlowConnSupTimeout.Text, out keConfig.connSlowConnSupTimeout);

            // auto_discon_time_fast
            UInt32.TryParse(txtAutoDisconTimeFast.Text, out keConfig.autoDisconTimeFast);

            // auto_discon_time_slow
            UInt32.TryParse(txtAutoDisconTimeSlow.Text, out keConfig.autoDisconTimeSlow);

            // rssi_ivl
            UInt16.TryParse(txtRssiIvl.Text, out keConfig.rssiIvl);

            // read_data_ivl
            UInt16.TryParse(txtReadDataIvl.Text, out keConfig.readDataIvl);

            // images_enable
            UInt32.TryParse(txtImagesEnable.Text, out keConfig.imagesEnable);

            // ble_tx_power
            sbyte.TryParse(txtBleTxPower.Text, out keConfig.bleTxPower);

            // debug_print_ram
            keConfig.debugPrintRam = txtDebugPrintRam.Text == "0" ? (byte)0 : (byte)1;

            return keConfig;

        }


        private async void btnReadKeconf_Click(object sender, EventArgs e)
        {

            var r = await KeCtrl.ReadKeConfig(bleAddrKeconfig);

            if( r.Item1 )
            {
                KeConfig2CtrlParam(r.Item2);
            }
        }

        private async void btnWriteKeconf_Click(object sender, EventArgs e)
        {

            // コントロール→KeConfig
            KeConfig keConfig = CtrlParam2KeConfig();

            // 設定をコントロールへ展開（例外が発生した場合の処理）
            KeConfig2CtrlParam(keConfig);

            // KeConfig書き込み
            bool res = await KeCtrl.WriteKeConfig(bleAddrKeconfig,keConfig);

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            // デフォルト設定
            KeConfig keConfig = new KeConfig();
            KeConfDef.SetDefaultKeConfig(ref keConfig);
            KeConfig2CtrlParam(keConfig);
        }
    }
}
