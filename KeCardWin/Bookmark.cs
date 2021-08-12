using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace KeCardWin
{
    [Serializable]
    public class Bookmark
    {
        public enum Mode
        {
            CAPTURE = 0,    // 画面キャプチャ
            FILE,           // ファイルから読み込み
            MAX,
        }

        public Mode mode;
        public Rectangle rect;
        public string keyword;
        public Bitmap bitmap;

        public Bookmark()
        {
            mode = Mode.CAPTURE;
            rect = new Rectangle();
            keyword = "";
            bitmap = null;
        }

        public Bookmark( Mode _mode , Rectangle _rect , string _keyword , string _fname )
        {
            mode = _mode;
            rect = _rect;
            keyword = _keyword;
            try
            {
                if(File.Exists(_fname) == true )
                {
                    FileStream fs;
                    fs = new FileStream(_fname, FileMode.Open, FileAccess.Read);
                    Bitmap _bmp = (Bitmap)System.Drawing.Bitmap.FromStream(fs);
                    fs.Close();

                    bitmap = new Bitmap(_bmp);
                }

            }
            catch { }
        }


        public Bookmark DeepCopy()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Position = 0;
                return (Bookmark)bf.Deserialize(ms);
            }
        }



    }
}
