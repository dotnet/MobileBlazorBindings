using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public sealed class BlazorWebView : Control, IDisposable
    {
        private const string webViewTemplateChildName = "WebView";
        private WebView2BlazorWebViewCore _core;

        public BlazorWebView()
        {
            Template = new ControlTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(WebView2), webViewTemplateChildName)
            };
        }

        public string HostPage { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var webview = (WebView2)GetTemplateChild(webViewTemplateChildName);

            // TODO: Can OnApplyTemplate get called multiple times? Do we need to handle this more efficiently?
            _core?.Dispose();
            _core = new WebView2BlazorWebViewCore(webview, HostPage);

            Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    await _core.StartAsync().ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    // TODO: Figure out how to ensure any exceptions propagate to a central handler
                    // without needing this try/catch
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            });
        }

        public void Dispose()
        {
            // TODO: Determine correct disposal pattern for WPF
            _core?.Dispose();
        }
    }
}
