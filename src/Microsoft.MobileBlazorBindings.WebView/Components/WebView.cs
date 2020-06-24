using Microsoft.MobileBlazorBindings.WebView.Components.Handlers;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.WebView.Components
{
    public class WebView : View
    {
        static WebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<WebView>(renderer => new WebViewHandler(renderer, new WebViewExtended()));
        }

        [Parameter] public XF.WebViewSource Source { get; set; }
        [Parameter] public EventCallback<string> OnWebMessageReceived { get; set; }

        public void SendMessage(string message)
        {
            ((WebViewHandler)ElementHandler).Control.SendMessage(message);
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onwebmessagereceived", EventCallback.Factory.Create<WebMessageEventArgs>(this, HandleOnWebMessageReceived));

            switch (Source)
            {
                case XF.HtmlWebViewSource htmlWebViewSource:
                    builder.AddAttribute(nameof(XF.HtmlWebViewSource), htmlWebViewSource.Html);
                    break;
                case XF.UrlWebViewSource urlWebViewSource:
                    builder.AddAttribute(nameof(XF.UrlWebViewSource), urlWebViewSource.Url);
                    break;
            }
        }

        private Task HandleOnWebMessageReceived(WebMessageEventArgs args)
            => OnWebMessageReceived.InvokeAsync(args.Message);

        internal class WebMessageEventArgs : EventArgs
        {
            public string Message { get; set; }
        }
    }
}
