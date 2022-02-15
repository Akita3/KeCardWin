using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeCardWin
{
    public partial class FormWatch : Form
    {

        public const int WATCH_COLUMN_COUNT = 3;
        public const int WATCH_LABEL_X = 0;
        public const int WATCH_VALUE_X = 1;
        public const int WATCH_COMMENT_X = 2;
        public const int WATCH_COLUMN_WIDTH = 300;


        public FormWatch()
        {
            InitializeComponent();
        }

        private void FormWatch_Load(object sender, EventArgs e)
        {
            InitGridView();
        }


        private void InitGridView()
        {
            // 幅、高さ設定
            grdDataView.ColumnCount = WATCH_COLUMN_COUNT;
            grdDataView.Columns[WATCH_LABEL_X].Width = WATCH_COLUMN_WIDTH;
            grdDataView.Columns[WATCH_VALUE_X].Width = WATCH_COLUMN_WIDTH;
            grdDataView.Columns[WATCH_COMMENT_X].Width = WATCH_COLUMN_WIDTH;

            // ソート禁止
            foreach( DataGridViewColumn column in grdDataView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Rowの数設定
            grdDataView.RowCount = Common.ramAll.rowCount;
        }


        public void UpdateGridView(string key, string data, bool scroll)
        {

            bool b = Common.ramAll.UpdateData(key, data);
            if (!b) return;

            RamList ramList = Common.ramAll.GetRamList(key);
            if (ramList == null) return;

            // スクロール
            if(scroll)
            {
                grdDataView.FirstDisplayedScrollingRowIndex = ramList.labelRow;
            }

            // ラベル
            grdDataView[WATCH_LABEL_X, ramList.labelRow].Value = "[" + key + "]";

            // データ設定
            for (int i = 0; i < ramList.rams.Length; i++)
            {
                int posY = ramList.startRow + i;
                grdDataView[WATCH_LABEL_X, posY].Value = ramList.rams[i].label;
                grdDataView[WATCH_VALUE_X, posY].Value = ramList.rams[i].valueText;
                grdDataView[WATCH_COMMENT_X, posY].Value = ramList.rams[i].comment;
            }

        }



    }
}
