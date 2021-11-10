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
    public partial class CtrlVoiceEvent : UserControl
    {

        public ApEventVoice apEventVoice = new ApEventVoice();
        private string imageFilePath = "";

        // スクリーン用フォーム
        FormScreen formScreen;


        public CtrlVoiceEvent()
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

        private void btnRange_Click(object sender, EventArgs e)
        {
            if (formScreen.Visible == false)
            {
                btnRange.Text = "キャンセル";

                Bitmap bitmap = ScCap.Capture(FormMain.mFormMain);
                FormMain.mFormMain.TopMost = true;
                formScreen.SetScreenImage(bitmap);
                formScreen.Show();

                // AppSetting.SaveBitmap(bitmap, "Screen.bmp");
            }
            else
            {
                btnRange.Text = "範囲選択";

                formScreen.Hide();
            }

        }

        private void CtrlVoiceEvent_Load(object sender, EventArgs e)
        {
            formScreen = new FormScreen();
            formScreen.selectedScreen += FormScreen_SelectedScreen;

            foreach (string label in ApEventVoice.VOICE_EVENT_TYPE_STR)
            {
                cmbSubEventType.Items.Add(label);
            }
            cmbSubEventType.SelectedIndex = (int)apEventVoice.subEventType;

            // 選択画像の読み込み
            imageFilePath = apEventVoice.imagePath;
            Bitmap bmp = ImgLib.LoadImage(imageFilePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }


            txtKeyword.Text = apEventVoice.keyword;

            // X,Y,W,H
            txtX.Text = apEventVoice.rect.X.ToString();
            txtY.Text = apEventVoice.rect.Y.ToString();
            txtW.Text = apEventVoice.rect.Width.ToString();
            txtH.Text = apEventVoice.rect.Height.ToString();


        }

        public void UpdateApEvent()
        {
            apEventVoice.imagePath = imageFilePath;
            apEventVoice.keyword = txtKeyword.Text;

            int x = 0, y = 0, w = 100, h = 100;
            int.TryParse(txtX.Text, out x);
            int.TryParse(txtY.Text, out y);
            int.TryParse(txtW.Text, out w);
            int.TryParse(txtH.Text, out h);

            apEventVoice.rect.X = x;
            apEventVoice.rect.Y = y;
            apEventVoice.rect.Width = w;
            apEventVoice.rect.Height = h;

        }

        private void cmbSubEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 <= cmbSubEventType.SelectedIndex
                && cmbSubEventType.SelectedIndex < (int)ApEventVoice.VOICE_EVENT_TYPE.MAX)
            {
                apEventVoice.subEventType = cmbSubEventType.SelectedIndex;
            }

            UpdateCtrls();

        }

        private void UpdateCtrls()
        {
            bool isTypeImage = cmbSubEventType.SelectedIndex == (int)ApEventVoice.VOICE_EVENT_TYPE.IMAGE;

            btnSelect.Enabled = isTypeImage;
            btnRange.Enabled = !isTypeImage;
            txtX.Enabled = !isTypeImage;
            txtY.Enabled = !isTypeImage;
            txtW.Enabled = !isTypeImage;
            txtH.Enabled = !isTypeImage;
            lblX.Enabled = !isTypeImage;
            lblY.Enabled = !isTypeImage;
            lblW.Enabled = !isTypeImage;
            lblH.Enabled = !isTypeImage;

        }


        // スクリーンが選択された
        private void FormScreen_SelectedScreen(Bitmap scBitmap)
        {
            formScreen.Hide();
            btnRange.Text = "範囲選択";

            // SetKeImage(scBitmap);

            Rectangle rect = formScreen.selectedRect;
            txtX.Text = rect.X.ToString();
            txtY.Text = rect.Y.ToString();
            txtW.Text = rect.Width.ToString();
            txtH.Text = rect.Height.ToString();

            picImage.Image = KeImage.FitImageSize(scBitmap);
        }


    }
}
