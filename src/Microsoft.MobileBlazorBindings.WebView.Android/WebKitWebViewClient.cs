// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Webkit;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWebView = Android.Webkit.WebView;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    public class WebKitWebViewClient : WebViewClient, IValueCallback
    {
        /// <summary>
        /// Script to enable C#/JS communication for Blazor, and then loads Blazor and starts it
        /// </summary>
        private const string InitScriptSource = @"
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
        ";

        private WebNavigationResult _navigationResult = WebNavigationResult.Success;
        private WebKitWebViewRenderer _renderer;
        private readonly WebViewExtended _webView;
        private string _lastUrlNavigatedCancel;

        public WebKitWebViewClient(WebKitWebViewRenderer renderer, WebViewExtended webView)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _webView = webView;
        }

        protected WebKitWebViewClient(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            // This constructor is called whenever the .NET proxy was disposed, and it was recreated by Java. It also
            // happens when overriden methods are called between execution of this constructor and the one above.
            // because of these facts, we have to check
            // all methods below for null field references and properties.
        }

        public override bool ShouldOverrideUrlLoading(AWebView view, IWebResourceRequest request)
        {
            // handle redirects to the app custom scheme by reloading the url in the view.
            // otherwise they will be blocked by Android.
#pragma warning disable CA1062 // Validate arguments of public methods
            if (request.IsRedirect && request.IsForMainFrame)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                var uri = new Uri(request.Url.ToString());
                if (uri.Host == "0.0.0.0")
                {
#pragma warning disable CA1062 // Validate arguments of public methods
                    view.LoadUrl(uri.ToString());
#pragma warning restore CA1062 // Validate arguments of public methods
                    return true;
                }
            }
            return base.ShouldOverrideUrlLoading(view, request);
        }

        private bool SendNavigatingCanceled(string url) => _renderer?.SendNavigatingCanceled(url) ?? true;

        public override WebResourceResponse ShouldInterceptRequest(AWebView view, IWebResourceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (_renderer?.Element != null && _renderer.Element.SchemeHandlers.TryGetValue(request.Url.Scheme, out var schemeHandler))
            {
                var contentStream = schemeHandler(request.Url.ToString(), out var contentType);
                if (contentStream != null)
                {
                    if (request.Url.Scheme == "framework")
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

                    // there is a result stream, prepare the response.
                    var responseHeaders = new Dictionary<string, string>()
                    {
                        { "Cache-Control", "no-cache, max-age=0, must-revalidate, no-store" },
                    };

                    return new WebResourceResponse(contentType, "UTF-8", 200, "OK", responseHeaders, contentStream);
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

            if (_renderer?.Element == null || _webView == null || string.IsNullOrWhiteSpace(url) || url == WebViewRenderer.AssetBaseUrl)
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
                _webView.HandleNavigationStarting(new Uri(url));
            }
        }

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

            if (_webView == null && _renderer?.Element == null || url == WebViewRenderer.AssetBaseUrl)
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

            var uri = new Uri(url);
            _webView.HandleNavigationFinished(uri);
            if (uri.Scheme == "app")
            {
                RunBlazorStartupScripts(view);
            }
        }

        private void RunBlazorStartupScripts(AWebView view)
        {
            Log.Info("RunBlazorStartupScripts", "attaching Blazor init script.");
            // we need to protect against reinsertion of the script tag because the
            // OnPageFinished event refires after the app is brought back from the 
            // foreground and the webview is brought back into view, without it actually
            // getting reloaded.
            view.EvaluateJavascript(@"
                if (document.getElementById('blazorDesktopFrameworkScriptTag') == null)
                {
                    var blazorScript = document.createElement('script');
                    blazorScript.id = 'blazorDesktopFrameworkScriptTag';
                    blazorScript.src = 'framework://blazor.desktop.js';
                    document.head.appendChild(blazorScript);
                }
            ", this);
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

        public void OnReceiveValue(Java.Lang.Object value)
        {
        }
    }
}
