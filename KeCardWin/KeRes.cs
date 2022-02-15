using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class KeRes
    {

        public const int RES_NONE = 0x00;           // なし
        public const int RES_NOTIFY = 0x01;         // Notify
        public const int RES_RSSI = 0x02;           // RSSI
        public const int RES_DATA = 0x03;           // Data
        public const int RES_INFO = 0x04;           // Info


        // 位置
        public const int RES_TYPE_POS = 0;          // タイプ位置
        public const int RES_NOTIFY_DATA_POS = 1;
        public const int RES_RSSI_DATA_POS = 1;
        public const int RES_DATA_RAW_POS = 3;
        public const int RES_INFO_TYPE_POS = 1;
        public const int RES_INFO_DATA_POS = 2;

        // 長さ
        public const int RES_NOTIFY_LEN = 2;        // Notify
        public const int RES_RSSI_LEN = 2;          // RSSI
        public const int RES_DATA_HEADER_LEN = 1;   // Data

    }


    public class KeResData
    {
        public const int RESULT_SUCCESS = 0;
        public const int RESULT_ERR_TYPE = -1;
        public const int RESULT_ERR_RCV_SIZE = -2;
        public const int RESULT_ERR_INDEX = -3;


        public List<byte> data;
        public int result;

        public KeResData()
        {
            data = new List<byte>();
            result = RESULT_SUCCESS;
        }

        public void RecieveData(byte[] _rcv)
        {
            // サイズチェック
            if( _rcv.Length <= KeRes.RES_DATA_HEADER_LEN)
            {
                result = RESULT_ERR_RCV_SIZE;
                return;
            }

            // タイプチェック
            if( _rcv[KeRes.RES_TYPE_POS] != KeRes.RES_DATA )
            {
                result = RESULT_ERR_TYPE;
                return;
            }

            // データを追加
            data.AddRange( _rcv.Skip(KeRes.RES_DATA_HEADER_LEN) );

        }
    }

}
