// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.Graphics;
using Android.Runtime;
using Android.Webkit;
using System;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWebView = Android.Webkit.WebView;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    public class WebKitWebViewClient : WebViewClient
    {
        private WebNavigationResult _navigationResult = WebNavigationResult.Success;
        private WebKitWebViewRenderer _renderer;
        private string _lastUrlNavigatedCancel;

        public WebKitWebViewClient(WebKitWebViewRenderer renderer)
            => _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));

        protected WebKitWebViewClient(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private bool SendNavigatingCanceled(string url) => _renderer?.SendNavigatingCanceled(url) ?? true;

        public override bool ShouldOverrideUrlLoading(AWebView view, IWebResourceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (_renderer.Element.SchemeHandlers.TryGetValue(request.Url.Scheme, out var schemeHandler))
            {
                var contentStream = schemeHandler(request.Url.ToString(), out var contentType);

                using var reader = new StreamReader(contentStream);
                var content = reader.ReadToEnd();

                view.LoadDataWithBaseURL(
                    baseUrl: "app://0.0.0.0/",
                    data: content,
                    mimeType: contentType,
                    encoding: "UTF-8",
                    historyUrl: null);

                return true;
            }
            return SendNavigatingCanceled(request.Url?.ToString());
        }

        public override WebResourceResponse ShouldInterceptRequest(AWebView view, IWebResourceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (_renderer.Element.SchemeHandlers.TryGetValue(request.Url.Scheme, out var schemeHandler))
            {
                var contentStream = schemeHandler(request.Url.ToString(), out var contentType);
                if (contentStream != null)
                {
                    using var reader = new StreamReader(contentStream);
                    var content = reader.ReadToEnd();
                    contentStream = schemeHandler(request.Url.ToString(), out contentType);
                    return new WebResourceResponse(contentType, "UTF-8", contentStream);
                }
            }

            return base.ShouldInterceptRequest(view, request);
        }

        public override void OnPageStarted(AWebView view, string url, Bitmap favicon)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (_renderer?.Element == null || string.IsNullOrWhiteSpace(url) || url == WebViewRenderer.AssetBaseUrl)
            {
                return;
            }

            _renderer.SyncNativeCookiesToElement(url);
            var cancel = false;
            if (!url.Equals(_renderer.UrlCanceled, StringComparison.OrdinalIgnoreCase))
            {
                cancel = SendNavigatingCanceled(url);
            }

            _renderer.UrlCanceled = null;

            if (cancel)
            {
                _navigationResult = WebNavigationResult.Cancel;
                view.StopLoading();
            }
            else
            {
                _navigationResult = WebNavigationResult.Success;
                base.OnPageStarted(view, url, favicon);
            }
        }

        /// <summary>
        /// Script to enable C#/JS communication for Blazor, and then loads Blazor and starts it
        /// </summary>
        private const string InitScriptSource = @"
            function blazorInitScript() {
                window.__receiveMessageCallbacks = [];
                window.__dispatchMessageCallback = function(message) {
                    window.__receiveMessageCallbacks.forEach(function(callback) { callback(message); });
                };

                window.external.sendMessage = function(message) {
                    window.external.PostMessage(message);
                };
                window.external.receiveMessage = function(callback) {
                    window.__receiveMessageCallbacks.push(callback);
                };

                var blazorScript = document.createElement('script');
                blazorScript.src = 'framework://blazor.desktop.js';
                document.head.appendChild(blazorScript);
            }
        ";

        private bool _firstPageLoad = true;

        public override void OnPageFinished(AWebView view, string url)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"'{nameof(url)}' cannot be null or empty", nameof(url));
            }

            if (_renderer?.Element == null || url == WebViewRenderer.AssetBaseUrl)
            {
                return;
            }

            var source = new UrlWebViewSource { Url = url };
            _renderer.IgnoreSourceChanges = true;
            _renderer.ElementController.SetValueFromRenderer(Xamarin.Forms.WebView.SourceProperty, source);
            _renderer.IgnoreSourceChanges = false;

            var navigate = _navigationResult != WebNavigationResult.Failure || !url.Equals(_lastUrlNavigatedCancel, StringComparison.OrdinalIgnoreCase);
            _lastUrlNavigatedCancel = _navigationResult == WebNavigationResult.Cancel ? url : null;

            if (navigate)
            {
                var args = new WebNavigatedEventArgs(_renderer.GetCurrentWebNavigationEvent(), source, url, _navigationResult);
                _renderer.SyncNativeCookiesToElement(url);
                _renderer.ElementController.SendNavigated(args);
            }

            _renderer.UpdateCanGoBackForward();

            base.OnPageFinished(view, url);

            // TODO: Not sure if this check is right. This protects against re-starting Blazor
            // during a Blazor navigation. But it will prevent re-starting Blazor if there's
            // a "full" navigation. Does that matter?
            if (_firstPageLoad)
            {
                RunBlazorStartupScripts(view);
                _firstPageLoad = false;
            }
        }


        private void RunBlazorStartupScripts(AWebView view)
        {
            var scriptSourceBytes = Encoding.UTF8.GetBytes(InitScriptSource);
            var encodedInitScript = Convert.ToBase64String(scriptSourceBytes, Base64FormattingOptions.None);

            // Add the init script tag reference
            view.LoadUrl("javascript:(function() {" +
                         "var initScript = document.createElement('script');" +
                         "initScript.innerHTML = window.atob('" + encodedInitScript + "');" +
                         "document.head.appendChild(initScript);" +
                         "})()");

            // Run the init script (which will then load Blazor and start it)
            view.LoadUrl("javascript:setTimeout(blazorInitScript, 0)");
        }

        public override void OnReceivedError(AWebView view, IWebResourceRequest request, WebResourceError error)
        {
            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            _navigationResult = WebNavigationResult.Failure;
            if (error.ErrorCode == ClientError.Timeout)
            {
                _navigationResult = WebNavigationResult.Timeout;
            }

            base.OnReceivedError(view, request, error);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _renderer = null;
            }
        }
    }
}
