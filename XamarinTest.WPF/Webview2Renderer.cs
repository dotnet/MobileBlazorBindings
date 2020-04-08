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
using WebWindows.Blazor.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.WPF;
using XamarinTest.WPF;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(Webview2Renderer))]

namespace XamarinTest.WPF
{
	public class Webview2Renderer : ViewRenderer<ExtendedWebView, WebView2Control>, IWebViewDelegate
    {
		[DllImport("Shlwapi.dll", SetLastError = false, ExactSpelling = true)]
		static extern IStream SHCreateMemStream(IntPtr pInit, uint cbInit);

		WebNavigationEvent _eventState;
		bool _updating;

		protected override void OnElementChanged(ElementChangedEventArgs<ExtendedWebView> e)
		{
			_ = HandleElementChangedAsync(e);
		}

		private async Task HandleElementChangedAsync(ElementChangedEventArgs<ExtendedWebView> e)
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
					SetNativeControl(new WebView2Control());
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

			if (e.PropertyName == WebView.SourceProperty.PropertyName)
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

			Device.BeginInvokeOnMainThread(() => {
				throw new NotImplementedException();
				//tcr.SetResult((string)Control.InvokeScript("eval", new[] { script }));
			});

			return await task.ConfigureAwait(false);
		}

		void OnGoBackRequested(object sender, EventArgs eventArgs)
		{
			if (Control.CanGoBack)
			{
				_eventState = WebNavigationEvent.Back;
				Control.GoBack();
			}

			UpdateCanGoBackForward();
		}

		void OnGoForwardRequested(object sender, EventArgs eventArgs)
		{
			if (Control.CanGoForward)
			{
				_eventState = WebNavigationEvent.Forward;
				Control.GoForward();
			}
			UpdateCanGoBackForward();
		}

		void OnReloadRequested(object sender, EventArgs eventArgs)
		{
			Control.Reload();
		}

		void SendNavigated(UrlWebViewSource source, WebNavigationEvent evnt, WebNavigationResult result)
		{
			Console.WriteLine("SendNavigated : " + source.Url);
			_updating = true;
			((IElementController)Element).SetValueFromRenderer(WebView.SourceProperty, source);
			_updating = false;

			Element.SendNavigated(new WebNavigatedEventArgs(evnt, source, source.Url, result));

			UpdateCanGoBackForward();
			_eventState = WebNavigationEvent.NewPage;
		}

		void UpdateCanGoBackForward()
		{
			((IWebViewController)Element).CanGoBack = Control.CanGoBack;
			((IWebViewController)Element).CanGoForward = Control.CanGoForward;
		}

		void WebBrowserOnNavigated(object sender, NavigationCompletedEventArgs navigationEventArgs)
		{
			if (navigationEventArgs.IsSuccess && Element.Source is UrlWebViewSource urlWebViewSource)
			{
				SendNavigated(urlWebViewSource, _eventState, WebNavigationResult.Success);
				UpdateCanGoBackForward();
			}
		}

		void WebBrowserOnNavigating(object sender, NavigationStartingEventArgs navigatingEventArgs)
		{
			if (navigatingEventArgs.Uri == null) return;

			string url = navigatingEventArgs.Uri;
			var args = new WebNavigatingEventArgs(_eventState, new UrlWebViewSource { Url = url }, url);

			Element.SendNavigating(args);

			navigatingEventArgs.Cancel = args.Cancel;

			// reset in this case because this is the last event we will get
			if (args.Cancel)
				_eventState = WebNavigationEvent.NewPage;
		}

		void WebBrowserOnNavigationFailed(object sender, NavigationFailedEventArgs navigationFailedEventArgs)
		{
			if (navigationFailedEventArgs.Uri == null) return;

			string url = navigationFailedEventArgs.Uri.IsAbsoluteUri ? navigationFailedEventArgs.Uri.AbsoluteUri : navigationFailedEventArgs.Uri.OriginalString;
			SendNavigated(new UrlWebViewSource { Url = url }, _eventState, WebNavigationResult.Failure);
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
