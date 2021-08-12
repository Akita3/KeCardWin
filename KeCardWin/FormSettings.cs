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
    public partial class FormSettings : Form
    {
        KeVoiceCmd keVoiceCmdTest;
        string testAwakeWord;
        string []testActionWords;
        bool closePermition = true;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void KeVoiceTestOnOff(bool sw)
        {
            if (sw)
            {
                keVoiceCmdTest = new KeVoiceCmd();
                keVoiceCmdTest.Open(testAwakeWord, testActionWords);
                keVoiceCmdTest.delegateKeVoiceCmdHypothesized = KeVoiceCmdTestHypothesized;
                keVoiceCmdTest.delegateKeVoiceCmdRecognized = KeVoiceCmdTestRecognized;

            }
            else
            {
                if (keVoiceCmdTest != null)
                {
                    keVoiceCmdTest.Close();
                    keVoiceCmdTest = null;
                }
            }
        }

        private void chkTest_CheckedChanged(object sender, EventArgs e)
        {

            if ( chkTest.Checked == true )
            {
                testAwakeWord = txtKeName.Text.Trim();
                testActionWords = new string[] { txtTestWord.Text.Trim() };

                if (testAwakeWord == "" || testActionWords[0] == "")
                {
                    MessageBox.Show("名前又はテストする言葉が空白です。");
                    chkTest.Checked = false;
                    return;
                }

                txtKeName.Enabled = false;
                txtTestWord.Enabled = false;

                KeVoiceTestOnOff(true);
            }
            else
            {
                KeVoiceTestOnOff(false);

                txtKeName.Enabled = true;
                txtTestWord.Enabled = true;
            }
        }

        private void KeVoiceCmdTestHypothesized(string word)
        {
            txtTestLog.AppendText(word + "(認識中)\r\n");
        }

        private void KeVoiceCmdTestRecognized(string word)
        {
            txtTestLog.AppendText(word + "(確定)\r\n");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            closePermition = true;

            // 送信後のWaitチェック
            try
            {
                int val = Convert.ToInt32(txtWaitAfterTransfer.Text);

            } catch
            {
                MessageBox.Show("データ送信後のWaitには数値を入力してください。");
                closePermition = false;
            }

            // 名前のチェック
            string kename = txtKeName.Text.Trim();
            if(kename == "" )
            {
                MessageBox.Show("名前を入力してください。");
                closePermition = false;
            }
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closePermition)
            {
                closePermition = true;
                e.Cancel = true;
            }
        }
    }
}
