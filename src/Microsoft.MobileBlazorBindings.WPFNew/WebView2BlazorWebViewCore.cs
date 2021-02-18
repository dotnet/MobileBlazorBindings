using System;
using System.Threading.Tasks;
using Microsoft.MobileBlazorBindings.HostingNew;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    // A BlazorWebViewCore subclass that is wired up to a WebView2 instance
    // This could be shared across all platforms that use WebView2 (not just WPF)
    class WebView2BlazorWebViewCore : BlazorWebViewCore
    {
        private readonly WebView2 _webview;
        private CoreWebView2Environment _webviewEnvironment;

        public WebView2BlazorWebViewCore(WebView2 webview, string hostPageFilePath)
            : base(hostPageFilePath)
        {
            _webview = webview;
        }

        public override async Task StartAsync()
        {
            _webviewEnvironment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);
            await _webview.EnsureCoreWebView2Async(_webviewEnvironment).ConfigureAwait(true);

            _webview.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
            _webview.CoreWebView2.WebResourceRequested += (sender, args) =>
            {
                if (TryGetResponseContent(new Uri(args.Request.Uri), out var statusCode, out var statusMessage, out var content, out var headers))
                {
                    args.Response = _webviewEnvironment.CreateWebResourceResponse(
                            content,
                            statusCode,
                            statusMessage,
                            headers);
                }
            };

            await base.StartAsync().ConfigureAwait(true);
        }

        protected override void Navigate(Uri uri)
        {
            _webview.Source = uri;
        }
    }
}
