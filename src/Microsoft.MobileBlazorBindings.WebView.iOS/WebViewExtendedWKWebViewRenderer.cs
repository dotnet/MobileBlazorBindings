// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Foundation;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.iOS;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using WebKit;
using Xamarin.Forms.Platform.iOS;
using XF = Xamarin.Forms;

[assembly: XF.ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedWKWebViewRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.iOS
{
#pragma warning disable CA1010 // Collections should implement generic interface
    public class WebViewExtendedWKWebViewRenderer : ViewRenderer<WebViewExtended, WKWebView>, XF.IWebViewDelegate, IWKScriptMessageHandler
#pragma warning restore CA1010 // Collections should implement generic interface
    {

        private const string InitScriptSource = @"
window.__receiveMessageCallbacks = [];
window.__dispatchMessageCallback = function(message) {
	window.__receiveMessageCallbacks.forEach(function(callback) { callback(message); });
};
window.external = {
	sendMessage: function(message) {
		window.webkit.messageHandlers.webwindowinterop.postMessage(message);
	},
	receiveMessage: function(callback) {
		window.__receiveMessageCallbacks.push(callback);
	}
};
";
        private WKWebView _wkWebView;

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            if (e.OldElement != null)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                e.OldElement.SendMessageFromJSToDotNetRequested -= OnSendMessageFromJSToDotNetRequested;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
#pragma warning disable CA2000 // Dispose objects before losing scope
                    var config = new WKWebViewConfiguration();
#pragma warning restore CA2000 // Dispose objects before losing scope
#pragma warning disable CA2000 // Dispose objects before losing scope
                    config.Preferences.SetValueForKey(FromObject(true), new NSString("developerExtrasEnabled"));
#pragma warning restore CA2000 // Dispose objects before losing scope

                    var frameworkScriptSource = @"
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
    }
                    ";
                    config.UserContentController.AddScriptMessageHandler(this, "webwindowinterop");
#pragma warning disable CA2000 // Dispose objects before losing scope
                    config.UserContentController.AddUserScript(new WKUserScript(
                        new NSString(frameworkScriptSource), WKUserScriptInjectionTime.AtDocumentEnd, true));
#pragma warning restore CA2000 // Dispose objects before losing scope

                    foreach (var (scheme, handler) in Element.SchemeHandlers)
                    {
#pragma warning disable CA2000 // Dispose objects before losing scope
                        config.SetUrlSchemeHandler(new SchemeHandler(handler), scheme);
#pragma warning restore CA2000 // Dispose objects before losing scope
                    }

                    _wkWebView = new WKWebView(Frame, config);
                    SetNativeControl(_wkWebView);

                    _wkWebView.NavigationDelegate = new WebViewNavigationDelegate(Element);
                    Element.SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
                }

                Load();
            }

            base.OnElementChanged(e);
        }

        private void OnSendMessageFromJSToDotNetRequested(object sender, string message)
        {
            var messageJSStringLiteral = JavaScriptEncoder.Default.Encode(message);
            Control.EvaluateJavaScript($"__dispatchMessageCallback(\"{messageJSStringLiteral}\")", null);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

#pragma warning disable CA1062 // Validate arguments of public methods
            if (e.PropertyName == XF.WebView.SourceProperty.PropertyName)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                Load();
            }
        }

        private void Load()
        {
            if (Element.Source != null)
            {
                Element.Source.Load(this);
            }
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadHtml(string html, string baseUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (html == null)
            {
                return;
            }

            using var baseNSUrl = new NSUrl(baseUrl ?? "about:blank");
            Control.LoadHtmlString(html, baseNSUrl);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadUrl(string url)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (url == null)
            {
                return;
            }

            using var nsUrl = new NSUrl(url);
            using var request = new NSUrlRequest(nsUrl);
            Control.LoadRequest(request);
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Element.HandleWebMessageReceived(((NSString)message.Body).ToString());
        }

        private class SchemeHandler : NSObject, IWKUrlSchemeHandler
        {
            private readonly ResolveWebResourceDelegate handler;

            public SchemeHandler(ResolveWebResourceDelegate handler)
            {
                this.handler = handler;
            }

            [Export("webView:startURLSchemeTask:")]
            public void StartUrlSchemeTask(WKWebView webView, IWKUrlSchemeTask urlSchemeTask)
            {
                var responseBytes = GetResponseBytes(urlSchemeTask.Request.Url.AbsoluteString, out var contentType, statusCode: out var statusCode);
                if (statusCode == 200)
                {
                    using (var dic = new NSMutableDictionary<NSString, NSString>())
                    {
                        dic.Add((NSString)"Content-Length", (NSString)(responseBytes.Length.ToString(CultureInfo.InvariantCulture)));
                        dic.Add((NSString)"Content-Type", (NSString)contentType);
                        // Disable local caching. This will prevent user scripts from executing correctly.
                        dic.Add((NSString)"Cache-Control", (NSString)"no-cache, max-age=0, must-revalidate, no-store");
                        using var response = new NSHttpUrlResponse(urlSchemeTask.Request.Url, statusCode, "HTTP/1.1", dic);
                        urlSchemeTask.DidReceiveResponse(response);
                    }
                    urlSchemeTask.DidReceiveData(NSData.FromArray(responseBytes));
                    urlSchemeTask.DidFinish();
                }
            }

            private byte[] GetResponseBytes(string url, out string contentType, out int statusCode)
            {
                var responseStream = handler(url, out contentType);
                if (responseStream == null)
                {
                    statusCode = 404;
                    return Array.Empty<byte>();
                }
                else
                {
                    statusCode = 200;
                    using var ms = new MemoryStream();
                    var uri = new Uri(url);

                    if (uri.Scheme == "framework")
                    {
                        var buffer = Encoding.UTF8.GetBytes(InitScriptSource);
                        ms.Write(buffer, 0, buffer.Length);
                        responseStream.CopyTo(ms);
                    }

                    responseStream.CopyTo(ms);
                    responseStream.Dispose();
                    return ms.ToArray();
                }
            }

            [Export("webView:stopURLSchemeTask:")]
            public void StopUrlSchemeTask(WKWebView webView, IWKUrlSchemeTask urlSchemeTask)
            {
            }
        }
    }
}
