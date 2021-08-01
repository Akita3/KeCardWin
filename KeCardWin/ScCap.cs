using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace KeCardWin
{
    // クラス：ScCap (スクリーンキャプチャ用クラス)
    public class ScCap
    {

        // キャプチャー
        static public Bitmap Capture(FormMain form)
        {
            try
            {
                // スクリーンを判別
                // Screen screen = Screen.FromControl(form);
                Screen screen = Screen.PrimaryScreen;

                // 範囲
                Rectangle rect = screen.Bounds;

                // プライマリスクリーン全体
                Bitmap bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);

                Graphics graphics = Graphics.FromImage(bitmap);
                // 画面全体をコピーする
                graphics.CopyFromScreen(rect.X, rect.Y, 0,0, bitmap.Size);
                // グラフィックスの解放
                graphics.Dispose();

                return bitmap;
            }
            catch{ }

            return null;
        }


    }
}
