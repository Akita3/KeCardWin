using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace KeCardWin
{
    // クラス：ScCap (スクリーンキャプチャ用クラス)
    public class ScCap
    {
        // キャプチャー
        static public Bitmap Capture()
        {
            try
            {
                // プライマリスクリーン全体
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                Graphics graphics = Graphics.FromImage(bitmap);
                // 画面全体をコピーする
                graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), bitmap.Size);
                // グラフィックスの解放
                graphics.Dispose();


                return bitmap;
            }
            catch{ }

            return null;
        }
    }
}
