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
    public partial class FormEventSelect : Form
    {
        public ApEvent apEvent = null;
        public bool isSubEvent = false;

        public FormEventSelect()
        {
            InitializeComponent();
        }

        private void btnAddTimerEvent_Click(object sender, EventArgs e)
        {
            apEvent = new ApEventTimer();
            apEvent.isSubEvent = isSubEvent;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAddBleEvent_Click(object sender, EventArgs e)
        {
        }

        private void btnButtonEvent_Click(object sender, EventArgs e)
        {
            apEvent = new ApEventButton();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAddPcEvent_Click(object sender, EventArgs e)
        {
            apEvent = new ApEventPc();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAddVoiceEvent_Click(object sender, EventArgs e)
        {
            apEvent = new ApEventVoice();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormEventSelect_Load(object sender, EventArgs e)
        {
            btnInit.Enabled = !isSubEvent;
            btnButtonEvent.Enabled = !isSubEvent;
            btnAddPcEvent.Enabled = !isSubEvent;
            btnAddVoiceEvent.Enabled = !isSubEvent;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            apEvent = new ApEventInit();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
