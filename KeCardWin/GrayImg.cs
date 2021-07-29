using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace KeCardWin
{
    // クラス：GrayImg (ディザリング計算用)
    public class GrayImg
    {
        // メンバ変数
        int width;
        int height;
        int[] pixels;

        // コンストラクタ
        public GrayImg(Bitmap grayImage)
        {
            SetImage(grayImage);
        }

        // イメージセット
        public void SetImage(Bitmap grayImage)
        {
            width = grayImage.Width;
            height = grayImage.Height;
            pixels = new int[width * height];

            int pos = 0;
            // 全ピクセルにアクセス
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // ピクセル取得
                    int pxl = grayImage.GetPixel(x, y).R;
                    int v1 = pxl & 0xFF;
                    pixels[pos] = v1;
                    pos++;
                }
            }
        }

        // ピクセル値取得
        public int GetPixel(int x, int y)
        {
            if (x < 0 || width <= x) return 0;
            if (y < 0 || height <= y) return 0;

            return pixels[y * width + x];
        }

        // ピクセル値取得
        public void SetPixel(int x, int y, int value)
        {
            if (x < 0 || width <= x) return;
            if (y < 0 || height <= y) return;

            pixels[y * width + x] = value;
        }

        // イメージ取得
        public Bitmap GetImage()
        {
            Bitmap imgDst = new Bitmap(width, height);

            // 2bit グレースケール化
            int pos = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    int pxl = pixels[pos];

                    if (pxl < 0) pxl = 0;
                    if (pxl > 255) pxl = 255;

                    Color color = Color.FromArgb(pxl, pxl, pxl);

                    // ピクセル設定
                    imgDst.SetPixel(x, y, color);

                    pos++;
                }
            }

            return imgDst;
        }




    }
}
