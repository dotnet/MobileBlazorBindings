using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.MobileBlazorBindings.HostingNew;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public class BlazorWebView : Control
    {
        private const string webViewTemplateChildName = "WebView";
        private readonly BlazorWebViewCore _core = new();
        private WebView2 _webview;
        private CoreWebView2Environment _webviewEnvironment;

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

            _core.SetHostPage(HostPage);
            _webview = (WebView2)GetTemplateChild(webViewTemplateChildName);

            Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    _webviewEnvironment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);
                    await _webview.EnsureCoreWebView2Async(_webviewEnvironment).ConfigureAwait(true);

                    _webview.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
                    _webview.CoreWebView2.WebResourceRequested += HandleWebResourceRequested;
                    _webview.Source = new Uri(new Uri($"https://{_core.ContentHost}/"), _core.HostPageRelativeUrl);
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

        private void HandleWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs args)
        {
            if (_core.TryGetResponseContent(new Uri(args.Request.Uri), out var statusCode, out var statusMessage, out var content, out var headers))
            {
                args.Response = _webviewEnvironment.CreateWebResourceResponse(
                        content,
                        statusCode,
                        statusMessage,
                        headers);
            }
        }
    }
}
