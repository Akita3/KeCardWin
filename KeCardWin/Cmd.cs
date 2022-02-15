using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class Cmd
    {

        // Command

        // 1byte コマンド
        public const byte CMD_NONE = 0x00;          /* コマンドなし */
        public const byte CMD_ERASE_FLASH = 0x01;   /* 消去コマンド */
        public const byte CMD_IMAGE_WRITE = 0x02;   /* イメージ書き込み */
        public const byte CMD_DISPLAY = 0x03;       /* 表示コマンド */


        // 2byte コマンド
        public const byte CMD_2BYTE = 0xFF;       /* 2Byteコマンド */
        public const byte CMD_TIME = 0xFE;        /* 時間コマンド */
        public const byte CMD_PROC = 0xFD;        /* 条件コマンド */
        public const byte CMD_HELLO = 0xFB;
        public const byte CMD_SYS_SET = 0xFA;
        public const byte CMD_ERASE_DATA = 0xF9;
        public const byte CMD_WRITE_DATA = 0xF8;
        public const byte CMD_READ_DATA = 0xF7;
        public const byte CMD_INFO = 0xF5;
        public const byte CMD_CLEAR = 0xF4;


        // 長さ
        public const int CMD_2BYTE_LEN = 2;
        public const int CMD_TIME_LEN = 6;

        public const int CMD_PHON_LEN = 2 + 2 + 4 + 8 + 4;
        public const int CMD_HELLO_LEN = 2 + 2;
        public const int CMD_SYS_SET_LEN = 2 + 4;
        public const int CMD_ERASE_DATA_LEN = 2 + 4 + 2;
        public const int CMD_WRITE_DATA_HEADER_LEN = 2 + 4;
        public const int CMD_WRITE_DATA_RAW_SIZE_MAX = 224;
        public const int CMD_WRITE_DATA_LEN_MIN = CMD_WRITE_DATA_HEADER_LEN + 1;
        public const int CMD_WRITE_DATA_LEN_MAX = CMD_WRITE_DATA_HEADER_LEN + CMD_WRITE_DATA_RAW_SIZE_MAX;
        public const int CMD_READ_DATA_LEN = 2 + 4 + 2;
        public const int CMD_READ_DATA_LEN_MAX = 224;
        public const int CMD_INFO_LEN = 2 + 1;
        public const int CMD_CLEAR_LEN = 2 + 1;

    }
}
