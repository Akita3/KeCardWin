using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeCardWin
{
    public class TextConv
    {

        public static string Hex2Text(byte[] hexDatas)
        {
            string text = "";
            foreach (byte hx in hexDatas)
            {
                if ((0x20 <= hx && hx <= 0x7E) || (hx == '\r') || (hx == '\n'))
                {
                    text += (Convert.ToChar(hx)).ToString();
                }
                else
                {
                    text += " ";
                }
            }

            return text;
        }

    }
}
