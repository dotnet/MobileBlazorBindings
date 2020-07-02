// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Foundation;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.macOS;
using System;
using System.ComponentModel;
using System.IO;
using System.Text.Encodings.Web;
using WebKit;
using Xamarin.Forms.Platform.MacOS;
using XF = Xamarin.Forms;

[assembly: XF.ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedWKWebViewRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.macOS
{
    public class WebViewExtendedWKWebViewRenderer : ViewRenderer<WebViewExtended, WKWebView>, XF.IWebViewDelegate, IWKScriptMessageHandler
    {
        private WKWebView _wkWebView;

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
            if (e.OldElement != null)
            {
                e.OldElement.SendMessageFromJSToDotNetRequested -= OnSendMessageFromJSToDotNetRequested;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var config = new WKWebViewConfiguration();
                    config.Preferences.SetValueForKey(FromObject(true), new NSString("developerExtrasEnabled"));

                    var initScriptSource = @"
                        window.onload = (function blazorInitScript() {
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

                            var blazorScript = document.createElement('script');
                            blazorScript.src = 'framework://blazor.desktop.js';
                            document.head.appendChild(blazorScript);
                        });
                    ";
                    config.UserContentController.AddScriptMessageHandler(this, "webwindowinterop");
                    config.UserContentController.AddUserScript(new WKUserScript(
                        new NSString(initScriptSource), WKUserScriptInjectionTime.AtDocumentStart, true));

                    foreach (var (scheme, handler) in Element.SchemeHandlers)
                    {
                        config.SetUrlSchemeHandler(new SchemeHandler(handler), scheme);
                    }

                    _wkWebView = new WKWebView(Frame, config);
                    SetNativeControl(_wkWebView);

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

            if (e.PropertyName == XF.WebView.SourceProperty.PropertyName)
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

        public void LoadHtml(string html, string baseUrl)
        {
            if (html == null)
            {
                return;
            }

            Control.LoadHtmlString(html, new NSUrl(baseUrl ?? "about:blank"));
        }

        public void LoadUrl(string url)
        {
            if (url == null)
            {
                return;
            }

            Control.LoadRequest(new NSUrlRequest(new NSUrl(url)));
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
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
                var responseBytes = GetResponseBytes(urlSchemeTask.Request.Url.AbsoluteString, out var contentType, out var statusCode);
                var response = new NSUrlResponse(urlSchemeTask.Request.Url, contentType, responseBytes.Length, null);
                urlSchemeTask.DidReceiveResponse(response);
                urlSchemeTask.DidReceiveData(NSData.FromArray(responseBytes));
                urlSchemeTask.DidFinish();
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
                    var ms = new MemoryStream();
                    responseStream.CopyTo(ms);
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
