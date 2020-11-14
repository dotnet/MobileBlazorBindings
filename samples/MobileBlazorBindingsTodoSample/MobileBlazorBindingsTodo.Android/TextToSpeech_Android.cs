// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.Runtime;
using Android.Speech.Tts;
using System;

namespace MobileBlazorBindingsTodo.Droid
{
    public class TextToSpeech_Android : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        private TextToSpeech _speaker;
        private string _toSpeak;

        public TextToSpeech_Android()
        {

        }

        public TextToSpeech_Android(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

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
