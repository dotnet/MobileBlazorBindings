// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.MobileBlazorBindings.Hosting;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.MobileBlazorBindings.WPF
{
    public delegate Stream ResolveWebResourceDelegate(string url, out string contentType);

#pragma warning disable CA1001 // Types that own disposable fields should be disposable; WPF controls don't have a standard dispose pattern, so for now this is not disposable
    public class BlazorWebView : Control, IWebViewIPCAdapter
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private WebView2 _webView2;
        private CoreWebView2Environment _coreWebView2Environment;

        public WebView2 WebView => _webView2;

        public static readonly DependencyProperty ComponentTypeProperty =
            DependencyProperty.Register(
                name: "ComponentType",
                propertyType: typeof(Type),
                ownerType: typeof(BlazorWebView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: new PropertyChangedCallback(OnComponentTypeChanged))
            );

        public Type ComponentType
        {
            get { return (Type)GetValue(ComponentTypeProperty); }
            set { SetValue(ComponentTypeProperty, value); }
        }

        private static void OnComponentTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: How to handle this?
        }

        public BlazorWebView() : this(WPFDispatcher.Instance, initOnParentSet: true)
        {
            var template = new ControlTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(WebView2), "WebView2")
            };

            Template = template;
        }

        private const string LoadBlazorJSScript =
    "window.onload = (function blazorInitScript() {" +
    "    var blazorScript = document.createElement('script');" +
    "    blazorScript.src = 'framework://blazor.desktop.js';" +
    "    document.head.appendChild(blazorScript);" +
    "});";


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _webView2 = (WebView2)GetTemplateChild("WebView2");

            OnParentSet();

            Dispatcher.InvokeAsync(async () =>
            {
                _coreWebView2Environment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);

                await _webView2.EnsureCoreWebView2Async(_coreWebView2Environment).ConfigureAwait(true);

                await _webView2.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };").ConfigureAwait(true);
                await _webView2.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(LoadBlazorJSScript).ConfigureAwait(true);
                SubscribeToControlEvents();

                Load();

                SubscribeToElementEvents();

            });
        }

        private void SubscribeToElementEvents()
        {
            SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
        }

        private void SubscribeToControlEvents()
        {
            _webView2.NavigationStarting += HandleNavigationStarting;
            _webView2.NavigationCompleted += HandleNavigationCompleted;
            _webView2.WebMessageReceived += HandleWebMessageReceived;
            _webView2.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
            _webView2.CoreWebView2.WebResourceRequested += HandleWebResourceRequested;
        }

        private void HandleNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //if (e.NavigationId == _navigationId)
            //{
            //    Element.HandleNavigationFinished(_currentUri);
            //}
        }

        private void HandleNavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            //_navigationId = e.NavigationId;
            //_currentUri = new Uri(e.Uri);

            // We stop blazor when we navigate away
            // and we restart it if our host is 0.0.0.0 again.
            Stop();
            if (new Uri(e.Uri).Host.Equals("0.0.0.0", StringComparison.Ordinal))
            {
                Start();
                WPFDispatcher.Instance.InvokeAsync(async () =>
                {
                    await InitAsync().ConfigureAwait(false);
                    RerenderAction?.Invoke();
                });
            }
        }

        private void OnSendMessageFromJSToDotNetRequested(object sender, string message)
        {
            _webView2.CoreWebView2.PostWebMessageAsString(message);
        }

        private void HandleWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            HandleWebMessageReceived(args.TryGetWebMessageAsString());
        }

        private void HandleWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs args)
        {
            var uriString = args.Request.Uri;
            var uri = new Uri(uriString);
            if (SchemeHandlers.TryGetValue(uri.Scheme, out var handler) && _coreWebView2Environment != null)
            {
                var responseStream = handler(uriString, out var responseContentType);
                if (responseStream != null) // If null, the handler doesn't want to handle it
                {
                    responseStream.Position = 0;
                    args.Response = _coreWebView2Environment.CreateWebResourceResponse(responseStream, StatusCode: 200, ReasonPhrase: "OK", Headers: $"Content-Type: {responseContentType}{Environment.NewLine}Cache-Control: no-cache, max-age=0, must-revalidate, no-store");
                }
            }
        }

        private string _webViewSource;

        private void Load()
        {
            if (_webViewSource != null && _webView2?.CoreWebView2 != null)
            {
                _webView2.CoreWebView2.Navigate(_webViewSource);
            }
        }


        // From BlazorWebView


        private static readonly FileExtensionContentTypeProvider FileExtensionContentTypeProvider = new();
        private static readonly RenderFragment EmptyRenderFragment = builder => { };

        private readonly Dispatcher _dispatcher;
        private readonly bool _initOnParentSet;

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

        private void OnParentSet()
        {
            // Need to be careful when we read the ComponentType property because it must
            // be read on the correct thread. So we capture it here to construct the generic
            // method, then later use the constructed generic method in the render fragment.
            var openComponentUnconstructedMethod = typeof(RenderTreeBuilder).GetMethod(nameof(RenderTreeBuilder.OpenComponent), genericParameterCount: 1, types: new[] { typeof(int) });
            var openComponentMethod = openComponentUnconstructedMethod.MakeGenericMethod(ComponentType);

            if (_initOnParentSet)
            {
                // TODO: Report errors
                RerenderAction = () =>
                {
                    _ = Render(builder =>
                    {
                        try
                        {
                            // Call this with generic method with a Type instance:
                            //      TComponent builder.OpenComponent<TComponent>(0);
                            openComponentMethod.Invoke(builder, new object[] { 0 });

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
            _webViewSource = $"{BlazorAppScheme}://0.0.0.0/";
        }

        private BlazorWebView(Dispatcher dispatcher, bool initOnParentSet)
        {
            _initOnParentSet = initOnParentSet;
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

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

            SchemeHandlers.Add(BlazorAppScheme, (string url, out string contentType) =>
            {
                var environment = RootServiceProvider.GetRequiredService<IHostEnvironment>();
                var fileProvider = RootServiceProvider.GetRequiredService<IFileProvider>();

                var uri = new Uri(url);
                if (uri.Host.Equals("0.0.0.0", StringComparison.Ordinal))
                {

                    if (TryGetFile(fileProvider, GetResourceFilenameFromUri(uri), out var fileStream))
                    {
                        contentType = GetContentType(uri.AbsolutePath[1..]);
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
            SchemeHandlers.Add("framework", (string url, out string contentType) =>
            {
                contentType = GetContentType(url);
                return SupplyFrameworkFile(url);
            });

            Start();
        }

        private void Start()
        {
            _ipc = new WebViewIPC(this);
        }

        private static string GetResourceFilenameFromUri(Uri uri)
        {
            return Uri.UnescapeDataString(uri.AbsolutePath[1..]);
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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously; this method is supposed to be async but the await'ed call below isn't working right now (see comment below)
        public async Task Render(RenderFragment fragment)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            BlazorHybridRenderer capturedRender;

            if ((capturedRender = _blazorHybridRenderer) == null || capturedRender.Dispatcher == null)
            {
                // we used to throw an exception here, but the webview could have navigated away from a Blazor page,
                // in which case this should just do nothing.
                return;
            }

            // TODO: For some reason calling Render using Dispatcher.InvokeAsync() doesn't work. The render fragment never gets called
            capturedRender.RootRenderHandle.Render(fragment ?? EmptyRenderFragment);
            //await capturedRender.Dispatcher.InvokeAsync(() => capturedRender.RootRenderHandle.Render(fragment ?? EmptyRenderFragment)).ConfigureAwait(false);
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
                return "http";
            }
        }

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

        // TODO: There isn't a standard disposal pattern for WPF controls. So how/when do we do the disposal?
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        Stop();
        //        if (_webView != null)
        //        {
        //            _webView.OnNavigationStarting -= HandleNavigationStarting;
        //            _webView.OnNavigationFinished -= HandleNavigationFinished;
        //            _webView = null;
        //            Content = null;
        //        }
        //    }
        //}

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


        // From WebViewExtended

        public EventHandler<string> OnWebMessageReceived { get; set; }
        public EventHandler<string> SendMessageFromJSToDotNetRequested { get; set; }

        // Unfortunately since the orginal Navigating and Navigated event invoke methods are internal
        // the events cannot be invoked and we have to duplicate those.
        public EventHandler<Uri> OnNavigationStarting { get; set; }

        public EventHandler<Uri> OnNavigationFinished { get; set; }

        public IDictionary<string, ResolveWebResourceDelegate> SchemeHandlers { get; }
            = new Dictionary<string, ResolveWebResourceDelegate>();

        public IBlazorErrorHandler ErrorHandler { get; }

        public void HandleWebMessageReceived(string webMessageAsString)
        {
            OnWebMessageReceived?.Invoke(this, webMessageAsString);
        }

        public void SendMessage(string message)
        {
            SendMessageFromJSToDotNetRequested?.Invoke(this, message);
        }

        public void BeginInvoke(Action action)
        {
            Dispatcher.BeginInvoke(action);
        }
    }
}
