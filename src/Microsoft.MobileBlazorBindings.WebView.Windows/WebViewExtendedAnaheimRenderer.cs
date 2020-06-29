using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.WebView.Windows;
using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wrapper;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xamarin.Forms.Platform.WPF;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedAnaheimRenderer))]

namespace Microsoft.MobileBlazorBindings.WebView.Windows
{
    public class WebViewExtendedAnaheimRenderer : ViewRenderer<WebViewExtended, WebView2Control>, XF.IWebViewDelegate
    {
        private static class NativeMethods
        {
            [DllImport("Shlwapi.dll", SetLastError = false, ExactSpelling = true)]
            public static extern IStream SHCreateMemStream(IntPtr pInit, uint cbInit);
        }

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
            "})";

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

                if (e.NewElement.RetainedNativeControl is WebView2Control retainedNativeControl)
                {
                    SetNativeControl(retainedNativeControl);
                    SubscribeToControlEvents();
                }
                else
                {
                    var nativeControl = new WebView2Control { MinHeight = 200 };
                    nativeControl.EnvironmentCreated += OnEnvironmentCreated;
                    e.NewElement.RetainedNativeControl = nativeControl;
                    SetNativeControl(nativeControl);
                    await WaitForBrowserCreatedAsync();

                    Control.AddScriptToExecuteOnDocumentCreated("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };", callbackArgs => { });
                    Control.AddScriptToExecuteOnDocumentCreated(LoadBlazorJSScript, callbackArgs => { });

                    SubscribeToControlEvents();

                    Load();
                }

                SubscribeToElementEvents();
            }

            base.OnElementChanged(e);
        }

        private void OnEnvironmentCreated(object sender, EnvironmentCreatedEventArgs e)
        {
            if (e.Result != 0)
            {
                if (e.Result == 2)
                {
                    throw new InvalidOperationException("Couldn't find Edge installation. Do you have a version installed that's compatible with this WebView2 SDK version?");
                }
                else
                {
                    throw new InvalidOperationException("Failed to create webview environment. Error: " + e.Result);
                }
            }
        }

        private void SubscribeToElementEvents()
        {
            Element.SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
        }

        private void SubscribeToControlEvents()
        {
            Control.WebMessageRecieved += HandleWebMessageReceived;
            Control.AddWebResourceRequestedFilter("*", WEBVIEW2_WEB_RESOURCE_CONTEXT.WEBVIEW2_WEB_RESOURCE_CONTEXT_ALL);
            Control.WebResourceRequested += HandleWebResourceRequested;
        }

        private void OnSendMessageFromJSToDotNetRequested(object sender, string message)
        {
            Control.PostWebMessageAsString(message);
        }

        private void HandleWebMessageReceived(object sender, WebMessageReceivedEventArgs args)
        {
            Element.HandleWebMessageReceived(args.WebMessageAsString);
        }

        private Task WaitForBrowserCreatedAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            Control.BrowserCreated += (sender, args) =>
            {
                tcs.TrySetResult(true);
            };
            return tcs.Task;
        }

        private void HandleWebResourceRequested(object sender, WebResourceRequestedEventArgs e)
        {
            var uri = new Uri(e.Request.Uri);
            if (Element.SchemeHandlers.TryGetValue(uri.Scheme, out var handler))
            {
                var responseStream = handler(e.Request.Uri, out var responseContentType);
                if (responseStream != null) // If null, the handler doesn't want to handle it
                {

                    using var ms = new MemoryStream();
                    responseStream.CopyTo(ms);
                    var responseBytes = ms.ToArray();
                    var responseBytesHandle = GCHandle.Alloc(responseBytes, GCHandleType.Pinned);
                    try
                    {
                        var responseStreamCom = NativeMethods.SHCreateMemStream(responseBytesHandle.AddrOfPinnedObject(), (uint)responseBytes.Length);
                        var response = Control.WebView2Environment.CreateWebResourceResponse(
                            responseStreamCom, 200, "OK", $"Content-Type: {responseContentType}");
                        e.SetResponse(response);
                    }
                    finally
                    {
                        responseBytesHandle.Free();
                    }
                }
            }
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
                Control.Navigate(url);
            }
        }

        bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (Control != null)
                {
                    Control.WebResourceRequested -= HandleWebResourceRequested;
                    Control.WebMessageRecieved -= HandleWebMessageReceived;

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
