using BlazorDesktop.Elements;
using BlazorDesktop.Windows;
using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wrapper;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows.Threading;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.WPF;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendedAnaheimRenderer))]

namespace BlazorDesktop.Windows
{
    public class WebViewExtendedAnaheimRenderer : ViewRenderer<WebViewExtended, WebView2Control>, XF.IWebViewDelegate
    {
		[DllImport("Shlwapi.dll", SetLastError = false, ExactSpelling = true)]
		static extern IStream SHCreateMemStream(IntPtr pInit, uint cbInit);

		XF.WebNavigationEvent _eventState;
		bool _updating;

		protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
		{
			_ = HandleElementChangedAsync(e);
		}

		private async Task HandleElementChangedAsync(ElementChangedEventArgs<WebViewExtended> e)
		{
			if (e.OldElement != null) // Clear old element event
			{
				e.OldElement.EvalRequested -= OnEvalRequested;
				e.OldElement.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
				e.OldElement.GoBackRequested -= OnGoBackRequested;
				e.OldElement.GoForwardRequested -= OnGoForwardRequested;
				e.OldElement.ReloadRequested -= OnReloadRequested;
				e.OldElement.SendMessageFromJSToDotNetRequested -= OnSendMessageFromJSToDotNetRequested;
			}

			if (e.NewElement != null)
			{
				if (Control == null) // construct and SetNativeControl and suscribe control event
				{
					SetNativeControl(new WebView2Control { MinHeight = 200 });
					await WaitForBrowserCreatedAsync();

					Control.AddScriptToExecuteOnDocumentCreated("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };", callbackArgs => { });

					Control.WebMessageRecieved += HandleWebMessageReceived;
					Control.NavigationCompleted += WebBrowserOnNavigated;
					Control.NavigationStarting += WebBrowserOnNavigating;
					Control.AddWebResourceRequestedFilter("*", WEBVIEW2_WEB_RESOURCE_CONTEXT.WEBVIEW2_WEB_RESOURCE_CONTEXT_ALL);
					Control.WebResourceRequested += HandleWebResourceRequested;
				}

				// Update control property 
				Load();

				// Suscribe element event
				Element.EvalRequested += OnEvalRequested;
				Element.EvaluateJavaScriptRequested += OnEvaluateJavaScriptRequested;
				Element.GoBackRequested += OnGoBackRequested;
				Element.GoForwardRequested += OnGoForwardRequested;
				Element.ReloadRequested += OnReloadRequested;
				Element.SendMessageFromJSToDotNetRequested += OnSendMessageFromJSToDotNetRequested;
			}

			base.OnElementChanged(e);
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
						var responseStreamCom = SHCreateMemStream(responseBytesHandle.AddrOfPinnedObject(), (uint)responseBytes.Length);
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
				if (!_updating)
					Load();
			}
		}

		void Load()
		{
			if (Element.Source != null)
				Element.Source.Load(this);

			UpdateCanGoBackForward();
		}

		public void LoadHtml(string html, string baseUrl)
		{
			if (html == null)
				return;

			Control.NavigateToString(html);
		}

		public void LoadUrl(string url)
		{
			if (url == null)
				return;

			Control.Navigate(url);
		}


		void OnEvalRequested(object sender, EvalRequested eventArg)
		{
			Control.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
				throw new NotImplementedException()));
			//Control.InvokeScript("eval", eventArg.Script)););
		}

		async Task<string> OnEvaluateJavaScriptRequested(string script)
		{
			var tcr = new TaskCompletionSource<string>();
			var task = tcr.Task;

			XF.Device.BeginInvokeOnMainThread(() => {
				throw new NotImplementedException();
				//tcr.SetResult((string)Control.InvokeScript("eval", new[] { script }));
			});

			return await task.ConfigureAwait(false);
		}

		void OnGoBackRequested(object sender, EventArgs eventArgs)
		{
			if (Control.CanGoBack)
			{
				_eventState = XF.WebNavigationEvent.Back;
				Control.GoBack();
			}

			UpdateCanGoBackForward();
		}

		void OnGoForwardRequested(object sender, EventArgs eventArgs)
		{
			if (Control.CanGoForward)
			{
				_eventState = XF.WebNavigationEvent.Forward;
				Control.GoForward();
			}
			UpdateCanGoBackForward();
		}

		void OnReloadRequested(object sender, EventArgs eventArgs)
		{
			Control.Reload();
		}

		void SendNavigated(XF.UrlWebViewSource source, XF.WebNavigationEvent evnt, XF.WebNavigationResult result)
		{
			Console.WriteLine("SendNavigated : " + source.Url);
			_updating = true;
			((XF.IElementController)Element).SetValueFromRenderer(XF.WebView.SourceProperty, source);
			_updating = false;

			Element.SendNavigated(new XF.WebNavigatedEventArgs(evnt, source, source.Url, result));

			UpdateCanGoBackForward();
			_eventState = XF.WebNavigationEvent.NewPage;
		}

		void UpdateCanGoBackForward()
		{
			((XF.IWebViewController)Element).CanGoBack = Control.CanGoBack;
			((XF.IWebViewController)Element).CanGoForward = Control.CanGoForward;
		}

		void WebBrowserOnNavigated(object sender, NavigationCompletedEventArgs navigationEventArgs)
		{
			if (navigationEventArgs.IsSuccess && Element.Source is XF.UrlWebViewSource urlWebViewSource)
			{
				SendNavigated(urlWebViewSource, _eventState, XF.WebNavigationResult.Success);
				UpdateCanGoBackForward();
			}
		}

		void WebBrowserOnNavigating(object sender, NavigationStartingEventArgs navigatingEventArgs)
		{
			if (navigatingEventArgs.Uri == null) return;

			string url = navigatingEventArgs.Uri;
			var args = new XF.WebNavigatingEventArgs(_eventState, new XF.UrlWebViewSource { Url = url }, url);

			Element.SendNavigating(args);

			navigatingEventArgs.Cancel = args.Cancel;

			// reset in this case because this is the last event we will get
			if (args.Cancel)
				_eventState = XF.WebNavigationEvent.NewPage;
		}

		void WebBrowserOnNavigationFailed(object sender, NavigationFailedEventArgs navigationFailedEventArgs)
		{
			if (navigationFailedEventArgs.Uri == null) return;

			string url = navigationFailedEventArgs.Uri.IsAbsoluteUri ? navigationFailedEventArgs.Uri.AbsoluteUri : navigationFailedEventArgs.Uri.OriginalString;
			SendNavigated(new XF.UrlWebViewSource { Url = url }, _eventState, XF.WebNavigationResult.Failure);
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
					Control.NavigationCompleted -= WebBrowserOnNavigated;
					Control.NavigationStarting -= WebBrowserOnNavigating;
					Control.WebResourceRequested -= HandleWebResourceRequested;
					Control.WebMessageRecieved -= HandleWebMessageReceived;

					// By waiting a bit before disposing, we can make switching between tabs much faster.
					// If not, then it will tear down all the Edge child processes then has to start them back
					// up immediately as the new tab content is built.
					Task.Factory.StartNew(async () =>
					{
						await Task.Delay(1000);
						Control.Dispatcher.Invoke(Control.Dispose);
					});
				}

				if (Element != null)
				{
					Element.EvalRequested -= OnEvalRequested;
					Element.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
					Element.GoBackRequested -= OnGoBackRequested;
					Element.GoForwardRequested -= OnGoForwardRequested;
					Element.ReloadRequested -= OnReloadRequested;
				}
			}

			_isDisposed = true;
			base.Dispose(disposing);
		}
	}
}
