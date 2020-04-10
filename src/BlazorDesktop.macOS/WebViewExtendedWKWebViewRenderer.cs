using BlazorDesktop.Elements;
using BlazorDesktop.macOS;
using Foundation;
using WebKit;
using XF = Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using System.ComponentModel;

[assembly: XF.ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedWKWebViewRenderer))]

namespace BlazorDesktop.macOS
{
    public class WebViewExtendedWKWebViewRenderer : ViewRenderer<WebViewExtended, WKWebView>, XF.IWebViewDelegate
    {
        WKWebView _wkWebView;

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var config = new WKWebViewConfiguration();
                    _wkWebView = new WKWebView(Frame, config);
                    SetNativeControl(_wkWebView);
                }

                Load();
            }

            base.OnElementChanged(e);
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
    }
}
