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
    public partial class FormBookmark : Form
    {
        public Bookmarks bookmarks = new Bookmarks();

        public FormBookmark()
        {
            InitializeComponent();
        }

        private void FormBookmark_Load(object sender, EventArgs e)
        {
            bookmarkCtrl1.lblName.Text = "お気に入り１";
            bookmarkCtrl2.lblName.Text = "お気に入り２";
            bookmarkCtrl3.lblName.Text = "お気に入り３";

            bookmarkCtrl1.SetBookmark(bookmarks.bookmarks[0]);
            bookmarkCtrl2.SetBookmark(bookmarks.bookmarks[1]);
            bookmarkCtrl3.SetBookmark(bookmarks.bookmarks[2]);

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bookmarks.bookmarks[0] = bookmarkCtrl1.GetBookmark();
            bookmarks.bookmarks[1] = bookmarkCtrl2.GetBookmark();
            bookmarks.bookmarks[2] = bookmarkCtrl3.GetBookmark();
        }
    }
}
