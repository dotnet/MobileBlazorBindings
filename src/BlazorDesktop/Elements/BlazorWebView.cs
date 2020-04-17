using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements
{
    public class BlazorWebView : XF.ContentView, IDisposable
    {
        private readonly Dispatcher _dispatcher;
        private readonly WebViewExtended _webView;
        private readonly IPC _ipc;
        private readonly JSRuntime _jsRuntime;
        private readonly bool _initOnParentSet;
        private readonly static RenderFragment EmptyRenderFragment = builder => { };
        private Task<InteropHandshakeResult> _attachInteropTask;
        private IServiceScope _serviceScope;
        private DesktopRenderer _desktopRenderer;
        private DesktopNavigationManager _navigationManager;

        public string ContentRoot { get; set; }
        public IServiceProvider Services { get; set; }
        public string ComponentType { get; set; }

        // Use this if no Services was supplied
        private static Lazy<IServiceProvider> DefaultServices = new Lazy<IServiceProvider>(() =>
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddBlazorDesktop();
            return serviceCollection.BuildServiceProvider();
        });

        // This is the constructor used when created from XAML
        // so we know we have to init as soon as the parent is set
        public BlazorWebView()
            : this(XamarinDeviceDispatcher.Instance, initOnParentSet: true)
        {
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (_initOnParentSet)
            {
                // TODO: Report errors
                _ = InitForXamlInstanceAsync();
            }
        }

        private async Task InitForXamlInstanceAsync()
        {
            try
            {
                await XamarinDeviceDispatcher.Instance.InvokeAsync(async () =>
                {
                    await InitAsync();
                    var componentType = Type.GetType(ComponentType, true);
                    if (!typeof(IComponent).IsAssignableFrom(componentType))
                    {
                        throw new ArgumentException($"The type '{ComponentType}' does not implement '{typeof(IComponent).FullName}'.");
                    }

                    Render(builder =>
                    {
                        builder.OpenComponent(0, componentType);
                        builder.CloseComponent();
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        protected BlazorWebView(Dispatcher dispatcher, bool initOnParentSet)
        {
            _initOnParentSet = initOnParentSet;
            Content = _webView = new WebViewExtended();
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

            _webView.SchemeHandlers.Add(BlazorAppScheme, (string url, out string contentType) =>
            {
                var uri = new Uri(url);
                if (uri.Host.Equals("0.0.0.0", StringComparison.Ordinal))
                {
                    // TODO: Prevent directory traversal?
                    var contentRootAbsolute = Path.GetFullPath(ContentRoot ?? ".");
                    var appFile = Path.Combine(contentRootAbsolute, uri.AbsolutePath.Substring(1));
                    if (appFile == contentRootAbsolute)
                    {
                        contentType = "text/html";
                        var indexHtmlPath = Path.Combine(contentRootAbsolute, "index.html");
                        return File.Exists(indexHtmlPath)
                            ? (Stream)File.OpenRead(indexHtmlPath)
                            // Use default HTML if none was provided in ContentRoot
                            : new MemoryStream(Encoding.UTF8.GetBytes(@"<!DOCTYPE html>
                                <html>
                                <head>
                                    <meta charset=""utf-8"" />
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"" />
                                    <base href=""/"" />
                                    <style type=""text/css"">
                                        #blazor-error-ui { background: lightyellow; bottom: 0; box-shadow: 0 -1px 2px rgba(0, 0, 0, 0.2); display: none; left: 0; padding: 0.6rem 1.25rem 0.7rem 1.25rem; position: fixed; width: 100%; z-index: 1000; }
                                        #blazor-error-ui .dismiss { cursor: pointer; position: absolute; right: 0.75rem; top: 0.5rem; }
                                    </style>
                                </head>
                                <body>
                                    <app></app>
                                    <div id=""blazor-error-ui"">
                                        An unhandled error has occurred.
                                        <a href="""" class=""reload"">Reload</a>
                                        <a class=""dismiss"">🗙</a>
                                    </div>
                                    <script src=""framework://blazor.desktop.js""></script>
                                </body>
                                </html>
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
        }

        // TODO: This isn't the right way to trigger the init, because it wouldn't happen naturally if consuming
        // BlazorWebView directly from Xamaring Forms XAML. It only works from MBB.
        internal async Task InitAsync()
        {
            _attachInteropTask ??= AttachInteropAsync();
            var handshakeResult = await _attachInteropTask;

            var services = Services ?? DefaultServices.Value;
            _serviceScope = services.CreateScope();

            var scopeServiceProvider = _serviceScope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            _navigationManager = (DesktopNavigationManager)scopeServiceProvider.GetRequiredService<NavigationManager>();
            _navigationManager.Initialize(_jsRuntime, handshakeResult.BaseUri, handshakeResult.InitialUri);
            _desktopRenderer = new DesktopRenderer(_ipc, scopeServiceProvider, loggerFactory, _jsRuntime, _dispatcher);
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

        private Task<InteropHandshakeResult> AttachInteropAsync()
        {
            var resultTcs = new TaskCompletionSource<InteropHandshakeResult>();

            // These hacks can go away once there's a proper IPC channel for event notifications etc.
            var selfAsDotNetObjectReference = typeof(DotNetObjectReference).GetMethod(nameof(DotNetObjectReference.Create))
                .MakeGenericMethod(GetType())
                .Invoke(null, new[] { this });
            var selfAsDotnetObjectReferenceId = (long)typeof(JSRuntime).GetMethod("TrackObjectReference", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(GetType())
                .Invoke(_jsRuntime, new[] { selfAsDotNetObjectReference });

            _ipc.Once("components:init", args =>
            {
                var argsArray = (object[])args;
                var initialUriAbsolute = ((JsonElement)argsArray[0]).GetString();
                var baseUriAbsolute = ((JsonElement)argsArray[1]).GetString();
                resultTcs.TrySetResult(new InteropHandshakeResult(baseUriAbsolute, initialUriAbsolute));
            });

            _ipc.On("BeginInvokeDotNetFromJS", args =>
            {
                var argsArray = (object[])args;
                var assemblyName = ((JsonElement)argsArray[1]).GetString();
                var methodIdentifier = ((JsonElement)argsArray[2]).GetString();
                var dotNetObjectId = ((JsonElement)argsArray[3]).GetInt64();
                var callId = ((JsonElement)argsArray[0]).GetString();
                var argsJson = ((JsonElement)argsArray[4]).GetString();

                // As a temporary hack, intercept blazor.desktop.js's JS interop calls for event notifications,
                // and direct them to our own instance. This is to avoid needing a static DesktopRenderer.Instance.
                // Similar temporary hack for navigation notifications
                // TODO: Change blazor.desktop.js to use a dedicated IPC call for these calls, not JS interop.
                if (assemblyName == "WebWindow.Blazor")
                {
                    assemblyName = null;
                    dotNetObjectId = selfAsDotnetObjectReferenceId;
                }

                DotNetDispatcher.BeginInvokeDotNet(
                    _jsRuntime,
                    new DotNetInvocationInfo(assemblyName, methodIdentifier, dotNetObjectId, callId),
                    argsJson);
            });

            _ipc.On("EndInvokeJSFromDotNet", args =>
            {
                var argsArray = (object[])args;
                DotNetDispatcher.EndInvokeJS(
                    _jsRuntime,
                    ((JsonElement)argsArray[2]).GetString());
            });

            _webView.Source = new XF.UrlWebViewSource { Url = $"{BlazorAppScheme}://0.0.0.0/" };

            return resultTcs.Task;
        }

        [JSInvokable(nameof(DispatchEvent))]
        public async Task DispatchEvent(WebEventDescriptor eventDescriptor, string eventArgsJson)
        {
            var webEvent = WebEventData.Parse(eventDescriptor, eventArgsJson);
            await _desktopRenderer.DispatchEventAsync(
                webEvent.EventHandlerId,
                webEvent.EventFieldInfo,
                webEvent.EventArgs);
        }

        [JSInvokable(nameof(NotifyLocationChanged))]
        public void NotifyLocationChanged(string uri, bool isInterceptedLink)
        {
            _navigationManager.SetLocation(uri, isInterceptedLink);
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

        public void Dispose()
        {
            _serviceScope.Dispose();
        }

        private class InteropHandshakeResult
        {
            public string BaseUri { get; }
            public string InitialUri { get; }

            public InteropHandshakeResult(string baseUri, string initialUri)
            {
                BaseUri = baseUri;
                InitialUri = initialUri;
            }
        }
    }
}
