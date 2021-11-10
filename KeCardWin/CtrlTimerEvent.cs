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
    public partial class CtrlTimerEvent : UserControl
    {

        public ApEventTimer apEventTimer = new ApEventTimer();
        private string imageFilePath = "";

        public CtrlTimerEvent()
        {
            InitializeComponent();
        }

        private void CtrlTimerEvent_Load(object sender, EventArgs e)
        {
            foreach( string label in ApEventTimer.TIMER_EVENT_TYPE_STR)
            {
                cmbSubEventType.Items.Add(label);
            }
            cmbSubEventType.SelectedIndex = (int)apEventTimer.subEventType;

            // 画像No
            txtImageNo.Text = apEventTimer.imageNo.ToString();

            // 選択画像の読み込み
            imageFilePath = apEventTimer.imagePath;
            Bitmap bmp = ImgLib.LoadImage(imageFilePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }

            UpdateCtrls();
        }

        private void UpdateCtrls()
        {
            switch (apEventTimer.subEventType)
            {
                case (int)ApEventTimer.TIMER_EVENT_TYPE.DATE_TIME:
                    pkrDate.Visible = true;
                    pkrTime.Visible = true;
                    pkrDate.Value = apEventTimer.dateTime;
                    pkrTime.Value = apEventTimer.dateTime;

                    break;
                case (int)ApEventTimer.TIMER_EVENT_TYPE.TIMER:
                    pkrDate.Visible = false;
                    pkrTime.Visible = true;
                    int sec = apEventTimer.timeout % 60;
                    int min = (apEventTimer.timeout / 60) % 60;
                    int hour = (apEventTimer.timeout / 60 * 60) % 24;

                    pkrTime.Value = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        hour,
                        min,
                        sec,
                        DateTimeKind.Local
                        );
                    break;
            }
        }

        private void cmbSubEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( 0 <= cmbSubEventType.SelectedIndex
                && cmbSubEventType.SelectedIndex < (int)ApEventTimer.TIMER_EVENT_TYPE.MAX )
            {
                apEventTimer.subEventType = cmbSubEventType.SelectedIndex;
            }

            UpdateCtrls();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = FormEvents.OpenImageFileDialog();

            if( dlg.ShowDialog() == DialogResult.OK ) { }
            //ダイアログを表示する

            //OKボタンがクリックされたとき、選択されたファイル名を表示する
            imageFilePath = dlg.FileName;
            Console.WriteLine(imageFilePath);

            Bitmap bmp = ImgLib.LoadImage(imageFilePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }

        }

        private void pkrDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void pkrTime_ValueChanged(object sender, EventArgs e)
        {
        }

        public void UpdateApEvent()
        {
            switch (apEventTimer.subEventType)
            {
                case (int)ApEventTimer.TIMER_EVENT_TYPE.DATE_TIME:
                    apEventTimer.dateTime = new DateTime(
                        pkrDate.Value.Year,
                        pkrDate.Value.Month,
                        pkrDate.Value.Day,
                        pkrTime.Value.Hour,
                        pkrTime.Value.Minute,
                        pkrTime.Value.Second,
                        DateTimeKind.Local
                        );
                    break;
                case (int)ApEventTimer.TIMER_EVENT_TYPE.TIMER:
                    apEventTimer.timeout = pkrTime.Value.Hour * 60 * 60 + pkrTime.Value.Minute * 60 + pkrTime.Value.Second;
                    break;
            }

            apEventTimer.imagePath = imageFilePath;
        }

    }
}
