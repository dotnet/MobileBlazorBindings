using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements
{
    public class BlazorWebView : XF.ContentView
    {
        private readonly Dispatcher _dispatcher;
        private readonly WebViewExtended _webView;
        private readonly Task _attachInteropTask;
        private readonly IPC _ipc;
        private readonly JSRuntime _jsRuntime;
        private readonly static RenderFragment EmptyRenderFragment = builder => { };
        private DesktopRenderer _desktopRenderer;

        public BlazorWebView(Dispatcher dispatcher)
        {
            Content = _webView = new WebViewExtended();
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

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

            _ipc = new IPC(_webView);
            _jsRuntime = new DesktopJSRuntime(_ipc);
            _attachInteropTask = AttachInteropAsync();
        }

        // TODO: This isn't the right way to trigger the init, because it wouldn't happen naturally if consuming
        // BlazorWebView directly from Xamaring Forms XAML. It only works from MBB.
        internal async Task InitAsync(IServiceProvider services)
        {
            await _attachInteropTask;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            _desktopRenderer = new DesktopRenderer(_ipc, services, loggerFactory, _jsRuntime, _dispatcher);
        }

        // TODO: This is also not the right way to trigger a render, as you wouldn't be able to call this if consuming
        // BlazorWebView directly from Xamaring Forms XAML. It only works from MBB.
        internal void Render(RenderFragment fragment)
        {
            if (_desktopRenderer == null)
            {
                throw new InvalidOperationException($"{nameof(Render)} was called before {nameof(InitAsync)}");
            }

            _desktopRenderer.RootRenderHandle.Render(fragment ?? EmptyRenderFragment);
        }

        private Task AttachInteropAsync()
        {
            var resultTcs = new TaskCompletionSource<bool>();
            _ipc.Once("components:init", args =>
            {
                var argsArray = (object[])args;
                var initialUriAbsolute = ((JsonElement)argsArray[0]).GetString();
                var baseUriAbsolute = ((JsonElement)argsArray[1]).GetString();
                resultTcs.TrySetResult(true);
            });

            _ipc.On("BeginInvokeDotNetFromJS", args =>
            {
                var argsArray = (object[])args;
                DotNetDispatcher.BeginInvokeDotNet(
                    _jsRuntime,
                    new DotNetInvocationInfo(
                        assemblyName: ((JsonElement)argsArray[1]).GetString(),
                        methodIdentifier: ((JsonElement)argsArray[2]).GetString(),
                        dotNetObjectId: ((JsonElement)argsArray[3]).GetInt64(),
                        callId: ((JsonElement)argsArray[0]).GetString()),
                    ((JsonElement)argsArray[4]).GetString());
            });

            _ipc.On("EndInvokeJSFromDotNet", args =>
            {
                var argsArray = (object[])args;
                DotNetDispatcher.EndInvokeJS(
                    _jsRuntime,
                    ((JsonElement)argsArray[2]).GetString());
            });

            _webView.Source = new XF.UrlWebViewSource { Url = $"{BlazorAppScheme}://app/" };

            return resultTcs.Task;
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
