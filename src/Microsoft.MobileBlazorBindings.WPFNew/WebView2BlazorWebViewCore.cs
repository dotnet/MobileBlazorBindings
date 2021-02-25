using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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

        public WebView2BlazorWebViewCore(WebView2 webview, IServiceProvider services, Dispatcher dispatcher, string hostPageFilePath)
            : base(services, dispatcher, hostPageFilePath)
        {
            _webview = webview;
        }

        public override async Task StartAsync()
        {
            _webviewEnvironment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);
            await _webview.EnsureCoreWebView2Async(_webviewEnvironment).ConfigureAwait(true);
            await _webview.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };").ConfigureAwait(true);
            _webview.CoreWebView2.WebMessageReceived += (sender, eventArgs) => ReceiveIpcMessage(eventArgs.Source, eventArgs.WebMessageAsJson);

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

        protected override ValueTask SendIpcMessageAsync(string message)
        {
            // TODO: Consider requiring that the message string is a valid JSON string,
            // so that we could use PostWebMessageAsJson instead. It's possible this will
            // result in less string manipulation on the receiving end.
            _webview.CoreWebView2.PostWebMessageAsString(message);
            return ValueTask.CompletedTask;
        }
    }
}
