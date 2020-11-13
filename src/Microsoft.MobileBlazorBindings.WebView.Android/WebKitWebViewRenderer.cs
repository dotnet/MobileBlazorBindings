// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.Content;
using Android.Runtime;
using Android.Webkit;
using Java.Interop;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using AWebView = Android.Webkit.WebView;
using MixedContentHandling = Android.Webkit.MixedContentHandling;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    public class WebKitWebViewRenderer : ViewRenderer<WebViewExtended, AWebView>, IWebViewDelegate
    {
        public const string AssetBaseUrl = "file:///android_asset/";
        private WebNavigationEvent _eventState;
        private WebKitWebViewClient _webViewClient;
        private WebKitWebChromeClient _webChromeClient;
        private bool _isDisposed;
        protected internal IWebViewController ElementController => Element;
        protected internal bool IgnoreSourceChanges { get; set; }
#pragma warning disable CA1056 // Uri properties should not be strings
        protected internal string UrlCanceled { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
        private PostMessageJavaScriptInterface _postMessageJSInterface;

        public WebKitWebViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadHtml(string html, string baseUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _eventState = WebNavigationEvent.NewPage;
            Control.LoadDataWithBaseURL(
                baseUrl: baseUrl ?? AssetBaseUrl,
                data: html,
                mimeType: "text/html",
                encoding: "UTF-8",
                historyUrl: null);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadUrl(string url)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            LoadUrl(url, fireNavigatingCanceled: true);
        }

        private void LoadUrl(string url, bool fireNavigatingCanceled)
        {
            if (!fireNavigatingCanceled || !SendNavigatingCanceled(url))
            {
                _eventState = WebNavigationEvent.NewPage;
                Control.LoadUrl(url);
            }
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        protected internal bool SendNavigatingCanceled(string url)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (Element == null || string.IsNullOrWhiteSpace(url))
            {
                return true;
            }

            if (url == AssetBaseUrl)
            {
                return false;
            }

            var args = new WebNavigatingEventArgs(_eventState, new UrlWebViewSource { Url = url }, url);
            SyncNativeCookies(url);
            ElementController.SendNavigating(args);
            UpdateCanGoBackForward();
            UrlCanceled = args.Cancel ? null : url;
            return args.Cancel;
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            if (disposing)
            {
                if (Element != null)
                {
                    Control?.StopLoading();

                    ElementController.EvalRequested -= OnEvalRequested;
                    ElementController.GoBackRequested -= OnGoBackRequested;
                    ElementController.GoForwardRequested -= OnGoForwardRequested;
                    ElementController.ReloadRequested -= OnReloadRequested;
                    ElementController.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;

                    _webViewClient?.Dispose();
                    _webChromeClient?.Dispose();
                }

                _postMessageJSInterface?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected virtual WebKitWebViewClient GetWebViewClient(WebViewExtended webView)
        {
            return new WebKitWebViewClient(this, webView);
        }

        protected virtual WebKitWebChromeClient GetFormsWebChromeClient()
        {
            return new WebKitWebChromeClient();
        }

        protected override Size MinimumSize()
        {
            return new Size(Context.ToPixels(40), Context.ToPixels(40));
        }

        protected override AWebView CreateNativeControl()
        {
            return new AWebView(Context);
        }

        internal WebNavigationEvent GetCurrentWebNavigationEvent()
        {
            return _eventState;
        }

        public void PostMessageFromJS(string message)
        {
            Element.HandleWebMessageReceived(message);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (e.OldElement != null)
            {
                e.OldElement.SendMessageFromJSToDotNetRequested -= OnSendMessageFromJSToDotNetRequested;
            }

            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = CreateNativeControl();
#pragma warning disable 618 // This can probably be replaced with LinearLayout(LayoutParams.MatchParent, LayoutParams.MatchParent); just need to test that theory
                webView.LayoutParameters = new global::Android.Widget.AbsoluteLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent, 0, 0);
#pragma warning restore 618

                _webViewClient = GetWebViewClient(Element);
                webView.SetWebViewClient(_webViewClient);

                _webChromeClient = GetFormsWebChromeClient();
                _webChromeClient.SetContext(Context);
                webView.SetWebChromeClient(_webChromeClient);


                // TODO: This API is internal so it can't be called. Is it needed? (There's no Designer here anyway.)
                //if (Context.IsDesignerContext())
                //{
                //	SetNativeControl(webView);
                //	return;
                //}

                webView.Settings.JavaScriptEnabled = true;
                webView.Settings.DomStorageEnabled = true;

                // TODO: Eventually remove this
                AWebView.SetWebContentsDebuggingEnabled(true);

                _postMessageJSInterface = new PostMessageJavaScriptInterface(PostMessageFromJS);
                webView.AddJavascriptInterface(_postMessageJSInterface, name: "external");

                SetNativeControl(webView);

                Element.SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
            }

            if (e.OldElement != null)
            {
                var oldElementController = e.OldElement as IWebViewController;
                oldElementController.EvalRequested -= OnEvalRequested;
                oldElementController.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
                oldElementController.GoBackRequested -= OnGoBackRequested;
                oldElementController.GoForwardRequested -= OnGoForwardRequested;
                oldElementController.ReloadRequested -= OnReloadRequested;
            }

            if (e.NewElement != null)
            {
                var newElementController = e.NewElement as IWebViewController;
                newElementController.EvalRequested += OnEvalRequested;
                newElementController.EvaluateJavaScriptRequested += OnEvaluateJavaScriptRequested;
                newElementController.GoBackRequested += OnGoBackRequested;
                newElementController.GoForwardRequested += OnGoForwardRequested;
                newElementController.ReloadRequested += OnReloadRequested;

                UpdateMixedContentMode();
                UpdateEnableZoomControls();
                UpdateDisplayZoomControls();
            }

            Load();
        }

        private void OnSendMessageFromJSToDotNetRequested(object sender, string message)
        {
            var messageJSStringLiteral = JavaScriptEncoder.Default.Encode(message);
            try
            {
                Control.EvaluateJavascript($"__dispatchMessageCallback(\"{messageJSStringLiteral}\")", null);
            }
            catch (ObjectDisposedException)
            {
                // the control was disposed, no evaluation possible anymore.
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Source":
                    Load();
                    break;
                case "MixedContentMode":
                    UpdateMixedContentMode();
                    break;
                case "EnableZoomControls":
                    UpdateEnableZoomControls();
                    break;
                case "DisplayZoomControls":
                    UpdateDisplayZoomControls();
                    break;
            }
        }

        private readonly HashSet<string> _loadedCookies = new HashSet<string>();

        private static Uri CreateUriForCookies(string url)
        {
            if (url == null)
            {
                return null;
            }


            if (url.Length > 2000)
            {
                url = url.Substring(0, 2000);
            }

            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                if (string.IsNullOrWhiteSpace(uri.Host))
                {
                    return null;
                }

                return uri;
            }

            return null;
        }

        private static CookieCollection GetCookiesFromNativeStore(string url)
        {
            var existingCookies = new CookieContainer();
            var cookieManager = CookieManager.Instance;
            var currentCookies = cookieManager.GetCookie(url);
            var uri = CreateUriForCookies(url);

            if (currentCookies != null)
            {
                foreach (var cookie in currentCookies.Split(';'))
                {
                    existingCookies.SetCookies(uri, cookie);
                }
            }

            return existingCookies.GetCookies(uri);
        }

        private void InitialCookiePreloadIfNecessary(string url)
        {
            var myCookieJar = Element.Cookies;
            if (myCookieJar == null)
            {
                return;
            }

            var uri = CreateUriForCookies(url);
            if (uri == null)
            {
                return;
            }

            if (!_loadedCookies.Add(uri.Host))
            {
                return;
            }

            var cookies = myCookieJar.GetCookies(uri);

            if (cookies != null)
            {
                var existingCookies = GetCookiesFromNativeStore(url);
                foreach (Cookie cookie in existingCookies)
                {
                    if (cookies[cookie.Name] == null)
                    {
                        myCookieJar.Add(cookie);
                    }
                }
            }
        }

        internal void SyncNativeCookiesToElement(string url)
        {
            var myCookieJar = Element.Cookies;
            if (myCookieJar == null)
            {
                return;
            }

            var uri = CreateUriForCookies(url);
            if (uri == null)
            {
                return;
            }

            var cookies = myCookieJar.GetCookies(uri);
            var retrieveCurrentWebCookies = GetCookiesFromNativeStore(url);

            foreach (Cookie cookie in cookies)
            {
                var nativeCookie = retrieveCurrentWebCookies[cookie.Name];
                if (nativeCookie == null)
                {
                    cookie.Expired = true;
                }
                else
                {
                    cookie.Value = nativeCookie.Value;
                }
            }

            SyncNativeCookies(url);
        }

        private void SyncNativeCookies(string url)
        {
            var uri = CreateUriForCookies(url);
            if (uri == null)
            {
                return;
            }

            var myCookieJar = Element.Cookies;
            if (myCookieJar == null)
            {
                return;
            }

            InitialCookiePreloadIfNecessary(url);
            var cookies = myCookieJar.GetCookies(uri);
            if (cookies == null)
            {
                return;
            }

            var retrieveCurrentWebCookies = GetCookiesFromNativeStore(url);

            var cookieManager = CookieManager.Instance;
            cookieManager.SetAcceptCookie(true);
            for (var i = 0; i < cookies.Count; i++)
            {
                var cookie = cookies[i];
                var cookieString = cookie.ToString();
                cookieManager.SetCookie(cookie.Domain, cookieString);
            }

            foreach (Cookie cookie in retrieveCurrentWebCookies)
            {
                if (cookies[cookie.Name] != null)
                {
                    continue;
                }

                var cookieString = $"{cookie.Name}=; max-age=0;expires=Sun, 31 Dec 2017 00:00:00 UTC";
                cookieManager.SetCookie(cookie.Domain, cookieString);
            }
        }

        private void Load()
        {
            if (IgnoreSourceChanges)
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
            LoadUrl("javascript:" + eventArg.Script, false);
        }

        private async Task<string> OnEvaluateJavaScriptRequested(string script)
        {
            using var jsr = new JavascriptResult();

            Control.EvaluateJavascript(script, jsr);

            return await jsr.JsResult.ConfigureAwait(false);
        }

        private void OnGoBackRequested(object sender, EventArgs eventArgs)
        {
            if (Control.CanGoBack())
            {
                _eventState = WebNavigationEvent.Back;
                Control.GoBack();
            }

            UpdateCanGoBackForward();
        }

        private void OnGoForwardRequested(object sender, EventArgs eventArgs)
        {
            if (Control.CanGoForward())
            {
                _eventState = WebNavigationEvent.Forward;
                Control.GoForward();
            }

            UpdateCanGoBackForward();
        }

        private void OnReloadRequested(object sender, EventArgs eventArgs)
        {
            SyncNativeCookies(Control.Url);
            _eventState = WebNavigationEvent.Refresh;
            Control.Reload();
        }

        protected internal void UpdateCanGoBackForward()
        {
            if (Element == null || Control == null)
            {
                return;
            }

            ElementController.CanGoBack = Control.CanGoBack();
            ElementController.CanGoForward = Control.CanGoForward();
        }

        private void UpdateMixedContentMode()
        {
            if (Control != null && ((int)FormsHelper.SdkInt >= 21))
            {
                Control.Settings.MixedContentMode = (MixedContentHandling)((Xamarin.Forms.WebView)Element).OnThisPlatform().MixedContentMode();
            }
        }

        private void UpdateEnableZoomControls()
        {
            var value = ((Xamarin.Forms.WebView)Element).OnThisPlatform().ZoomControlsEnabled();
            Control.Settings.SetSupportZoom(value);
            Control.Settings.BuiltInZoomControls = value;
        }

        private void UpdateDisplayZoomControls()
        {
            Control.Settings.DisplayZoomControls = ((Xamarin.Forms.WebView)Element).OnThisPlatform().ZoomControlsDisplayed();
        }

        private class JavascriptResult : Java.Lang.Object, IValueCallback
        {
            private readonly TaskCompletionSource<string> _source;

            public JavascriptResult()
            {
                _source = new TaskCompletionSource<string>();
            }

            public JavascriptResult(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
            {
                // This constructor is called whenever the .NET proxy was disposed, and it was recreated by Java. It also
                // happens when overriden methods are called between execution of this constructor and the one above.
                // because of these facts, we have to check all methods below for null field references and properties.            
            }

            public Task<string> JsResult => _source?.Task ?? Task.FromResult(string.Empty);

            public void OnReceiveValue(Java.Lang.Object result)
            {
                if (_source == null)
                {
                    return;
                }

                var json = ((Java.Lang.String)result).ToString();
                _source.SetResult(json);
            }
        }

        private class PostMessageJavaScriptInterface : Java.Lang.Object
        {
            public PostMessageJavaScriptInterface(Action<string> postMessageCallback)
            {
                PostMessageCallback = postMessageCallback ?? throw new ArgumentNullException(nameof(postMessageCallback));
            }

            public PostMessageJavaScriptInterface(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
            {
                // This constructor is called whenever the .NET proxy was disposed, and it was recreated by Java. It also
                // happens when overriden methods are called between execution of this constructor and the one above.
                // because of these facts, we have to check all methods below for null field references and properties.            
            }

            public Action<string> PostMessageCallback { get; }

            [Export(nameof(PostMessage))]
            [JavascriptInterface]
            public void PostMessage(Java.Lang.String message)
            {
                PostMessageCallback?.Invoke(message.ToString());
            }
        }
    }
}
