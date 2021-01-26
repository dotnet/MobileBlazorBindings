﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.MobileBlazorBindings.Hosting;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfBlazorSample
{
    public delegate Stream ResolveWebResourceDelegate(string url, out string contentType);

    /// <summary>
    /// Custom dispatcher for WPF apps to ensure all UI work is done on the UI (main) thread.
    /// </summary>
    internal class WPFDeviceDispatcher : Dispatcher
    {
        public static WPFDeviceDispatcher Instance { get; } = new WPFDeviceDispatcher();

        public override bool CheckAccess()
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.CheckAccess();
        }

        public override Task InvokeAsync(Action workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Result;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Result;
        }
    }

    public class WpfBlazorWebView : Control
    {
        private WebView2 _webView2;
        private CoreWebView2Environment _coreWebView2Environment;

        public WebView2 WebView => _webView2;

        public static readonly DependencyProperty ComponentTypeProperty =
            DependencyProperty.Register(
                name: "ComponentType",
                propertyType: typeof(Type),
                ownerType: typeof(WpfBlazorWebView),
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
        }

        public WpfBlazorWebView() : this(WPFDeviceDispatcher.Instance, initOnParentSet: true)
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

            var x = GetTemplateChild("WebView2");
            _webView2 = (WebView2)x;

            //_webView.Source = new Uri("https://bing.com/");

            Dispatcher.InvokeAsync(async () =>
            {
                _coreWebView2Environment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);

                await _webView2.EnsureCoreWebView2Async(_coreWebView2Environment).ConfigureAwait(true);

                await _webView2.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };").ConfigureAwait(true);
                await _webView2.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(LoadBlazorJSScript).ConfigureAwait(true);
                SubscribeToControlEvents();

                OnParentSet();

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

            //Element.HandleNavigationStarting(_currentUri);
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

        private WPFIPC _ipc;
        private JSRuntime _jsRuntime;
        private Task<InteropHandshakeResult> _attachInteropTask;
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
            if (_initOnParentSet)
            {
                // TODO: Report errors
                RerenderAction = () =>
                {
                    _ = Render(builder =>
                    {
                        try
                        {
                            // Call this with non-generic:
                            //      TComponent builder.OpenComponent<TComponent>(0);
                            var openComponentUnconstructedMethod = typeof(RenderTreeBuilder).GetMethod(nameof(RenderTreeBuilder.OpenComponent), genericParameterCount: 1, types: new[] { typeof(int) });
                            var openComponentMethod = openComponentUnconstructedMethod.MakeGenericMethod(ComponentType);
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

        private WpfBlazorWebView(Dispatcher dispatcher, bool initOnParentSet)
        {
            _initOnParentSet = initOnParentSet;
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

            OnNavigationStarting += HandleNavigationStarting;
            OnNavigationFinished += HandleNavigationFinished;

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
            SchemeHandlers.Add("framework", (string url, out string contentType) =>
            {
                contentType = GetContentType(url);
                return SupplyFrameworkFile(url);
            });

            Start();
        }

        private void Start()
        {
            _ipc = new WPFIPC(this);
        }

        private void HandleNavigationStarting(object sender, Uri e)
        {
            // We stop blazor when we navigate away
            // and we restart it if our host is 0.0.0.0 again.
            Stop();
            if (e.Host.Equals("0.0.0.0", StringComparison.Ordinal))
            {
                Start();
                WPFDeviceDispatcher.Instance.InvokeAsync(async () =>
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

            _attachInteropTask ??= AttachInteropAsync();
            var handshakeResult = await _attachInteropTask.ConfigureAwait(false);

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
                var assemblyName = argsArray[1] != null ? ((JsonElement)argsArray[1]).GetString() : null;
                var methodIdentifier = ((JsonElement)argsArray[2]).GetString();
                var dotNetObjectId = ((JsonElement)argsArray[3]).GetInt64();
                var callId = ((JsonElement)argsArray[0]).GetString();
                var argsJson = ((JsonElement)argsArray[4]).GetString();

                // As a temporary hack, intercept blazor.desktop.js's JS interop calls for event notifications,
                // and direct them to our own instance. This is to avoid needing a static BlazorHybridRenderer.Instance.
                // Similar temporary hack for navigation notifications
                // TODO: Change blazor.desktop.js to use a dedicated IPC call for these calls, not JS interop.
                if (assemblyName == "Microsoft.MobileBlazorBindings.WebView")
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

            return resultTcs.Task;
        }

        [JSInvokable(nameof(DispatchEvent))]
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

        [JSInvokable(nameof(NotifyLocationChanged))]
#pragma warning disable CA1054 // Uri parameters should not be strings
        public void NotifyLocationChanged(string uri, bool isInterceptedLink)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _navigationManager.SetLocation(uri, isInterceptedLink);
        }

        [JSInvokable(nameof(OnRenderCompleted))]
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
                "framework://blazor.desktop.js" => Microsoft.MobileBlazorBindings.Hosting.BlazorAssets.GetBlazorDesktopJS(),
                _ => throw new ArgumentException($"Unknown framework file: {uri}"),
            };
        }

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
            if (_attachInteropTask != null)
            {
                _attachInteropTask = null;
            }
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

        public void HandleNavigationStarting(Uri url)
        {
            OnNavigationStarting?.Invoke(this, url);
        }

        public void HandleNavigationFinished(Uri url)
        {
            OnNavigationFinished?.Invoke(this, url);
        }

    }
}
