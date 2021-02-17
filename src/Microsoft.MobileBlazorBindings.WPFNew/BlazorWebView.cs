using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public class BlazorWebView : Control
    {
        private const string webViewTemplateChildName = "WebView";
        private WebView2 _webview;

        public BlazorWebView()
        {
            Template = new ControlTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(WebView2), webViewTemplateChildName)
            };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _webview = (WebView2)GetTemplateChild(webViewTemplateChildName);

            Dispatcher.InvokeAsync(async () =>
            {
                // TODO: Figure out how to ensure any exceptions propagate from here.
                // Currently they are just lost.
                await _webview.EnsureCoreWebView2Async().ConfigureAwait(true);
                _webview.Source = new System.Uri("https://microsoft.com");
            });
        }
    }
}
