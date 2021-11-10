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
    public partial class CtrlInitEvent : UserControl
    {

        public ApEventInit apEventInit = new ApEventInit();
        private string imageFilePath = "";

        public CtrlInitEvent()
        {
            InitializeComponent();
        }

        private void CtrlInitEvent_Load(object sender, EventArgs e)
        {
            foreach (string label in ApEventInit.INIT_EVENT_TYPE_STR)
            {
                cmbSubEventType.Items.Add(label);
            }
            cmbSubEventType.SelectedIndex = (int)apEventInit.subEventType;

            // 画像No
            txtImageNo.Text = apEventInit.imageNo.ToString();

            // 選択画像の読み込み
            imageFilePath = apEventInit.imagePath;
            Bitmap bmp = ImgLib.LoadImage(imageFilePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }

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

        public void UpdateApEvent()
        {
            if (0 <= cmbSubEventType.SelectedIndex &&
                cmbSubEventType.SelectedIndex < (int)ApEventInit.INIT_EVENT_TYPE.MAX)
            {
                apEventInit.subEventType = cmbSubEventType.SelectedIndex;
            }
            apEventInit.imagePath = imageFilePath;

        }


    }
}
