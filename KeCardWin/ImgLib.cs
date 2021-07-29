using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KeCardWin
{
    // クラス：ImgLib (イメージ関連ライブラリ)
    public class ImgLib
    {

        // 新しいビットマップ作成
        static public Bitmap NewBitmap(int width , int height)
        {
            try
            {
                Bitmap bitmap = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(bitmap);

                g.Clear(Color.White);

                g.Dispose();

                return bitmap;
            }
            catch { }

            return null;
        }

        // カラー画像→グレースケール変換
        static public Bitmap ColorImageToGrayScale(Bitmap colorBmp)
        {
            Bitmap grayBmp = new Bitmap(colorBmp.Width, colorBmp.Height);

            // ロックビット
            BitmapData colorData = colorBmp.LockBits(
                new Rectangle(0, 0, colorBmp.Width, colorBmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            BitmapData grayData = grayBmp.LockBits(
                new Rectangle(0, 0, grayBmp.Width, grayBmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // バッファ準備
            byte[] colorBuf = new byte[colorBmp.Width * colorBmp.Height * 4];
            byte[] grayBuf = new byte[colorBmp.Width * colorBmp.Height * 4];
            Marshal.Copy(colorData.Scan0, colorBuf, 0, colorBuf.Length);
            Marshal.Copy(grayData.Scan0, grayBuf, 0, grayBuf.Length);

            // グレースケール化
            for (int i = 0; i < colorBuf.Length;)
            {
                byte grey = (byte)(0.299 * colorBuf[i] + 0.587 * colorBuf[i + 1] + 0.114 * colorBuf[i + 2]);
                grayBuf[i++] = grey;
                grayBuf[i++] = grey;
                grayBuf[i++] = grey;
                grayBuf[i++] = 255;
            }

            // バッファをビットマップへコピー
            Marshal.Copy(grayBuf, 0, grayData.Scan0, grayBuf.Length);

            // アンロック
            colorBmp.UnlockBits(colorData);
            grayBmp.UnlockBits(grayData);


            /*
            for (int y = 0; y < colorBmp.Height; y++)
            {
                for (int x = 0; x < colorBmp.Width; x++)
                {
                    try
                    {
                        Color pixelColor = colorBmp.GetPixel(x, y);
                        byte grayValue = Convert.ToByte((pixelColor.R + pixelColor.G + pixelColor.B) / 3);

                        Color grayColor = Color.FromArgb(pixelColor.A, grayValue, grayValue, grayValue);
                        grayBmp.SetPixel(x, y, grayColor);
                    }
                    catch
                    {
                    }
                }
            }
            */
            return grayBmp;
        }

        // ビットマップリサイズ
        static public Bitmap ResizeBitmap(Bitmap srcBmp , int dstWidth , int dstHeight)
        {
            try
            {
                Bitmap dstBmp = new Bitmap(dstWidth, dstHeight);
                Graphics g = Graphics.FromImage(dstBmp);

                g.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(srcBmp, 0, 0, dstWidth, dstHeight);

                g.Dispose();

                return dstBmp;
            } catch { }
            return null;
        }

        // ビットマップコピー (例外スルー)
        static public Bitmap CopyBitmap(Bitmap srcBmp , int x , int y , int w , int h)
        {
            try
            {
                Bitmap dstBmp = new Bitmap(w, h);
                Graphics g = Graphics.FromImage(dstBmp);

                g.DrawImage(srcBmp, 0, 0, new Rectangle(x, y, w, h), GraphicsUnit.Pixel);

                g.Dispose();

                return dstBmp;
            }
            catch { }

            return null;
        }

        // ビットマップ描画
        static public bool DrawBitmap(Bitmap dstBmp , Bitmap srcBmp, int x, int y)
        {
            try
            {
                Graphics g = Graphics.FromImage(dstBmp);

                g.DrawImage(srcBmp, x, y, new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), GraphicsUnit.Pixel);

                g.Dispose();

                return true;
            }
            catch { }

            return false;
        }

        // 色反転
        static public Bitmap Invert(Bitmap srcBmp)
        {
            Bitmap dstBmp = (Bitmap)srcBmp.Clone();

            // ロックビット
            BitmapData dstData = dstBmp.LockBits(
                new Rectangle(0, 0, dstBmp.Width, dstBmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // バッファ準備
            byte[] dstBuf = new byte[srcBmp.Width * srcBmp.Height * 4];
            Marshal.Copy(dstData.Scan0, dstBuf, 0, dstBuf.Length);

            // 反転
            for (int i = 0; i < dstBuf.Length; i += 4)
            {
                int r = 255 - dstBuf[i];
                if (r < 0) r = 0;
                else if (255 < r) r = 255;

                int g = 255 - dstBuf[i+1];
                if (g < 0) g = 0;
                else if (255 < g) g = 255;

                int b = 255 - dstBuf[i+2];
                if (b < 0) b = 0;
                else if (255 < b) b = 255;

                dstBuf[i] = (byte)r;
                dstBuf[i+1] = (byte)g;
                dstBuf[i+2] = (byte)b;
            }

            // バッファをビットマップへコピー
            Marshal.Copy(dstBuf, 0, dstData.Scan0, dstBuf.Length);

            // アンロック
            dstBmp.UnlockBits(dstData);

            return dstBmp;
        }

        // 色加算
        static public Bitmap Add(Bitmap srcBmp , int value)
        {
            Bitmap dstBmp = (Bitmap)srcBmp.Clone();

            // ロックビット
            BitmapData dstData = dstBmp.LockBits(
                new Rectangle(0, 0, dstBmp.Width, dstBmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // バッファ準備
            byte[] dstBuf = new byte[srcBmp.Width * srcBmp.Height * 4];
            Marshal.Copy(dstData.Scan0, dstBuf, 0, dstBuf.Length);

            // 足す
            for (int i = 0; i < dstBuf.Length; i += 4)
            {
                int r = dstBuf[i] + value;
                if (r < 0) r = 0;
                else if (255 < r) r = 255;

                int g = dstBuf[i + 1] + value;
                if (g < 0) g = 0;
                else if (255 < g) g = 255;

                int b = dstBuf[i + 2] + value;
                if (b < 0) b = 0;
                else if (255 < b) b = 255;

                dstBuf[i] = (byte)r;
                dstBuf[i + 1] = (byte)g;
                dstBuf[i + 2] = (byte)b;
            }

            // バッファをビットマップへコピー
            Marshal.Copy(dstBuf, 0, dstData.Scan0, dstBuf.Length);

            // アンロック
            dstBmp.UnlockBits(dstData);

            return dstBmp;
        }

        // 濃くする
        static public Bitmap Darken(Bitmap srcBmp, int value)
        {
            Bitmap dstBmp = (Bitmap)srcBmp.Clone();

            // ロックビット
            BitmapData dstData = dstBmp.LockBits(
                new Rectangle(0, 0, dstBmp.Width, dstBmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // バッファ準備
            byte[] dstBuf = new byte[srcBmp.Width * srcBmp.Height * 4];
            Marshal.Copy(dstData.Scan0, dstBuf, 0, dstBuf.Length);

            // 引く
            for (int i = 0; i < dstBuf.Length; i += 4)
            {
                int r = dstBuf[i];
                int g = dstBuf[i + 1];
                int b = dstBuf[i + 2];

                int pxl = (r + g + b) / 3;
                if (pxl < 196)
                {
                    r -= value;
                    g -= value;
                    b -= value;

                    if (r < 0) r = 0;
                    else if (255 < r) r = 255;

                    if (g < 0) g = 0;
                    else if (255 < g) g = 255;

                    if (b < 0) b = 0;
                    else if (255 < b) b = 255;
                }

                dstBuf[i] = (byte)r;
                dstBuf[i + 1] = (byte)g;
                dstBuf[i + 2] = (byte)b;
            }

            // バッファをビットマップへコピー
            Marshal.Copy(dstBuf, 0, dstData.Scan0, dstBuf.Length);

            // アンロック
            dstBmp.UnlockBits(dstData);

            return dstBmp;
        }



        // 色Divide演算
        static public Bitmap divideImage(Bitmap grayImg1, Bitmap grayImg2, double scale)
        {
            int w = Math.Min(grayImg1.Width, grayImg2.Width);
            int h = Math.Min(grayImg1.Height, grayImg2.Height);

            // 出力用ビットマップ作成
            Bitmap dstBmp = new Bitmap(w, h);

            // ロックビット
            BitmapData grayData1 = grayImg1.LockBits(
                new Rectangle(0, 0, grayImg1.Width, grayImg1.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            BitmapData grayData2 = grayImg2.LockBits(
                new Rectangle(0, 0, grayImg2.Width, grayImg2.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            BitmapData dstData = dstBmp.LockBits(
                new Rectangle(0, 0, dstBmp.Width, dstBmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // バッファ準備
            byte[] grayBuf1 = new byte[grayImg1.Width * grayImg1.Height * 4];
            byte[] grayBuf2 = new byte[grayImg2.Width * grayImg2.Height * 4];
            byte[] dstBuf = new byte[dstBmp.Width * dstBmp.Height * 4];
            Marshal.Copy(grayData1.Scan0, grayBuf1, 0, grayBuf1.Length);
            Marshal.Copy(grayData2.Scan0, grayBuf2, 0, grayBuf2.Length);
            Marshal.Copy(dstData.Scan0, dstBuf, 0, dstBuf.Length);

            // グレースケール化
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // ピクセル取得
                    int grayPos1 = (y * grayImg1.Width + x) * 4;
                    int grayPos2 = (y * grayImg2.Width + x) * 4;
                    int dstPos = (y * dstBmp.Width + x) * 4;
                    int pxl1 = grayBuf1[grayPos1 + 0 /* r */ ];
                    int pxl2 = grayBuf2[grayPos2 + 0 /* r */ ]; ;

                    int val;
                    if (pxl2 == 0) val = 255;
                    else val = (int)(scale * pxl1 / pxl2);

                    if (val > 255) val = 255;

                    // ピクセル設定
                    dstBuf[dstPos + 0] = (byte)val;     // R
                    dstBuf[dstPos + 1] = (byte)val;     // G
                    dstBuf[dstPos + 2] = (byte)val;     // B
                    dstBuf[dstPos + 3] = 255;
                }
            }

            // バッファをビットマップへコピー
            Marshal.Copy(grayBuf1, 0, grayData1.Scan0, grayBuf1.Length);
            Marshal.Copy(grayBuf2, 0, grayData2.Scan0, grayBuf2.Length);
            Marshal.Copy(dstBuf, 0, dstData.Scan0, dstBuf.Length);

            // アンロック
            grayImg1.UnlockBits(grayData1);
            grayImg2.UnlockBits(grayData1);
            dstBmp.UnlockBits(dstData);



            /*
            // 2bit グレースケール化
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // ピクセル取得
                    int pxl1 = grayImg1.GetPixel(x, y).R;
                    int pxl2 = grayImg2.GetPixel(x, y).R;

                    int val;
                    if (pxl2 == 0) val = 255;
                    else val = (int)(scale * pxl1 / pxl2);

                    if (val > 255) val = 255;

                    // ピクセル設定
                    Color color = Color.FromArgb(val, val, val);
                    imgDst.SetPixel(x, y, color);
                }
            }
            */

            return dstBmp;
        }


    }


}
