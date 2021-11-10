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
    public partial class FormNotification : Form
    {

        public ApEventButton apEventButton = new ApEventButton();


        public FormNotification()
        {
            InitializeComponent();
        }

        private async void btnSwitchImage_Click(object sender, EventArgs e)
        {
            btnSwitchImage.Enabled = false;
            lblMsg.Visible = true;

            await KeCtrl.SwitchImage((byte)apEventButton.imageNo);
        }

        private void FormNotification_Load(object sender, EventArgs e)
        {
            // 選択画像の読み込み
            Bitmap bmp = ImgLib.LoadImage(apEventButton.imagePath);
            if (bmp != null)
            {
                picImage.Image = bmp;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
