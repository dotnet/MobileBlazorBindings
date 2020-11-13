// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Foundation;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;
using WebKit;

namespace Microsoft.MobileBlazorBindings.WebView.macOS
{
    internal class WebViewNavigationDelegate : WKNavigationDelegate
    {
        private readonly WebViewExtended _webView;

        private WKNavigation _currentNavigation;
        private Uri _currentUri;

        public WebViewNavigationDelegate(WebViewExtended webView)
        {
            _webView = webView;
        }

        public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            _currentNavigation = navigation;
        }

        public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
        {
            if (navigationAction.TargetFrame.MainFrame)
            {
                _currentUri = navigationAction.Request.Url;
            }
            decisionHandler(WKNavigationActionPolicy.Allow);
        }

        public override void DidReceiveServerRedirectForProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            // We need to intercept the redirects to the app scheme because Safari will block them.
            // We will handle these redirects through the Navigation Manager.
            if (_currentUri.Host == "0.0.0.0")
            {
                var uri = _currentUri;
                _currentUri = null;
                _currentNavigation = null;
#pragma warning disable CA2000 // Dispose objects before losing scope
                var request = new NSUrlRequest(uri);
#pragma warning restore CA2000 // Dispose objects before losing scope
                webView.LoadRequest(request);
            }
        }

        public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            _currentUri = null;
            _currentNavigation = null;
        }

        public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            _currentUri = null;
            _currentNavigation = null;
        }

        public override void DidCommitNavigation(WKWebView webView, WKNavigation navigation)
        {
            if (_currentUri != null && _currentNavigation == navigation)
            {
                _webView.HandleNavigationStarting(_currentUri);
            }
        }

        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            if (_currentUri != null && _currentNavigation == navigation)
            {
                _webView.HandleNavigationFinished(_currentUri);
                _currentUri = null;
                _currentNavigation = null;
            }
        }
    }
}