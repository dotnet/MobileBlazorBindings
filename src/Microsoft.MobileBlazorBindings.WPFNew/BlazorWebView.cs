using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.MobileBlazorBindings.HostingNew;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public sealed class BlazorWebView : Control, IDisposable
    {
        private const string webViewTemplateChildName = "WebView";
        private BlazorWebViewCore _core;
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
            _webview = (WebView2)GetTemplateChild(webViewTemplateChildName);

            // TODO: Can OnApplyTemplate get called multiple times? Do we need to handle this more efficiently?
            _core?.Dispose();

            _core = new BlazorWebViewCore(HostPage);
            _core.OnNavigate += (sender, uri) => _webview.Source = uri;

            Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    _webviewEnvironment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);
                    await _webview.EnsureCoreWebView2Async(_webviewEnvironment).ConfigureAwait(true);

                    _webview.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
                    _webview.CoreWebView2.WebResourceRequested += HandleWebResourceRequested;
                    _core.Start();
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
            // TODO: Find correct disposal pattern within WPF, ideally with IAsyncDisposable
            _core?.Dispose();
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
