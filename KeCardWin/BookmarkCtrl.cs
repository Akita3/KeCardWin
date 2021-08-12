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
    public partial class BookmarkCtrl : UserControl
    {

        public BookmarkCtrl()
        {
            InitializeComponent();
        }

        private void BookmarkCtrl_Load(object sender, EventArgs e)
        {
            cmbMode.Items.Add("画面キャプチャ");
            cmbMode.Items.Add("ファイル");
            cmbMode.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            picImage.Image = null;
            txtX.Text = "";
            txtY.Text = "";
            txtW.Text = "";
            txtH.Text = "";
            txtKeyword.Text = "";
            cmbMode.SelectedIndex = 0;
        }

        public void SetBookmark(Bookmark b)
        {
            if(b.bitmap != null) picImage.Image = new Bitmap( b.bitmap );
            txtX.Text = b.rect.X.ToString();
            txtY.Text = b.rect.Y.ToString();
            txtW.Text = b.rect.Width.ToString();
            txtH.Text = b.rect.Height.ToString();
            txtKeyword.Text = b.keyword;
            cmbMode.SelectedIndex = (int)b.mode;
        }

        public Bookmark GetBookmark()
        {
            Bookmark b = new Bookmark();

            try
            {
                b.mode = (Bookmark.Mode)cmbMode.SelectedIndex;
                if(picImage.Image != null)
                {
                    b.bitmap = new Bitmap(picImage.Image);
                }
                b.rect.X = Convert.ToInt32(txtX.Text);
                b.rect.Y = Convert.ToInt32(txtY.Text);
                b.rect.Width = Convert.ToInt32(txtW.Text);
                b.rect.Height = Convert.ToInt32(txtH.Text);
                b.keyword = txtKeyword.Text;
            }
            catch { }

            return b;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "画像ファイル(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "画像ファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(ofd.FileName);

                Bitmap bmp = ImgLib.LoadImage(ofd.FileName);
                if (bmp != null) picImage.Image = bmp;

                txtX.Text = "0";
                txtY.Text = "0";
                txtW.Text = "0";
                txtH.Text = "0";
            }
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool capture = cmbMode.SelectedIndex == (int)Bookmark.Mode.CAPTURE;
            bool file = cmbMode.SelectedIndex == (int)Bookmark.Mode.FILE;

            btnSelect.Enabled = file;

            txtX.Enabled = capture;
            txtY.Enabled = capture;
            txtW.Enabled = capture;
            txtH.Enabled = capture;

        }
    }
}
