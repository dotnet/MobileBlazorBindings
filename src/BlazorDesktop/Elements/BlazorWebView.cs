using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements
{
    public class BlazorWebView : XF.ContentView
    {
        private readonly WebViewExtended _webView = new WebViewExtended();

        public BlazorWebView()
        {
            Content = _webView;

            var contentRootAbsolute = Path.GetDirectoryName(Path.GetFullPath("."));

            _webView.SchemeHandlers.Add(BlazorAppScheme, (string url, out string contentType) =>
            {
                var uri = new Uri(url);
                if (uri.Host.Equals("app", StringComparison.Ordinal))
                {
                    // TODO: Prevent directory traversal?
                    var appFile = Path.Combine(contentRootAbsolute, uri.AbsolutePath.Substring(1));
                    if (appFile == contentRootAbsolute)
                    {
                        contentType = "text/html";
                        return new MemoryStream(Encoding.UTF8.GetBytes(@"
                            Hello, this is from the BlazorWebView.
                            <script src='framework://blazor.desktop.js'></script>
                        "));
                    }
                    else if (File.Exists(appFile))
                    {
                        contentType = GetContentType(appFile);
                        return File.OpenRead(appFile);
                    }
                }

                contentType = default;
                return null;
            });

            // framework:// is resolved as embedded resources
            _webView.SchemeHandlers.Add("framework", (string url, out string contentType) =>
            {
                contentType = GetContentType(url);
                return SupplyFrameworkFile(url);
            });

            // Wait for the JS-side code to connect
            var ipc = new IPC(_webView);
            ipc.Once("components:init", args =>
            {

            });

            _webView.Source = new XF.UrlWebViewSource { Url = $"{BlazorAppScheme}://app/" };
        }

        private static string BlazorAppScheme
        {
            get
            {
                // On Windows, we can't use a custom scheme to host the initial HTML,
                // because webview2 won't let you do top-level navigation to such a URL.
                // On Linux/Mac, we must use a custom scheme, because their webviews
                // don't have a way to intercept http:// scheme requests.
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "http"
                    : "app";
            }
        }

        private static string GetContentType(string url)
        {
            var ext = Path.GetExtension(url);
            switch (ext)
            {
                case ".html": return "text/html";
                case ".css": return "text/css";
                case ".js": return "text/javascript";
                case ".wasm": return "application/wasm";
            }
            return "application/octet-stream";
        }

        private static Stream SupplyFrameworkFile(string uri)
        {
            switch (uri)
            {
                case "framework://blazor.desktop.js":
                    return typeof(BlazorWebView).Assembly.GetManifestResourceStream("BlazorDesktop.blazor.desktop.js");
                default:
                    throw new ArgumentException($"Unknown framework file: {uri}");
            }
        }
    }
}
