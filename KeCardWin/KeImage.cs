using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    // クラス：KeImage ((E)CARDディスプレイイメージ)
    public class KeImage
    {
        // 2bitグレースケールの色の定義
        public const int COLOR_0 = 0;
        public const int COLOR_1 = 127;
        public const int COLOR_2 = 195;
        public const int COLOR_3 = 255;

        // 2bitグレースケール変換時の閾値
        public const int IMG_TH1 = 64;
        public const int IMG_TH2 = 160;
        public const int IMG_TH3 = 224;

        // イメージの幅、高さ、アスペクト比
        public const int IMG_WIDTH = 264;
        public const int IMG_HEIGHT = 176;
        public const double IMG_ASPECT = 1.0 * IMG_WIDTH / IMG_HEIGHT;

        // ビット数、総バイト数
        public const int IMG_BIT_N = 2;
        public const int IMG_BYTE_N = IMG_WIDTH * IMG_HEIGHT * IMG_BIT_N / 8;

        // 1パケットのデータ量
        public const int DATA_SIZE_LOW_SPEED = 16;
        public const int DATA_SIZE_HIGH_SPEED = 128;

        // 送信データサイズ
        public int sendDataSize = DATA_SIZE_HIGH_SPEED;

        // 送信イメージ、送信番号
        private byte[] sendingImage;
        private int sendingNo = 0;


        // イメージサイズにフィット
        static public Bitmap FitImageSize(Bitmap image , bool ZoomCenter = false)
        {

            // 既に264x176
            if (image.Width == IMG_WIDTH && image.Height == IMG_HEIGHT)
            {
                return image;
            }

            Bitmap dstImage;
            // 比率
            double aspect = 1.0 * image.Width / image.Height;

            double zoomRatio;
            // 縮小
            if (aspect > IMG_ASPECT)
            {   
                // width > height
                if(ZoomCenter)
                {
                    // 縦に合わせて横をカット
                    zoomRatio = 1.0 * IMG_HEIGHT / image.Height;
                    int w = (int)(zoomRatio * image.Width);
                    dstImage = ImgLib.ResizeBitmap(image, w, IMG_HEIGHT);
                    int offsetX = (dstImage.Width - IMG_WIDTH) / 2;

                    dstImage = ImgLib.CopyBitmap(dstImage, offsetX, 0, IMG_WIDTH, IMG_HEIGHT);
                } else
                {
                    // 横に合わせて上下に白いバーを表示
                    zoomRatio = 1.0 * IMG_WIDTH / image.Width;
                    int h = (int)(zoomRatio * image.Height);
                    var drawImage = ImgLib.ResizeBitmap(image, IMG_WIDTH, h);
                    int offsetY = (IMG_HEIGHT - drawImage.Height) / 2;

                    dstImage = ImgLib.NewBitmap(IMG_WIDTH, IMG_HEIGHT);
                    ImgLib.DrawBitmap(dstImage , drawImage, 0, offsetY);
                }
            }
            else if (aspect < IMG_ASPECT)
            {
                // height > width
                if (ZoomCenter)
                {
                    // 横に合わせて縦をカット
                    zoomRatio = 1.0 * IMG_WIDTH / image.Width;
                    int h = (int)(zoomRatio * image.Height);
                    dstImage = ImgLib.ResizeBitmap(image, IMG_WIDTH, h);
                    int offsetY = (dstImage.Height - IMG_HEIGHT) / 2;
                    dstImage = ImgLib.CopyBitmap(dstImage, 0, offsetY, IMG_WIDTH, IMG_HEIGHT);
                } else
                {
                    // 縦に合わせて左右に白いバーを表示
                    zoomRatio = 1.0 * IMG_HEIGHT / image.Height;
                    int w = (int)(zoomRatio * image.Width);
                    var drawImage = ImgLib.ResizeBitmap(image, w, IMG_HEIGHT);
                    int offsetX = (IMG_WIDTH - drawImage.Width) / 2;

                    dstImage = ImgLib.NewBitmap(IMG_WIDTH, IMG_HEIGHT);
                    ImgLib.DrawBitmap(dstImage, drawImage, offsetX, 0);
                }

            }
            else
            { // サイズが異なる
                dstImage = ImgLib.ResizeBitmap(image, IMG_WIDTH, IMG_HEIGHT);
            }

            return dstImage;
        }

        // カラー→2bitグレースケールへ変換 (通常)
        static public Bitmap ImageToGray2bitImageNormal(Bitmap image, int th1 = IMG_TH1, int th2 = IMG_TH2, int th3 = IMG_TH3)
        {
            Bitmap dstImage;

            // サイズを調整
            image = FitImageSize( image );

            // グレースケール化
            var gray = ImgLib.ColorImageToGrayScale(image);

            // 2bit グレースケール化
            for (int y = 0; y < IMG_HEIGHT; y++)
            {
                for (int x = 0; x < IMG_WIDTH; x++)
                {
                    // ピクセル取得
                    int pxl = gray.GetPixel(x, y).R;

                    int v1 = pxl & 0xFF;
                    int v2 = 0x00;
                    if (v1 < th1) { v2 = COLOR_0; }
                    else if (v1 < th2) { v2 = COLOR_1; }
                    else if (v1 < th3) { v2 = COLOR_2; }
                    else { v2 = COLOR_3; }

                    // ピクセル設定
                    Color color = Color.FromArgb(v2, v2, v2);
                    gray.SetPixel(x, y, color);
                }
            }

            dstImage = gray;

            return dstImage;
        }


        // カラー→2bitグレースケールへ変換 (ディザリング)
        static public Bitmap ImageToGray2bitImageDithering(Bitmap image, int th1 = IMG_TH1, int th2 = IMG_TH2, int th3 = IMG_TH3)
        {
            double v;

            // サイズを調整
            image = FitImageSize(image);

            // グレースケール化
            var gray = ImgLib.ColorImageToGrayScale(image);

            // ピクセルの配列を取得
            var ditherImg = new GrayImg(gray);

            // ディザリング処理
            for (int y = 0; y < IMG_HEIGHT; y++)
            {
                for (int x = 0; x < IMG_WIDTH; x++)
                {
                    // ピクセル取得
                    int pxl = ditherImg.GetPixel(x, y);

                    int newpxl;
                    if (pxl < th1) { newpxl = COLOR_0; }
                    else if (pxl < th2) { newpxl = COLOR_1; }
                    else if (pxl < th3) { newpxl = COLOR_2; }
                    else { newpxl = COLOR_3; }

                    ditherImg.SetPixel(x, y, newpxl);
                    var qerr = pxl - newpxl;

                    // x+1 , y
                    v = ditherImg.GetPixel(x + 1, y) + 7.0 / 16.0 * qerr;
                    ditherImg.SetPixel(x + 1, y, (int)v);

                    // x-1 , y+1
                    v = ditherImg.GetPixel(x - 1, y + 1) + 3.0 / 16.0 * qerr;
                    ditherImg.SetPixel(x - 1, y + 1, (int)v);

                    // x , y+1
                    v = ditherImg.GetPixel(x, y + 1) + 5.0 / 16.0 * qerr;
                    ditherImg.SetPixel(x, y + 1, (int)v);

                    // x+1 , y+1
                    v = ditherImg.GetPixel(x + 1, y + 1) + 1.0 / 16.0 * qerr;
                    ditherImg.SetPixel(x + 1, y + 1, (int)v);

                }
            }

            Bitmap dstImage = ditherImg.GetImage();

            return dstImage;
        }


        // スケッチ風に変換
        static public Bitmap NormalSketch(Bitmap srcImage, int scale = 20)
        {

            var copyImg = (Bitmap)srcImage.Clone();

            // グレースケール化
            var grayImage = ImgLib.ColorImageToGrayScale(copyImg);

            // 反転
            var invGrayImage = (Bitmap)grayImage.Clone();
            invGrayImage = ImgLib.Invert(invGrayImage);

            // Gaussian Blur
            var blur = new SuperfastBlur.GaussianBlur(invGrayImage);
            var blurredImg = blur.Process(scale);

            // 反転
            var invBlurredImg = (Bitmap)blurredImg.Clone();
            invBlurredImg = ImgLib.Invert(invBlurredImg);

            // divide
            var imgOut = ImgLib.divideImage(grayImage, invBlurredImg, 256.0);

            return imgOut;
        }


        // 最大送信No取得
        public int GetSendNoMax()
        {
            if (sendingImage == null) return 0;

            int n = (sendingImage.Length / sendDataSize);
            if (sendingImage.Length % sendDataSize != 0) n++;

            return n;
        }

        // ビットマップ→2bitグレースケールデータ列 変換
        byte[] BitmapToGray2bit(Bitmap image)
        {

            // Nullチェック
            if (image == null) return null;

            // サイズチェック
            if (image.Width != IMG_WIDTH || image.Height != IMG_HEIGHT)
            {

                // リサイズ
                image = ImgLib.ResizeBitmap(image, IMG_WIDTH, IMG_HEIGHT);

            }

            // グレースケール化
            var gray = ImgLib.ColorImageToGrayScale(image);

            // バッファ準備
            byte[] byteArray = new byte[IMG_BYTE_N];
            int pxlNo = 0;

            //  左に90度回転させる為
            //  x : width - 1 -> 0
            //  y : 0 -> height
            for (int x = IMG_WIDTH - 1; x >= 0; x--)
            {
                for (int y = 0; y < IMG_HEIGHT; y++)
                {

                    var pxl = gray.GetPixel(x, y).R;

                    int vl = 0;
                    if (pxl >= IMG_TH3) vl = 3;
                    else if (pxl >= IMG_TH2) vl = 2;
                    else if (pxl >= IMG_TH1) vl = 1;

                    int arrayNo = pxlNo / (8 / IMG_BIT_N);
                    int shift = (3 - pxlNo % (8 / IMG_BIT_N)) * IMG_BIT_N;

                    byte bit = (byte)(vl << shift);
                    byteArray[arrayNo] |= bit;

                    pxlNo++;
                }
            }

            return byteArray;
        }

        // イメージ設定
        public bool SetImage(Bitmap image)
        {

            sendingImage = BitmapToGray2bit(image);

            if (sendingImage == null) return false;
            if (sendingImage.Length <= 0) return false;

            sendingNo = 0;

            return true;
        }


        // 送信パケット取得
        public byte[] GetSendPacket()
        {
            const int HEADER_SIZE = 2;

            // エラーチェック
            if (sendingImage == null) return null;

            // 完了済み？
            if (HasFinished()) return null;

            // ヘッダー作成
            byte[] header = new byte[HEADER_SIZE];
            header[0] = (byte)(sendingNo & 0xFF);
            header[1] = (byte)((sendingNo >> 8) & 0xFF);

            // データ作成
            int pos = sendingNo * sendDataSize;
            var size = IMG_BYTE_N - pos;
            if (size > sendDataSize) size = sendDataSize;

            byte[] data = new byte[size];
            Array.Copy(sendingImage, pos, data, 0, size);

            byte[] pkt = new byte[HEADER_SIZE + size];

            Array.Copy(header, pkt, HEADER_SIZE);
            Array.Copy(data, 0, pkt, HEADER_SIZE, size);

            sendingNo++;

            return pkt;
        }

        // 進捗状況取得
        public int GetProgress()
        {
            int v = sendingNo * 100 / GetSendNoMax();
            return v;
        }

        // 完了チェック
        public bool HasFinished()
        {
            if (GetSendNoMax() <= sendingNo) return true;
            return false;
        }

        // 進捗状況をリセットする
        public void ResetProgress()
        {
            sendingNo = 0;
        }

    }

}
