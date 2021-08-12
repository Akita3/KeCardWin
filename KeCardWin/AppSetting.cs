using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace KeCardWin
{
    // クラス：AppSetting (アプリケーションの設定)
    public class AppSetting
    {
        // 画像フィルター
        public enum IMG_FILTER{
            NONE = 0,
            DITHERING,
            SKETCH,
        };

        // パラメータ (XMLとして保存されるパラメータ)
        public IMG_FILTER imgFilter = IMG_FILTER.NONE;
        public byte imageNo = 0;
        public string backupMemoName = "backupMemo.txt";
        public string backupImageName = "backupImage.png";
        public string transferImageName = "transferImage.png";
        public bool darkMode = false;
        public ExRichTextBox.RuledLineType ruledLineType = ExRichTextBox.RuledLineType.Dash;
        public int waitAfterTransfer = 0;

        public const string BOOKMARK1_IMG = "bookmark1.png";
        public const string BOOKMARK2_IMG = "bookmark2.png";
        public const string BOOKMARK3_IMG = "bookmark3.png";

        public string keName = "かっこいいカード";

        public int bookmark1Mode = 0;
        public int bookmark1Left = 0;
        public int bookmark1Top = 0;
        public int bookmark1Width = 0;
        public int bookmark1Height = 0;
        public string bookmark1Keyword = "";
        public int bookmark2Mode = 0;
        public int bookmark2Left = 0;
        public int bookmark2Top = 0;
        public int bookmark2Width = 0;
        public int bookmark2Height = 0;
        public string bookmark2Keyword = "";
        public int bookmark3Mode = 0;
        public int bookmark3Left = 0;
        public int bookmark3Top = 0;
        public int bookmark3Width = 0;
        public int bookmark3Height = 0;
        public string bookmark3Keyword = "";

        // Bookmarks から設定
        public Bookmarks GetBookmarks()
        {
            Bookmarks b = new Bookmarks();

            b.bookmarks[0] = new Bookmark( (Bookmark.Mode)bookmark1Mode , new Rectangle(bookmark1Left, bookmark1Top, bookmark1Width, bookmark1Height), bookmark1Keyword , BOOKMARK1_IMG);
            b.bookmarks[1] = new Bookmark( (Bookmark.Mode)bookmark2Mode,  new Rectangle(bookmark2Left, bookmark2Top, bookmark2Width, bookmark2Height), bookmark2Keyword , BOOKMARK2_IMG);
            b.bookmarks[2] = new Bookmark( (Bookmark.Mode)bookmark3Mode, new Rectangle(bookmark3Left, bookmark3Top, bookmark3Width, bookmark3Height), bookmark3Keyword , BOOKMARK3_IMG);

            return b;
        }

        // Bookmarks を取得
        public void SetBookmarks(Bookmarks b)
        {
            bookmark1Mode = (int)b.bookmarks[0].mode;
            bookmark1Left = b.bookmarks[0].rect.X;
            bookmark1Top = b.bookmarks[0].rect.Y;
            bookmark1Width = b.bookmarks[0].rect.Width;
            bookmark1Height = b.bookmarks[0].rect.Height;
            bookmark1Keyword = b.bookmarks[0].keyword;

            if(b.bookmarks[0].bitmap != null) b.bookmarks[0].bitmap.Save(BOOKMARK1_IMG);

            bookmark2Mode = (int)b.bookmarks[1].mode;
            bookmark2Left = b.bookmarks[1].rect.X;
            bookmark2Top = b.bookmarks[1].rect.Y;
            bookmark2Width = b.bookmarks[1].rect.Width;
            bookmark2Height = b.bookmarks[1].rect.Height;
            bookmark2Keyword = b.bookmarks[1].keyword;
            if (b.bookmarks[1].bitmap != null) b.bookmarks[1].bitmap.Save(BOOKMARK2_IMG);

            bookmark3Mode = (int)b.bookmarks[2].mode;
            bookmark3Left = b.bookmarks[2].rect.X;
            bookmark3Top = b.bookmarks[2].rect.Y;
            bookmark3Width = b.bookmarks[2].rect.Width;
            bookmark3Height = b.bookmarks[2].rect.Height;
            bookmark3Keyword = b.bookmarks[2].keyword;
            if (b.bookmarks[2].bitmap != null) b.bookmarks[2].bitmap.Save(BOOKMARK3_IMG);

        }


        // 読み込んだパラメータの値チェック
        static private void CheckValue(ref AppSetting appSetting)
        {
            if (appSetting.imageNo > KeBle.KE_IMAGE_NO_MAX) appSetting.imageNo = 0;

            if (appSetting.bookmark1Mode < 0 || (int)Bookmark.Mode.MAX <= appSetting.bookmark1Mode) appSetting.bookmark1Mode = 0;
            if (appSetting.bookmark2Mode < 0 || (int)Bookmark.Mode.MAX <= appSetting.bookmark2Mode) appSetting.bookmark2Mode = 0;
            if (appSetting.bookmark3Mode < 0 || (int)Bookmark.Mode.MAX <= appSetting.bookmark3Mode) appSetting.bookmark3Mode = 0;
        }

        // 設定読み込み
        static public AppSetting LoadSettings(string fileName)
        {
            AppSetting appSetting = new AppSetting();
            try
            {
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(AppSetting));
                //読み込むファイルを開く
                System.IO.StreamReader sr = new System.IO.StreamReader(
                    fileName, new System.Text.UTF8Encoding(false));
                //XMLファイルから読み込み、逆シリアル化する
                appSetting = (AppSetting)serializer.Deserialize(sr);
                //ファイルを閉じる
                sr.Close();

                // 値をチェック
                CheckValue(ref appSetting);
            }
            catch { }
            return appSetting;
        }

        // 設定書き込み
        static public bool SaveSettings(AppSetting appSetting,string fileName )
        {
            bool res = false;
            try
            {
                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(AppSetting));
                //書き込むファイルを開く（UTF-8 BOM無し）
                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    fileName, false, new System.Text.UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, appSetting);
                //ファイルを閉じる
                sw.Close();

                res = true;
            }
            catch { }

            return res;
        }

        // ビットマップ読み込み (例外スルー)
        static public Bitmap LoadBitmap(string fileName)
        {
            Bitmap bitmap = null;

            try
            {
                bitmap = new Bitmap(fileName);
            }
            catch { }

            return bitmap;
        }

        // ビットマップ書き込み (例外スルー)
        static public bool SaveBitmap(Bitmap bitmap , string fileName)
        {
            bool res = false;
            try
            {
                bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

                res = true;
            }
            catch { }

            return res;
        }

        // メモ保存
        static public bool SaveMemo(string fname , string text )
        {
            bool res = false;
            try
            {
                File.WriteAllText(fname, text);
                res = true;
            }
            catch { }
            return res;
        }

        // メモ読み込み
        static public string LoadMemo(string fname)
        {
            string res = "";
            try
            {
                res = File.ReadAllText(fname);
            }
            catch { }
            return res;
        }


    }
}
