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
    public partial class CtrlButtonEvent : UserControl
    {

        public ApEventButton apEventButton = new ApEventButton();
        private string imageFilePath = "";

        public CtrlButtonEvent()
        {
            InitializeComponent();
        }

        private void CtrlButtonEvent_Load(object sender, EventArgs e)
        {
            foreach (string label in ApEventButton.BUTTON_EVENT_TYPE_STR)
            {
                cmbSubEventType.Items.Add(label);
            }
            cmbSubEventType.SelectedIndex = (int)apEventButton.subEventType;

            // 画像No
            txtImageNo.Text = apEventButton.imageNo.ToString();

            // 選択画像の読み込み
            imageFilePath = apEventButton.imagePath;
            Bitmap bmp = ImgLib.LoadImage(imageFilePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }

        }

        public void UpdateApEvent()
        {
            if (0 <= cmbSubEventType.SelectedIndex &&
                cmbSubEventType.SelectedIndex < (int)ApEventButton.BUTTON_EVENT_TYPE.MAX)
            {
                apEventButton.subEventType = cmbSubEventType.SelectedIndex;
            }

            apEventButton.imagePath = imageFilePath;
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
    }
}
