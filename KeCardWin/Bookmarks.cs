using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace KeCardWin
{
    [Serializable]
    public class Bookmarks
    {
        public const int BOOK_MARK_MAX = 3;

        public Bookmark[] bookmarks = new Bookmark[BOOK_MARK_MAX];

        public Bookmarks()
        {
            for( int i = 0; i < BOOK_MARK_MAX; i ++ )
            {
                bookmarks[i] = new Bookmark();
            }
        }

        public Bookmarks DeepCopy()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Position = 0;
                return (Bookmarks)bf.Deserialize(ms);
            }
        }


        public string[] GetKeywords()
        {
            List<string> _keywords = new List<string>();

            foreach ( var b in bookmarks )
            {
                if (b.keyword == "") continue;
                if( b.keyword.Contains(",") )
                {
                    var _words = b.keyword.Split(',');
                    _keywords.AddRange(_words.ToList());
                }
                else
                {
                    _keywords.Add(b.keyword);
                }
            }

            return _keywords.ToArray();
        }



    }
}
