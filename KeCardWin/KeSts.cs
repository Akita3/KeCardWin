using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class KeSts
    {

        // ステータスの種類
        public const int KESTS_NONE = 0x00;      /* なし */
        public const int KESTS_ERASE_FLASH = 0x10;      /* フラッシュ消去 */
        public const int KESTS_IMAGE_WRITE = 0x20;      /* イメージ書き込み */
        public const int KESTS_DISPLAY = 0x30;      /* イメージ表示 */
        public const int KESTS_CLEAR = 0x40;      /* クリア */
        public const int KESTS_INFO = 0x50;      /* Info */
        public const int KESTS_READ_DATA = 0x70;      /* Read data */
        public const int KESTS_WRITE_DATA = 0x80;      /* Write data */
        public const int KESTS_ERASE_DATA = 0x90;      /* Erase data */
        public const int KESTS_SYS_SET = 0xA0;      /* System Setting */
        public const int KESTS_HELLO = 0xB0;      /* Hello */
        public const int KESTS_PROC = 0xD0;      /* Procedure */
        public const int KESTS_TIME = 0xE0;      /* Time */


        // ステータスの結果
        public const int KESTS_RESULT_ING = 0x00;      /* 実行中 */
        public const int KESTS_RESULT_SUCCESS = 0x01;      /* 成功 */
        public const int KESTS_RESULT_FAIL = 0x02;      /* 失敗 */


    }
}
