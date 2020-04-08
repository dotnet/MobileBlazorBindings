using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements
{
    public class BlazorWebView : XF.ContentView
    {
        private readonly WebViewExtended _webView = new WebViewExtended();
        private bool _hasInitialized;

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
                            <app>Loading...</app>
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
        }

        // TODO: This isn't the right way to trigger the init, because it wouldn't happen naturally if consuming
        // BlazorWebView directly from Xamaring Forms XAML. It only works from MBB.
        public void Init(IServiceProvider services, Dispatcher dispatcher)
        {
            if (_hasInitialized)
            {
                throw new InvalidOperationException($"This {GetType().FullName} instance has already initialized.");
            }

            _hasInitialized = true;
            StartConnectionHandshake(services, dispatcher);
            _webView.Source = new XF.UrlWebViewSource { Url = $"{BlazorAppScheme}://app/" };
        }

        private void StartConnectionHandshake(IServiceProvider services, Dispatcher dispatcher)
        {
            var ipc = new IPC(_webView);
            ipc.Once("components:init", args =>
            {
                var argsArray = (object[])args;
                var initialUriAbsolute = ((JsonElement)argsArray[0]).GetString();
                var baseUriAbsolute = ((JsonElement)argsArray[1]).GetString();

                ContinueConnectionAfterHandshake(ipc, services, dispatcher);
            });
        }

        private void ContinueConnectionAfterHandshake(IPC ipc, IServiceProvider services, Dispatcher dispatcher)
        {
            var jsRuntime = new DesktopJSRuntime(ipc);

            ipc.On("BeginInvokeDotNetFromJS", args =>
            {
                var argsArray = (object[])args;
                DotNetDispatcher.BeginInvokeDotNet(
                    jsRuntime,
                    new DotNetInvocationInfo(
                        assemblyName: ((JsonElement)argsArray[1]).GetString(),
                        methodIdentifier: ((JsonElement)argsArray[2]).GetString(),
                        dotNetObjectId: ((JsonElement)argsArray[3]).GetInt64(),
                        callId: ((JsonElement)argsArray[0]).GetString()),
                    ((JsonElement)argsArray[4]).GetString());
            });

            ipc.On("EndInvokeJSFromDotNet", args =>
            {
                var argsArray = (object[])args;
                DotNetDispatcher.EndInvokeJS(
                    jsRuntime,
                    ((JsonElement)argsArray[2]).GetString());
            });

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var desktopRenderer = new DesktopRenderer(ipc, services, loggerFactory, jsRuntime, dispatcher);

            desktopRenderer.RootRenderHandle.Render(builder =>
            {
                builder.AddContent(0, "This is technically a Blazor component");
            });
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
