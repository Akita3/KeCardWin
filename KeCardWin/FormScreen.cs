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
    // デリゲート：スクリーン選択
    public delegate void SelectedScreen(Bitmap bitmap);

    // クラス：FormScreen
    public partial class FormScreen : Form
    {
        // メンバ変数
        private bool mouseDownFlag;     // マウスダウン中フラグ
        private Point mouseDownPos;     // マウスダウン時の位置

        private Bitmap screenBitmap;    // スクリーンそのままのキャプチャ画像
        private Bitmap displayBitmap;   // ディスプレイ表示用の画像
        private Bitmap backupBitmap;    // 退避用の画像

        public Rectangle selectedRect;         // 選択範囲

        public SelectedScreen selectedScreen;   // デリゲート 選択時に通知

        const int FILTER_ADD = -32;     // 選択時に色を変えるフィルター

        // スクリーン画像設定
        public void SetScreenImage(Bitmap bitmap)
        {
            screenBitmap = (Bitmap)bitmap.Clone();
            displayBitmap = ImgLib.ColorImageToGrayScale(bitmap);
            displayBitmap = ImgLib.Add(displayBitmap , FILTER_ADD );
            selectedRect = new Rectangle(0, 0, 0, 0);
            backupBitmap = null;
        }


        // コンストラクタ
        public FormScreen()
        {
            InitializeComponent();
        }

        // Form Load (イベントハンドラ)
        private void FormScreen_Load(object sender, EventArgs e)
        {
        }

        // Mouse Down (イベントハンドラ)
        private void FormScreen_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownFlag = true;
            mouseDownPos.X = System.Windows.Forms.Cursor.Position.X;
            mouseDownPos.Y = System.Windows.Forms.Cursor.Position.Y;
        }

        // Mouse Move (イベントハンドラ)
        private void FormScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDownFlag) return;

            int mouseUpX = System.Windows.Forms.Cursor.Position.X;
            int mouseUpY = System.Windows.Forms.Cursor.Position.Y;

            int x = Math.Min(mouseDownPos.X, mouseUpX);
            int y = Math.Min(mouseDownPos.Y, mouseUpY);

            int w = Math.Abs(mouseUpX - mouseDownPos.X);
            int h = Math.Abs(mouseUpY - mouseDownPos.Y);

            Rectangle selectingRect = new Rectangle(x, y, w, h);

            // 選択範囲が不正？
            if (selectingRect.Width <= 0 || selectingRect.Height <= 0) return;


            Graphics graphics;

            // バックアップイメージを元に戻す
            if (backupBitmap != null)
            {
                graphics = Graphics.FromImage(displayBitmap);

                graphics.DrawImage(backupBitmap, selectedRect.Left, selectedRect.Top);
                graphics.Dispose();
                backupBitmap = null;
            }

            // 描画部分をバックアップ
            backupBitmap = new Bitmap(w, h);
            graphics = Graphics.FromImage(backupBitmap);
            graphics.DrawImage(displayBitmap, new Rectangle(0, 0, selectingRect.Width, selectingRect.Height), selectingRect, GraphicsUnit.Pixel);
            graphics.Dispose();


            // 選択部分を描画
            graphics = Graphics.FromImage(displayBitmap);
            graphics.DrawImage(screenBitmap, selectingRect, selectingRect, GraphicsUnit.Pixel);
            graphics.Dispose();


            Rectangle changedRect = new Rectangle(
                Math.Min(selectingRect.Left, selectedRect.Left),
                Math.Min(selectingRect.Top, selectedRect.Top),
                Math.Max(selectingRect.Right, selectedRect.Right) - Math.Min(selectingRect.Left, selectedRect.Left),
                Math.Max(selectingRect.Bottom, selectedRect.Bottom) - Math.Min(selectingRect.Top, selectedRect.Top)
                );
            Invalidate(changedRect);

            selectedRect = selectingRect;

            // graphics.DrawImage(screenBitmap, selectRect, selectRect, GraphicsUnit.Pixel);


        }

        // Mouse Up (イベントハンドラ)
        private void FormScreen_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownFlag = false;

            FormScreen_MouseMove(null, null);

            if (selectedScreen != null && selectedRect.Width > 0 && selectedRect.Height > 0)
            {

                // 描画部分をバックアップ
                Bitmap bitmap = new Bitmap(selectedRect.Width, selectedRect.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(screenBitmap, new Rectangle(0, 0, selectedRect.Width, selectedRect.Height), selectedRect, GraphicsUnit.Pixel);
                graphics.Dispose();

                selectedScreen(bitmap);
            }
        }

        // Mouse Leave (イベントハンドラ)
        private void FormScreen_MouseLeave(object sender, EventArgs e)
        {
            mouseDownFlag = false;
        }

        // Paint (イベントハンドラ)
        private void FormScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (screenBitmap != null)
            {
                g.DrawImage(displayBitmap, 
                    new Rectangle(0,0,this.Width ,this.Height ),
                    new Rectangle(0,0, displayBitmap.Width, displayBitmap.Height),
                    GraphicsUnit.Pixel
                    );
            }
        }
    }
}
