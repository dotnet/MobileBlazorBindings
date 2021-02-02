using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace Microsoft.MobileBlazorBindings.WindowsForms
{
    public class BlazorWebView : Control
    {
        private WebView2 _webView2;

        public BlazorWebView()
        {
            _webView2 = new WebView2()
            {
                Dock = DockStyle.Fill,
            };
            Controls.Add(_webView2);

            _webView2.Source = new Uri("https://www.microsoft.com");
        }
    }
}
