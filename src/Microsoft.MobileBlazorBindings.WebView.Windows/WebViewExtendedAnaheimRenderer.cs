// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using Xamarin.Forms.Platform.WPF;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedAnaheimRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.Windows
{
    public class WebViewExtendedAnaheimRenderer : ViewRenderer<WebViewExtended, WebView2>, XF.IWebViewDelegate
    {
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
                    await nativeControl.EnsureCoreWebView2Async().ConfigureAwait(true);

                    await Control.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };").ConfigureAwait(true);
                    await Control.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(LoadBlazorJSScript).ConfigureAwait(true);

                    SubscribeToControlEvents();

                    Load();
                }

                SubscribeToElementEvents();
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
            // TODO: we have to resort to reflection here because of two issues:
            //
            // 1) The Uri for the framework resource has a custom scheme and HttpRequestMessage does not respond
            //    non http schemes: 
            //    https://github.com/dotnet/runtime/blob/0c7e9c19cb22420248c53eec6bb885fb563c700d/src/libraries/System.Net.Http/src/System/Net/Http/HttpRequestMessage.cs#L89-L92
            //    https://github.com/dotnet/runtime/blob/0c7e9c19cb22420248c53eec6bb885fb563c700d/src/libraries/System.Net.Http/src/System/Net/Http/HttpRequestMessage.cs#L188-L191
            //    So we have to take the Uri string from the native ICoreWebView2WebResourceRequest and create an
            //    Uri from it ourselves.
            //    This issue is tracked here: https://github.com/MicrosoftEdge/WebViewFeedback/issues/325
            // 2) There is a null reference exception that occurs in WebView2 when trying to set the response on the event
            //    argument. 
            //    This issue is tracked here: https://github.com/MicrosoftEdge/WebViewFeedback/issues/219

            var eventType = args.GetType();
            var field = eventType.GetField("_nativeCoreWebView2WebResourceRequestedEventArgs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var nativeArgs = field.GetValue(args);
            
            var requestProperty = eventType.Assembly.GetType("Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequestedEventArgs").GetProperty("Request");
            var nativeRequest = requestProperty.GetValue(nativeArgs);
            var uriProperty = eventType.Assembly.GetType("Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequest").GetProperty("Uri");
            var uriString = (string)uriProperty.GetValue(nativeRequest);
            var uri = new Uri(uriString);
           
            if (Element.SchemeHandlers.TryGetValue(uri.Scheme, out var handler))
            {
                var responseStream = handler(uriString, out var responseContentType);
                if (responseStream != null) // If null, the handler doesn't want to handle it
                {
                    responseStream.Position = 0;

                    var environment = (CoreWebView2Environment)Control.GetType().GetProperty("Environment", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(Control);
                    field = environment.GetType().GetField("_nativeCoreWebView2Environment", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var nativeEnvironment = field.GetValue(environment);

                    var managedStream = Activator.CreateInstance(eventType.Assembly.GetType("Microsoft.Web.WebView2.Core.ManagedIStream"),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                        null, 
                        new object[] { responseStream }, 
                        null);

                    var createWebSourceResponseMethod = eventType.Assembly.GetType("Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Environment").GetMethod("CreateWebResourceResponse", new Type[] { Type.GetType("System.Runtime.InteropServices.ComTypes.IStream"), typeof(int), typeof(string), typeof(string) });
                    var response = createWebSourceResponseMethod.Invoke(nativeEnvironment, new object[] { managedStream, 200, "OK", $"Content-Type: {responseContentType}" });

                    var responseProperty = eventType.Assembly.GetType("Microsoft.Web.WebView2.Core.Raw.ICoreWebView2WebResourceRequestedEventArgs").GetProperty("Response");
                    responseProperty.SetValue(nativeArgs, response);
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
