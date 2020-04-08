using System;
using WebWindows.Blazor.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using XamarinTest.WPF;

[assembly: ExportRenderer(typeof(SpartanWebView), typeof(SpartanWebViewRenderer))]

namespace XamarinTest.WPF
{
#pragma warning disable CS0618 // Type or member is obsolete
	public class SpartanWebViewRenderer : ViewRenderer<SpartanWebView, Microsoft.Toolkit.Wpf.UI.Controls.WebView>, IWebViewDelegate
	{
		protected override void OnElementChanged(ElementChangedEventArgs<SpartanWebView> e)
		{
			if (e.OldElement != null) // Clear old element event
			{
			}

			if (e.NewElement != null)
			{
				if (Control == null) // construct and SetNativeControl and suscribe control event
				{
					SetNativeControl(new Microsoft.Toolkit.Wpf.UI.Controls.WebView());
				}

				// Update control property 
				Load();

				// Suscribe element event
			}

			base.OnElementChanged(e);
		}

		public void LoadHtml(string html, string baseUrl)
		{
			Control.NavigateToString(html);
		}

		public void LoadUrl(string url)
		{
			Control.Navigate(new Uri(url));
		}

		void Load()
		{
			if (Element.Source != null)
			{
				Element.Source.Load(this);
			}
		}
	}
#pragma warning restore CS0618 // Type or member is obsolete
}
