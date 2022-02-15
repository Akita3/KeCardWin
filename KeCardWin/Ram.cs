using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KeCardWin
{

    public enum RamType
    {
        HEX,
        DEC,
    }

    public class Ram
    {
        public string label;        // ラベル
        public int size;            // データサイズ
        public RamType type;        // 型
        public string comment;      // コメント
        public string valueText;    // 値

        public Ram(string _label, int _size, string _type , string _comment )
        {
            label = _label;
            size = _size;
            type = _type == "int(16)" ? RamType.HEX : RamType.DEC;
            comment = _comment;
            valueText = "";
        }

        public void SetHexData( string _hexData )
        {
            if(type == RamType.HEX)
            {
                valueText = _hexData;
            } else
            {
                int _num;
                bool b = int.TryParse(_hexData, System.Globalization.NumberStyles.HexNumber,null, out _num);
                if( b )
                {
                    valueText = _num.ToString();
                } else
                {
                    valueText = "Error";
                }
            }
        }

    }

    public class RamList
    {
        public Ram[] rams;
        public int labelRow = 0;
        public int startRow = 1;

        const int FILE_HEADER_ROW = 0;

        public bool Load( string fileName )
        {
            const int LABEL_POS = 0;
            const int SIZE_POS = 1;
            const int TYPE_POS = 2;
            const int COMMENT_POS = 3;
            const int ELEMENT_COUNT = 4;     // 要素数

            // リスト作成
            List<Ram> tmpRams = new List<Ram>();

            try
            {
                string[] lines = File.ReadAllLines(fileName, Encoding.GetEncoding("Shift_JIS") );

                for( int i = 0; i < lines.Count() ;i ++ )
                {
                    if (i == FILE_HEADER_ROW) continue;

                    string[] data = lines[i].Split(',');
                    if (data.Length < ELEMENT_COUNT) continue;

                    string _label = data[LABEL_POS].Trim();
                    int _size = 0;
                    int.TryParse(data[SIZE_POS].Trim(), out _size);
                    string _type = data[TYPE_POS].Trim();
                    string _comment = data[COMMENT_POS].Trim();

                    Ram ram = new Ram(_label, _size, _type, _comment);

                    // リストに追加
                    tmpRams.Add(ram);
                }

                // 結果を格納
                rams = tmpRams.ToArray();
                return true;
            }
            catch { }
            return false;
        }

    }


    public class RamAll
    {
        public Dictionary<string, RamList> all;
        public int rowCount = 0;

        public const int GAP_ROW = 2;

        // コンストラクタ
        public RamAll()
        {
            all = new Dictionary<string, RamList>();
        }


        // ファイルから全情報読み込み
        public bool Load(string folderPath , string[] nameList )
        {
            rowCount = 0;

            foreach( string name in nameList)
            {
                string file = Path.Combine(folderPath, name + ".csv");

                // キーを取得
                string key = Path.GetFileNameWithoutExtension(file);

                // ファイル読み込み
                RamList ramList = new RamList();
                bool b = ramList.Load(file);
                if( b )
                {
                    // 位置情報設定
                    ramList.labelRow = rowCount;
                    ramList.startRow = rowCount + 1;
                    rowCount += ramList.rams.Length + GAP_ROW;

                    all.Add(key, ramList);
                }

            }


            return false;
        }


        // バイトスワップ
        public string ByteSwap(string dataText)
        {
            string result = "";
            while (dataText.Length > 0)
            {
                result = dataText.Substring(0, 2) + result;
                dataText = dataText.Substring(2);
            }
            return result;
        }

        // データ更新
        public bool UpdateData(string key, string dataText)
        {
            if (all.ContainsKey(key) == false) return false;

            int pos = 0;
            foreach (Ram ram in all[key].rams)
            {
                // スキップサイズを計算
                int skip = 0;
                if (pos % ram.size != 0)
                {
                    skip = ((pos / ram.size) + 1) * ram.size - pos;

                    if (skip * 2 > dataText.Length) return false;

                    dataText = dataText.Substring(skip * 2);
                    pos += skip;
                }

                // データサイズチェック
                if (ram.size * 2 > dataText.Length) return false;

                // データ取得
                ram.SetHexData( ByteSwap(dataText.Substring(0, ram.size * 2)) );
                dataText = dataText.Substring(ram.size * 2);
                pos += ram.size;
            }

            if (dataText.Length != 0) return false;

            return true;
        }

        // Ramリスト取得
        public RamList GetRamList(string key)
        {
            if(all.ContainsKey(key))
            {
                return all[key];
            }
            return null;
        }

    }

}
