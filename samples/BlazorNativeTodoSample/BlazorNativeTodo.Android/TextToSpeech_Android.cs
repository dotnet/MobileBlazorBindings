using Android.Speech.Tts;
using Java.Lang;

namespace BlazorNativeTodo.Droid
{
    public class TextToSpeech_Android : Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        private TextToSpeech _speaker;
        private string _toSpeak;

        public void Speak(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _toSpeak = text;
                if (_speaker == null)
                {
                    _speaker = new TextToSpeech(MainActivity.Instance, this);
                }
                else
                {
                    _speaker.Speak(_toSpeak, QueueMode.Flush, null, null);
                }
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                _speaker.Speak(_toSpeak, QueueMode.Flush, null, null);
            }
        }
        #endregion
    }
}
