// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xamarin.Forms.Platform.WPF;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedAnaheimRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.Windows
{
    public class WebViewExtendedAnaheimRenderer : ViewRenderer<WebViewExtended, WebView2>, XF.IWebViewDelegate
    {
        private CoreWebView2Environment _coreWebView2Environment;

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            _ = HandleElementChangedAsync(e);
        }

        private const string LoadBlazorJSScript =
            "window.onload = (function blazorInitScript() {" +
            "    var blazorScript = document.createElement('script');" +
            "    blazorScript.src = 'framework://blazor.desktop.js';" +
            "    document.head.appendChild(blazorScript);" +
            "});";

        private async Task HandleElementChangedAsync(ElementChangedEventArgs<WebViewExtended> e)
        {
            if (e.OldElement != null)
            {
                throw new NotSupportedException("On WPF, we need to retain the association between WebView elements and renderers, so switching to a different element isn't supported.");
            }

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    throw new NotSupportedException("On WPF, we need to retain the association between WebView elements and renderers, so switching to a different element isn't supported.");
                }

                if (e.NewElement.RetainedNativeControl is WebView2 retainedNativeControl)
                {
                    SetNativeControl(retainedNativeControl);
                    SubscribeToControlEvents();
                }
                else
                {
                    var nativeControl = new WebView2() { MinHeight = 200 };
                    e.NewElement.RetainedNativeControl = nativeControl;
                    SetNativeControl(nativeControl);

                    _coreWebView2Environment = await CoreWebView2Environment.CreateAsync( null, BlazorHybridWindows.WebViewDirectory).ConfigureAwait(true);

                    await nativeControl.EnsureCoreWebView2Async(_coreWebView2Environment).ConfigureAwait(true);

                    await Control.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };").ConfigureAwait(true);
                    await Control.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(LoadBlazorJSScript).ConfigureAwait(true);

                    SubscribeToControlEvents();

                    Load();
                }

                SubscribeToElementEvents();

                // There is a weird bug in WebView2 where on 200% DPI it does not redraw the WebView2 until you
                // send a WM_WINDOWPOSCHANGING message to the child window that serves as a host for WebView2.
                // this sends the required message.
                Control.UpdateWindowPos();
            }

            base.OnElementChanged(e);

        }

        private void SubscribeToElementEvents()
        {
            Element.SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
        }

        private void SubscribeToControlEvents()
        {
            Control.WebMessageReceived += HandleWebMessageReceived;
            Control.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
            Control.CoreWebView2.WebResourceRequested += HandleWebResourceRequested;
        }

        private void OnSendMessageFromJSToDotNetRequested(object sender, string message)
        {
            Control.CoreWebView2.PostWebMessageAsString(message);
        }

        private void HandleWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            Element.HandleWebMessageReceived(args.TryGetWebMessageAsString());
        }

        private void HandleWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs args)
        {
            var uriString = args.Request.Uri;
            var uri = new Uri(uriString);
            if (Element.SchemeHandlers.TryGetValue(uri.Scheme, out var handler) && _coreWebView2Environment != null)
            {
                var responseStream = handler(uriString, out var responseContentType);
                if (responseStream != null) // If null, the handler doesn't want to handle it
                {
                    responseStream.Position = 0;
                    args.Response = Control.CoreWebView2.Environment.CreateWebResourceResponse(responseStream, StatusCode: 200, ReasonPhrase: "OK", Headers: $"Content-Type: {responseContentType}");
                }
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
            if (html != null)
            {
                Control.NavigateToString(html);
            }
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void LoadUrl(string url)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (url != null)
            {
                Control.CoreWebView2.Navigate(url);
            }
        }

        private bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (Control != null)
                {
                    Control.CoreWebView2.WebResourceRequested -= HandleWebResourceRequested;
                    Control.CoreWebView2.RemoveWebResourceRequestedFilter("*", Web.WebView2.Core.CoreWebView2WebResourceContext.All);
                    Control.WebMessageReceived -= HandleWebMessageReceived;

                    switch (Control.Parent)
                    {
                        case FormsPanel formsPanel:
                            formsPanel.Children.Remove(Control);
                            break;
                        case ContentControl contentControl:
                            contentControl.Content = null;
                            break;
                        default:
                            throw new NotImplementedException($"Don't know how to detach from a parent of type {Control.Parent.GetType().FullName}");
                    }
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
