using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Xamarin.Forms;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements.Handlers
{
    internal class WebViewHandler : ViewHandler
    {
        public XF.WebView Control { get; }

        public WebViewHandler(NativeComponentRenderer renderer, XF.WebView control)
            : base(renderer, control)
        {
            Control = control;

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body>
                                <h1>Trident</h1>
                                <p>This is an IE11 control.</p>
                                <script>document.write(navigator.userAgent);</script>
                                </body>
                                </html>";
            control.Source = htmlSource;
        }
    }
}
