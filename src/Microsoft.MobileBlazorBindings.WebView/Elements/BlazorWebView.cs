// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.MobileBlazorBindings.Hosting;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

[assembly: InternalsVisibleTo("Microsoft.MobileBlazorBindings")]

namespace Microsoft.MobileBlazorBindings.WebView.Elements
{
    public class BlazorWebView<TComponent> : XF.ContentView, IDisposable, IWebViewIPCAdapter where TComponent : IComponent
    {
        private static readonly FileExtensionContentTypeProvider FileExtensionContentTypeProvider = new();
        private static readonly RenderFragment EmptyRenderFragment = builder => { };

        private readonly Dispatcher _dispatcher;
        private readonly bool _initOnParentSet;

        private WebViewExtended _webView;
        private WebViewIPC _ipc;
        private JSRuntime _jsRuntime;
        private IServiceScope _serviceScope;
        private BlazorHybridRenderer _blazorHybridRenderer;
        private BlazorHybridNavigationManager _navigationManager;

        internal Action RerenderAction { get; set; }

        public IHost Host { get; set; }

        /// <summary>
        /// The element selector for the root component. The default value is <c>app</c>, representing a component named <code>&lt;app&gt;...&lt;/app&gt;</code>
        /// </summary>
        public string RootComponentElementSelector { get; set; } = "app";

        // Use this if no Services was supplied
        private static readonly Lazy<IServiceProvider> DefaultServices = new(() =>
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddBlazorHybrid();
            return serviceCollection.BuildServiceProvider();
        });

        /// <summary>
        /// Get the non-scoped root service provider.
        /// </summary>
        private IServiceProvider RootServiceProvider => Host?.Services ?? DefaultServices.Value;

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
                RerenderAction = () =>
                {
                    _ = Render(builder =>
                    {
                        try
                        {
                            builder.OpenComponent<TComponent>(0);
                            builder.CloseComponent();
#pragma warning disable CA1031 // Do not catch general exception types
                        }
                        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
                        {
                            ErrorHandler.HandleException(ex);
                        }
                    });
                };

                SetInitialSource();
            }
        }

        internal void SetInitialSource()
        {
            _webView.Source = new XF.UrlWebViewSource() { Url = $"{BlazorAppScheme}://0.0.0.0/" };
        }

        protected BlazorWebView(Dispatcher dispatcher, bool initOnParentSet)
        {
            _initOnParentSet = initOnParentSet;
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

            _webView = new WebViewExtended(this.ErrorHandler);

            _webView.OnNavigationStarting += HandleNavigationStarting;
            _webView.OnNavigationFinished += HandleNavigationFinished;

            static bool TryGetFile(IFileProvider fileProvider, string filename, out Stream fileStream)
            {
                var fileInfo = fileProvider.GetFileInfo(filename);
                if (fileInfo != null && fileInfo.Exists)
                {
                    fileStream = fileInfo.CreateReadStream();
                    return true;
                }
                fileStream = null;
                return false;
            }

            _webView.SchemeHandlers.Add(BlazorAppScheme, (string url, out string contentType) =>
            {
                var environment = RootServiceProvider.GetRequiredService<IHostEnvironment>();
                var fileProvider = RootServiceProvider.GetRequiredService<IFileProvider>();

                var uri = new Uri(url);
                if (uri.Host.Equals("0.0.0.0", StringComparison.Ordinal))
                {

                    if (TryGetFile(fileProvider, GetResourceFilenameFromUri(uri), out var fileStream))
                    {
                        contentType = GetContentType(uri.AbsolutePath.Substring(1));
                        return fileStream;
                    }
                    else
                    {
                        // We will always return the main page for files that are not found. This is similar
                        // to blazor fallback routing.
                        var contentRootAbsolute = Path.GetFullPath(environment.ContentRootPath ?? ".");

                        contentType = "text/html";
                        const string IndexHtmlFilename = "index.html";
                        var indexHtmlPath = Path.Combine(contentRootAbsolute, IndexHtmlFilename);

                        if (TryGetFile(fileProvider, IndexHtmlFilename, out var indexStream))
                        {
                            return indexStream;
                        }
                        else
                        {
                            // Use default HTML if none was provided in ContentRoot
                            return new MemoryStream(Encoding.UTF8.GetBytes(@"<!DOCTYPE html>
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
                                </body>
                                </html>
                                "));
                        }
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

            Content = _webView;

            Start();
        }

        private void Start()
        {
            _ipc = new WebViewIPC(this);
        }

        private void HandleNavigationStarting(object sender, Uri e)
        {
            // We stop blazor when we navigate away
            // and we restart it if our host is 0.0.0.0 again.
            Stop();
            if (e.Host.Equals("0.0.0.0", StringComparison.Ordinal))
            {
                Start();
                XamarinDeviceDispatcher.Instance.InvokeAsync(async () =>
                {
                    await InitAsync().ConfigureAwait(false);
                    RerenderAction?.Invoke();
                });
            }
        }

        private void HandleNavigationFinished(object sender, Uri e)
        {
        }

        private static string GetResourceFilenameFromUri(Uri uri)
        {
            return Uri.UnescapeDataString(uri.AbsolutePath.Substring(1));
        }

        // TODO: This isn't the right way to trigger the init, because it wouldn't happen naturally if consuming
        // BlazorWebView directly from Xamarin Forms XAML. It only works from MBB.
        public async Task InitAsync()
        {
            var services = RootServiceProvider;
            _serviceScope = services.CreateScope();
            var scopeServiceProvider = _serviceScope.ServiceProvider;

            var webViewJSRuntime = (BlazorHybridJSRuntime)scopeServiceProvider.GetRequiredService<IJSRuntime>();
            webViewJSRuntime.AttachToIpcChannel(_ipc);
            _jsRuntime = webViewJSRuntime;

            var handshakeResult = await _ipc.AttachInteropAsync(_jsRuntime).ConfigureAwait(false);

            var loggerFactory = scopeServiceProvider.GetRequiredService<ILoggerFactory>();
            _navigationManager = (BlazorHybridNavigationManager)scopeServiceProvider.GetRequiredService<NavigationManager>();
            _navigationManager.Initialize(_jsRuntime, handshakeResult.BaseUri, handshakeResult.InitialUri);
            _blazorHybridRenderer = new BlazorHybridRenderer(_ipc, scopeServiceProvider, loggerFactory, _jsRuntime, _dispatcher, ErrorHandler, RootComponentElementSelector);
        }

        // TODO: This is also not the right way to trigger a render, as you wouldn't be able to call this if consuming
        // BlazorWebView directly from Xamarin Forms XAML. It only works from MBB.
        public async Task Render(RenderFragment fragment)
        {
            BlazorHybridRenderer capturedRender;

            if ((capturedRender = _blazorHybridRenderer) == null || capturedRender.Dispatcher == null)
            {
                // we used to throw an exception here, but the webview could have navigated away from a Blazor page,
                // in which case this should just do nothing.
                return;
            }

            await capturedRender.Dispatcher.InvokeAsync(() => capturedRender.RootRenderHandle.Render(fragment ?? EmptyRenderFragment)).ConfigureAwait(false);
        }

        public async Task DispatchEvent(WebEventDescriptor eventDescriptor, string eventArgsJson)
        {
            if (eventDescriptor is null)
            {
                throw new ArgumentNullException(nameof(eventDescriptor));
            }
            if (_blazorHybridRenderer == null)
            {
                // we were shut down, but we got a render completion from javascript afterwards.
                return;
            }

            await _blazorHybridRenderer.DispatchEventAsync(eventDescriptor, eventArgsJson).ConfigureAwait(false);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void NotifyLocationChanged(string uri, bool isInterceptedLink)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _navigationManager.SetLocation(uri, isInterceptedLink);
        }

        public async Task OnRenderCompleted(long renderId, string errorMessageOrNull)
        {
            if (_blazorHybridRenderer == null)
            {
                // we were shut down, but we got a render completion from javascript afterwards.
                return;
            }
            await _blazorHybridRenderer.OnRenderCompletedAsync(renderId, errorMessageOrNull).ConfigureAwait(false);
        }

        private static string BlazorAppScheme
        {
            get
            {
                // On Windows and Tizen, we can't use a custom scheme to host the initial HTML,
                // because webview2 won't let you do top-level navigation to such a URL.
                // On Linux/Mac, we must use a custom scheme, because their webviews
                // don't have a way to intercept http:// scheme requests.
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || XF.Device.RuntimePlatform == XF.Device.Tizen
                    ? "http"
                    : "app";
            }
        }

        public IBlazorErrorHandler ErrorHandler { get; set; }

        /// <summary>
        /// Gets the content type for the url.
        /// </summary>
        /// <param name="url">The url to use.</param>
        /// <returns>The content type.</returns>
        private static string GetContentType(string url)
        {
            if (FileExtensionContentTypeProvider.TryGetContentType(url, out var result))
            {
                return result;
            }

            return "application/octet-stream";
        }

        private static Stream SupplyFrameworkFile(string uri)
        {
            return uri switch
            {
                "framework://blazor.desktop.js" => BlazorAssets.GetBlazorDesktopJS(),
                _ => throw new ArgumentException($"Unknown framework file: {uri}"),
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
                if (_webView != null)
                {
                    _webView.OnNavigationStarting -= HandleNavigationStarting;
                    _webView.OnNavigationFinished -= HandleNavigationFinished;
                    _webView = null;
                    Content = null;
                }
            }
        }

        private void Stop()
        {
            if (_blazorHybridRenderer != null)
            {
                _blazorHybridRenderer.Dispose();
                _blazorHybridRenderer = null;
            }
            if (_serviceScope != null)
            {
                _serviceScope.Dispose();
                _serviceScope = null;
            }
            if (_ipc != null)
            {
                _ipc.Dispose();
                _ipc = null;
            }
        }

        public void SendMessage(string message)
        {
            _webView.SendMessage(message);
        }

        public void BeginInvoke(Action action)
        {
            _webView.Dispatcher.BeginInvokeOnMainThread(action);
        }

        public EventHandler<string> OnWebMessageReceived
        {
            get => _webView.OnWebMessageReceived;
            set => _webView.OnWebMessageReceived = value;
        }
    }
}
