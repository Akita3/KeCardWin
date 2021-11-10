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
    public partial class CtrlPcEvent : UserControl
    {

        public ApEventPc apEventPc = new ApEventPc();
        private string imageFilePath = "";

        public CtrlPcEvent()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = FormEvents.OpenImageFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK) { }
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

        private void CtrlPcEvent_Load(object sender, EventArgs e)
        {
            foreach (string label in ApEventPc.PC_EVENT_TYPE_STR)
            {
                cmbSubEventType.Items.Add(label);
            }
            cmbSubEventType.SelectedIndex = (int)apEventPc.subEventType;

            // 画像No
            txtImageNo.Text = apEventPc.imageNo.ToString();

            // 選択画像の読み込み
            imageFilePath = apEventPc.imagePath;
            Bitmap bmp = ImgLib.LoadImage(imageFilePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }

        }

        public void UpdateApEvent()
        {
            if (0 <= cmbSubEventType.SelectedIndex &&
                cmbSubEventType.SelectedIndex < (int)ApEventPc.PC_EVENT_TYPE.MAX)
            {
                apEventPc.subEventType = cmbSubEventType.SelectedIndex;
            }
            apEventPc.imagePath = imageFilePath;

        }

    }
}
