using System;
using Xamarin.Forms;

namespace WebWindows.Blazor.XamarinForms
{
    public class ExtendedWebView : WebView
    {
        public EventHandler<string> OnWebMessageReceived { get; set; }
        public EventHandler<string> SendMessageFromJSToDotNetRequested { get; set; }

        public void HandleWebMessageReceived(string webMessageAsString)
        {
            OnWebMessageReceived?.Invoke(this, webMessageAsString);
        }

        public void SendMessage(string message)
        {
            SendMessageFromJSToDotNetRequested?.Invoke(this, message);
        }
    }
}
