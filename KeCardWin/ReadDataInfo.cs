using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{

    // 大量データ読み込み用情報
    public class ReadDataInfo
    {
        public const int LONG_READ_SIZE = 128;

        public UInt32 readAddr = 0;
        public int readSize = 0;
        public int remainSize = 0;
        public int recieveSize = 0;
        public bool longRead = false;

        public ReadDataInfo()
        {
            readAddr = 0;
            readSize = 0;
            remainSize = 0;
            recieveSize = 0;
            longRead = false;
        }

        public ReadDataInfo( UInt32 _addr , int _length , bool _longRead)
        {
            readAddr = _addr;
            remainSize = _length;
            recieveSize = 0;
            longRead = _longRead;

            if (longRead)
            {
                readSize = remainSize > LONG_READ_SIZE ? LONG_READ_SIZE : remainSize;
            }
            else
            {
                readSize = remainSize;
            }
            remainSize -= readSize;
            if (remainSize <= 0) longRead = false;
        }

        public void Next()
        {
            readAddr += (UInt32)readSize;
            readSize = remainSize > LONG_READ_SIZE ? LONG_READ_SIZE : remainSize;
            remainSize -= readSize;
            if (remainSize <= 0) longRead = false;
        }

    }

}
