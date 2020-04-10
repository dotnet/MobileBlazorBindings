using BlazorDesktop.Elements;
using BlazorDesktop.macOS;
using Foundation;
using WebKit;
using XF = Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using System.ComponentModel;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.Encodings.Web;

[assembly: XF.ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedWKWebViewRenderer))]

namespace BlazorDesktop.macOS
{
    public class WebViewExtendedWKWebViewRenderer : ViewRenderer<WebViewExtended, WKWebView>, XF.IWebViewDelegate, IWKScriptMessageHandler
    {
        WKWebView _wkWebView;

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
			            };";
                    config.UserContentController.AddUserScript(new WKUserScript(
                        new NSString(initScriptSource), WKUserScriptInjectionTime.AtDocumentStart, isForMainFrameOnly: true));
                    config.UserContentController.AddScriptMessageHandler(this, "webwindowinterop");

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

        void Load()
        {
            if (Element.Source != null)
            {
                Element.Source.Load(this);
            }
        }

        public void LoadHtml(string html, string baseUrl)
        {
            if (html == null)
                return;

            Control.LoadHtmlString(html, new NSUrl(baseUrl ?? "about:blank"));
        }

        public void LoadUrl(string url)
        {
            if (url == null)
                return;

            Control.LoadRequest(new NSUrlRequest(new NSUrl(url)));
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.HandleWebMessageReceived(((NSString)message.Body).ToString());
        }
    }
}
