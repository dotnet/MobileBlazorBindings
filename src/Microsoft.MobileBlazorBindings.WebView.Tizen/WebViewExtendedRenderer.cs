// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.Tizen;
using Tizen.WebView;
using TWebView = Tizen.WebView.WebView;
using TChromium = Tizen.WebView.Chromium;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.Tizen
{
    public class WebViewExtendedRenderer : ViewRenderer<WebViewExtended, WebViewContainer>, IWebViewDelegate
    {
        private bool _isUpdating;
        private WebNavigationEvent _eventState;
        private TWebView NativeWebView => Control.WebView;
        private WebViewExtension.InterceptRequestCallback _interceptRequestCallback;

#if TIZEN60
        private const string LoadBlazorJSScript =@"
            if (window.location.href.startsWith('app://'))
            {
                var blazorScript = document.createElement('script');
                blazorScript.src = 'framework://blazor.desktop.js';
                document.body.appendChild(blazorScript);
                (function () {
	                window.onpageshow = function(event) {
		                if (event.persisted) {
			                window.location.reload();
		                }
	                };
                })();
            }";
#else
        private const string LoadBlazorJSScript = @"
            if (window.location.href.startsWith('http://0.0.0.0/'))
            {
                var blazorScript = document.createElement('script');
                blazorScript.src = 'http://framework/blazor.desktop.js';
                document.body.appendChild(blazorScript);
                (function () {
	                window.onpageshow = function(event) {
		                if (event.persisted) {
			                window.location.reload();
		                }
	                };
                })();
            }";
#endif

        private const string InitScriptSource = @"
            window.__receiveMessageCallbacks = [];
            window.__dispatchMessageCallback = function(message) {
	            window.__receiveMessageCallbacks.forEach(function(callback) { callback(message); });
            };
            window.external = {
	            sendMessage: function(message) {
		            window.BlazorHandler.postMessage(message);
	            },
	            receiveMessage: function(callback) {
		            window.__receiveMessageCallbacks.push(callback);
	            }
            };";

        protected internal IWebViewController ElementController => Element;
        protected internal bool IgnoreSourceChanges { get; set; }
#pragma warning disable CA1056 // Uri properties should not be strings
        protected internal string UrlCanceled { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        public WebViewExtendedRenderer()
        {
            RegisterPropertyHandler(WebViewExtended.SourceProperty, Load);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadHtml(string html, string baseUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            NativeWebView.LoadHtml(html, baseUrl);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadUrl(string url)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (!string.IsNullOrEmpty(url))
            {
                NativeWebView.LoadUrl(url);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    NativeWebView.StopLoading();
                    NativeWebView.LoadStarted -= OnLoadStarted;
                    NativeWebView.LoadFinished -= OnLoadFinished;
                    NativeWebView.LoadError -= OnLoadError;
                }

                if (Element != null)
                {
                    Element.EvalRequested -= OnEvalRequested;
                    Element.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
                    Element.GoBackRequested -= OnGoBackRequested;
                    Element.GoForwardRequested -= OnGoForwardRequested;
                    Element.ReloadRequested -= OnReloadRequested;
                    Element.SendMessageFromJSToDotNetRequested -= OnSendMessageFromJSToDotNetRequested;
                }
            }
            base.Dispose(disposing);
        }

        public void PostMessageFromJS(JavaScriptMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Name.Equals("BlazorHandler", StringComparison.Ordinal))
            {
                Element.HandleWebMessageReceived(message.GetBodyAsString());
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (Control == null)
            {
                TChromium.Initialize();
                Forms.Context.Terminated += (sender, arg) => TChromium.Shutdown();
                SetNativeControl(new WebViewContainer(Forms.NativeParent));
                _interceptRequestCallback = OnRequestInterceptCallback;
                NativeWebView.LoadStarted += OnLoadStarted;
                NativeWebView.LoadFinished += OnLoadFinished;
                NativeWebView.LoadError += OnLoadError;
                NativeWebView.AddJavaScriptMessageHandler("BlazorHandler", PostMessageFromJS);
                NativeWebView.SetInterceptRequestCallback(_interceptRequestCallback);
                NativeWebView.GetSettings().JavaScriptEnabled = true;
            }

            if (e.OldElement != null)
            {
                e.OldElement.SendMessageFromJSToDotNetRequested -= OnSendMessageFromJSToDotNetRequested;
                e.OldElement.EvalRequested -= OnEvalRequested;
                e.OldElement.GoBackRequested -= OnGoBackRequested;
                e.OldElement.GoForwardRequested -= OnGoForwardRequested;
                e.OldElement.ReloadRequested -= OnReloadRequested;
            }

            if (e.NewElement != null)
            {
                e.NewElement.EvalRequested += OnEvalRequested;
                e.NewElement.EvaluateJavaScriptRequested += OnEvaluateJavaScriptRequested;
                e.NewElement.GoForwardRequested += OnGoForwardRequested;
                e.NewElement.GoBackRequested += OnGoBackRequested;
                e.NewElement.ReloadRequested += OnReloadRequested;
                e.NewElement.SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
                Load();
            }
            base.OnElementChanged(e);
        }

        private void OnRequestInterceptCallback(IntPtr context, IntPtr request, IntPtr userdata)
        {
            if (request == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(request));
            }

#if TIZEN60
            var url = NativeWebView.GetInterceptRequestUrl(request);
#else
            var url = NativeWebView.GetInterceptRequestUrl(request).Replace("http://framework/", "framework://");
#endif
            var urlScheme = url.Substring(0, url.IndexOf(':'));

            if (Element != null && Element.SchemeHandlers.TryGetValue(urlScheme, out var schemeHandler))
            {
                var uri = new Uri(url);
                Console.WriteLine($"uri : Host : {uri.Host} - {uri.AbsolutePath.Substring(1)} - test {uri.Host.Equals("0.0.0.0", StringComparison.Ordinal)}");

                var contentStream = schemeHandler(url, out var contentType);
                if (contentStream != null)
                {
                    var header = $"HTTP/1.0 200 OK\r\nContent-Type:{contentType}; charset=utf-8\r\nCache-Control:no-cache, max-age=0, must-revalidate, no-store\r\n\r\n";

                    if (urlScheme == "framework")
                    {
#pragma warning disable CA2000 // Dispose objects before losing scope
                        var memoryStream = new MemoryStream();
#pragma warning restore CA2000 // Dispose objects before losing scope
                        var buffer = Encoding.UTF8.GetBytes(InitScriptSource);
                        memoryStream.Write(buffer, 0, buffer.Length);
                        contentStream.CopyTo(memoryStream);
                        contentStream.Dispose();
                        memoryStream.Position = 0;
                        contentStream = memoryStream;
                    }
#pragma warning disable CA2000 // Dispose objects before losing scope
                    var body = new StreamReader(contentStream).ReadToEnd();
#pragma warning restore CA2000 // Dispose objects before losing scope
                    NativeWebView.SetInterceptRequestResponse(request, header, body, (uint)body.Length);
                    return;
                }
            }
            NativeWebView.IgnoreInterceptRequest(request);
        }

        private void OnSendMessageFromJSToDotNetRequested(object sender, string message)
        {
            var messageJSStringLiteral = JavaScriptEncoder.Default.Encode(message);
            NativeWebView.Eval($"__dispatchMessageCallback(\"{messageJSStringLiteral}\")");
        }

        private void OnLoadError(object sender, global::Tizen.WebView.SmartCallbackLoadErrorArgs e)
        {
            var url = e.Url;
            if (!string.IsNullOrEmpty(url))
            {
                SendNavigated(new UrlWebViewSource { Url = url }, _eventState, WebNavigationResult.Failure);
            }
        }

        private void OnLoadStarted(object sender, EventArgs e)
        {
            var url = NativeWebView.Url;

            if (!string.IsNullOrEmpty(url))
            {
                var args = new WebNavigatingEventArgs(_eventState, new UrlWebViewSource { Url = url }, url);
                Element.SendNavigating(args);
                Element.HandleNavigationStarting(new Uri(url));

                if (args.Cancel)
                {
                    _eventState = WebNavigationEvent.NewPage;
                }
            }
        }

        private void OnLoadFinished(object sender, EventArgs e)
        {
            var url = NativeWebView.Url;
            if (!string.IsNullOrEmpty(url))
            {
                SendNavigated(new UrlWebViewSource { Url = url }, _eventState, WebNavigationResult.Success);
            }

            NativeWebView.SetFocus(true);
            UpdateCanGoBackForward();

            NativeWebView.Eval(LoadBlazorJSScript);
            Element.HandleNavigationFinished(new Uri(url));
        }

        private void Load()
        {
            if (_isUpdating)
            {
                return;
            }

            if (Element.Source != null)
            {
                Element.Source.Load(this);
            }

            UpdateCanGoBackForward();
        }

        private void OnEvalRequested(object sender, EvalRequested eventArg)
        {
            NativeWebView.Eval(eventArg.Script);
        }

        private Task<string> OnEvaluateJavaScriptRequested(string script)
        {
            NativeWebView.Eval(script);
            return null;
        }

        private void OnGoBackRequested(object sender, EventArgs eventArgs)
        {
            if (NativeWebView.CanGoBack())
            {
                _eventState = WebNavigationEvent.Back;
                NativeWebView.GoBack();
            }

            UpdateCanGoBackForward();
        }

        private void OnGoForwardRequested(object sender, EventArgs eventArgs)
        {
            if (NativeWebView.CanGoForward())
            {
                _eventState = WebNavigationEvent.Forward;
                NativeWebView.GoForward();
            }

            UpdateCanGoBackForward();
        }

        private void OnReloadRequested(object sender, EventArgs eventArgs)
        {
            NativeWebView.Reload();
        }

        private void SendNavigated(UrlWebViewSource source, WebNavigationEvent evnt, WebNavigationResult result)
        {
            _isUpdating = true;
            ((IElementController)Element).SetValueFromRenderer(WebViewExtended.SourceProperty, source);
            _isUpdating = false;

            Element.SendNavigated(new WebNavigatedEventArgs(evnt, source, source.Url, result));

            UpdateCanGoBackForward();
            _eventState = WebNavigationEvent.NewPage;
        }

        private void UpdateCanGoBackForward()
        {
            ElementController.CanGoBack = NativeWebView.CanGoBack();
            ElementController.CanGoForward = NativeWebView.CanGoForward();
        }
    }
}
