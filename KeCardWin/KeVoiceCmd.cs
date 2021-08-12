using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;


namespace KeCardWin
{

    // デリゲート：
    public delegate void DelegateKeVoiceCmdHypothesized(string word);
    public delegate void DelegateKeVoiceCmdRecognized(string word);


    public class KeVoiceCmd
    {
        SpeechRecognitionEngine engine;

        string[] awakeWord;
        string[] actionWords;

        // デリゲート
        public DelegateKeVoiceCmdHypothesized delegateKeVoiceCmdHypothesized = null;
        public DelegateKeVoiceCmdRecognized delegateKeVoiceCmdRecognized = null;

        //
        public void Open( string _awakeWord , string [] _actionWords )
        {
            awakeWord = new string[] { _awakeWord };
            actionWords = _actionWords;

            // 呼び出し
            Choices awake = new Choices(awakeWord);

            // アクションワード
            Choices actions = new Choices();
            actions.Add(actionWords);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(awake);
            gb.Append(actions);

            engine = new SpeechRecognitionEngine();

            // 音声認識が認識処理を終えたときのイベントハンドラを設定する。
            engine.SpeechRecognized += recogEngine_SpeechRecognized;

            // 音声認識が推定処理を終えたときのイベントハンドラを設定する。
            engine.SpeechHypothesized += recogEngine_SpeechHypothesized;

            // SystemSpeech を利用したディクテーションを行う場合には、
            engine.LoadGrammar(new Grammar(gb));

            // 実行環境の標準の入力を音声認識エンジンの入力とする。
            engine.SetInputToDefaultAudioDevice();

            // 非同期の認識を継続して実行するようにして音声認識を開始する。
            engine.RecognizeAsync(RecognizeMode.Multiple);

        }

        public void Close()
        {
            if (this.engine != null)
            {
                this.engine.SpeechRecognized -= recogEngine_SpeechRecognized;
                this.engine.SpeechHypothesized -= recogEngine_SpeechHypothesized;
                this.engine.Dispose();
                this.engine = null;
            }
        }

        public void Dispose()
        {
            this.Close();
        }

        private void recogEngine_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            System.Console.WriteLine("○" + e.Result.Text + "(" + e.Result.Confidence + ")");
            if(delegateKeVoiceCmdHypothesized != null)
            {
                delegateKeVoiceCmdHypothesized(e.Result.Text);
            }
        }

        private void recogEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine("●" + e.Result.Text + "(" + e.Result.Confidence + ")");

            // 最初に見つかった文字を結果とする
            foreach( string w in actionWords)
            {
                if( e.Result.Text.Contains(w) )
                {
                    if(delegateKeVoiceCmdRecognized != null)
                    {
                        delegateKeVoiceCmdRecognized(w);
                    }
                    System.Console.WriteLine(w);
                    break;
                }
            }

        }



    }
}
