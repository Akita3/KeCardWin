using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class Cmd
    {

        // 1byte コマンド
        public const byte CMD_NONE = 0x00;      /* コマンドなし */
        public const byte CMD_ERASE_FLASH = 0x01;      /* 消去コマンド */
        public const byte CMD_IMAGE_WRITE = 0x02;      /* イメージ書き込み */
        public const byte CMD_DISPLAY = 0x03;      /* 表示コマンド */
        public const byte CMD_FAST_MODE = 0x04;      /* FASTモード遷移コマンド */
        public const byte CMD_SLOW_MODE = 0x05;      /* SLOWモード遷移コマンド */
        public const byte CMD_ERASE_CND_ACT = 0x06;      /* 条件＆アクション消去コマンド */


        // 2byte コマンド
        public const byte CMD_2BYTE = (0xFF);      /* 2Byteコマンド */
        public const byte CMD_TIME = (0xFE);  /* 時間コマンド */
        public const byte CMD_COND = (0xFD);   /* 条件コマンド */
        public const byte CMD_ACT = (0xFC); /* アクションコマンド */


        // 長さ
        public const byte CMD_2BYTE_LEN = (2);
        public const byte CMD_TIME_LEN = (6);
        public const byte CMD_COND_LEN = (2 + 4 + 8);
        public const byte CMD_ACT_LEN = (2 + 4 + 8);


    }
}
