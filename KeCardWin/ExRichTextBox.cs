using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace KeCardWin
{
    // クラス：ExRichTextBox (罫線あり)
    public partial class ExRichTextBox : RichTextBox
    {

        // 罫線のタイプ 定義
        public enum RuledLineType
        {
            None = 0,   // なし
            Dash,       // 破線    
            Title,      // タイトルあり
            Max
        };


        // 破線タイプ 変数
        private RuledLineType ruledLineType = RuledLineType.Title;

        // IME制御中フラグ
        bool imeFlag = false;


        // WinAPI SendMessage 定義
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg,
                                                 Int32 wParam, ref PARAFORMAT2 lParam);

        // Windowsメッセージ
        private const int WM_PAINT = 0x000F;
        private const int WM_IME_NOTIFY = 0x0282;
        private const int WM_IME_STARTCOMPOSITION = 0x10D; // IME変換開始
        private const int WM_IME_ENDCOMPOSITION = 0x10E;   // IME変換終了

        // 定数定義
        private const int SCF_DEFAULT = 0;
        private const int SCF_SELECTION = 1;
        public const int PFM_LINESPACING = 256;
        public const int EM_SETPARAFORMAT = 1095;

        // PARAFORMAT2 定義
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public Int16 wNumbering;
            public Int16 wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public Int16 wAlignment;
            public Int16 cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public Int16 sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public Int16 wShadingWeight;
            public Int16 wShadingStyle;
            public Int16 wNumberingStart;
            public Int16 wNumberingStyle;
            public Int16 wNumberingTab;
            public Int16 wBorderSpace;
            public Int16 wBorderWidth;
            public Int16 wBorders;
        }

        // 行間隔を設定
        public void SetLineSpacing(byte bLineSpacingRule, int dyLineSpacing)
        {
            PARAFORMAT2 format = new PARAFORMAT2();
            format.cbSize = Marshal.SizeOf(format);
            format.dwMask = PFM_LINESPACING;
            format.dyLineSpacing = dyLineSpacing;
            format.bLineSpacingRule = bLineSpacingRule;
            SendMessage(this.Handle, EM_SETPARAFORMAT, SCF_DEFAULT /* SCF_SELECTION */, ref format);
        }

        // コンストラクタ
        public ExRichTextBox()
        {
            InitializeComponent();

            // 行間隔を設定
            SetLineSpacing(2, 0);
        }

        // 罫線タイプ設定
        public void SetRuledLineType( RuledLineType _type )
        {
            ruledLineType = _type;
            this.Invalidate();
        }

        // 罫線描画
        private void DrawRuledLine(Graphics g)
        {
            if (ruledLineType == RuledLineType.None) return;

            const int GAP_Y = 5;

            List<int> lstY = new List<int>();
            // using (Graphics g = this.CreateGraphics())
            using (Brush brush = new SolidBrush(Color.DarkGreen))
            {
                // 改行検索
                Regex regex = new Regex("\n");
                foreach (Match match in regex.Matches(this.Text))
                {
                    int idx = match.Index;
                    if (idx + 1 <= this.Text.Length)
                    {
                        Point point = this.GetPositionFromCharIndex(idx + 1);
                        lstY.Add(point.Y - GAP_Y);
                    }

                }

                // ペンを準備
                Pen borderPen = new Pen(Color.Black);
                Pen dashPen = new Pen(Color.DarkGray);
                borderPen.Width = 2;
                dashPen.Width = 2;
                dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                int lineWidth = this.ClientSize.Width - 3;

                // 罫線を描画
                for( int i = 0;i < lstY.Count; i ++ )
                {
                    Pen selectPen = dashPen;
                    if( ruledLineType == RuledLineType.Title && i == 0 )
                    {
                        selectPen = borderPen;
                    }
                    g.DrawLine(selectPen, new Point(2, lstY[i]), new Point(lineWidth, lstY[i]));
                }

            }

        }

        // OnPaint
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // IME制御中なら罫線描画は行わない (日本語が入力できない問題)
            if (imeFlag) return;

            // 罫線描画
            DrawRuledLine(pe.Graphics);

        }



        // WndProc
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch( m.Msg )
            {
                case WM_PAINT:
                    using (Graphics graphic = base.CreateGraphics())
                        OnPaint(new PaintEventArgs(graphic, base.ClientRectangle));
                    break;
                case WM_IME_STARTCOMPOSITION:
                    imeFlag = true;
                    break;
                case WM_IME_ENDCOMPOSITION:
                    imeFlag = false;
                    break;

            }
        }


        // ビットマップ取得
        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bitmap, new Rectangle(0, 0, this.Width, this.Height));

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                DrawRuledLine(g);
            }

            return bitmap;
        }


        // プレーンテキストとして貼り付け
        public void PastePlainText()
        {
            this.Paste(DataFormats.GetFormat("Text"));
        }

        // キー操作
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V))
            {
                // ペースト Ctrl +V
                PastePlainText();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
