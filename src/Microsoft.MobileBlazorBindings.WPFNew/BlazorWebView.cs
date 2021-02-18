using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public class BlazorWebView : Control
    {
        private const string webViewTemplateChildName = "WebView";
        private static readonly FileExtensionContentTypeProvider FileExtensionContentTypeProvider = new();
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

        private string _contentRoot;
        private string _hostPageRelativeUrl;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _webview = (WebView2)GetTemplateChild(webViewTemplateChildName);

            var hostPageAbsolute = Path.GetFullPath(HostPage);
            _contentRoot = Path.GetDirectoryName(hostPageAbsolute);
            _hostPageRelativeUrl = Path.GetRelativePath(_contentRoot, hostPageAbsolute).Replace(Path.DirectorySeparatorChar, '/');

            Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    _webviewEnvironment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);
                    await _webview.EnsureCoreWebView2Async(_webviewEnvironment).ConfigureAwait(true);

                    _webview.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
                    _webview.CoreWebView2.WebResourceRequested += HandleWebResourceRequested;
                    _webview.Source = new Uri(new Uri("https://app/"), _hostPageRelativeUrl);
                }
                catch (Exception ex)
                {
                    // TODO: Figure out how to ensure any exceptions propagate to a central handler
                    // without needing this try/catch
                    MessageBox.Show(ex.ToString());
                }
            });
        }

        private void HandleWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs args)
        {
            var request = args.Request;
            var requestUri = new Uri(request.Uri);
            if (string.Equals(requestUri.Host, "app", StringComparison.Ordinal))
            {
                var filePath = Path.GetFullPath(Path.Combine(_contentRoot, requestUri.GetComponents(UriComponents.Path, UriFormat.Unescaped)));
                if (filePath.StartsWith(_contentRoot, StringComparison.Ordinal)
                    && File.Exists(filePath))
                {
                    var responseContentType = FileExtensionContentTypeProvider.TryGetContentType(filePath, out var matchedContentType)
                        ? matchedContentType
                        : "application/octet-stream";

                    args.Response = _webviewEnvironment.CreateWebResourceResponse(
                        Content: File.OpenRead(filePath),
                        StatusCode: 200,
                        ReasonPhrase: "OK",
                        Headers: $"Content-Type: {responseContentType}{Environment.NewLine}Cache-Control: no-cache, max-age=0, must-revalidate, no-store");
                }
            }
        }
    }
}
