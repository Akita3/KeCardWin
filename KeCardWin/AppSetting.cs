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

        // パラメータ (XMLとして保存されるパラメータ)
        public KeImage.IMG_FILTER imgFilter = KeImage.IMG_FILTER.NONE;
        public byte imageNo = 0;
        public string backupMemoName = "backupMemo.txt";
        public string backupImageName = "backupImage.png";
        public string transferImageName = "transferImage.png";
        public bool darkMode = false;
        public ExRichTextBox.RuledLineType ruledLineType = ExRichTextBox.RuledLineType.Dash;
        public int waitAfterTransfer = 0;
        public int transferPacketSize = KeImage.DATA_SIZE_HIGH_SPEED;

        public string keName = "かっこいいカード";



        // 読み込んだパラメータの値チェック
        static private void CheckValue(ref AppSetting appSetting)
        {
            if (appSetting.imageNo > KeBle.KE_IMAGE_NO_MAX) appSetting.imageNo = 0;

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
