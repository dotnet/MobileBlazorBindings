using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WebWindows.Blazor.XamarinForms
{
    public class BlazorView : ContentView
    {
        private readonly ExtendedWebView _webView = new ExtendedWebView();

        public BlazorView()
        {
            Content = _webView;
            _webView.Source = new HtmlWebViewSource { Html = "Blazor will go here" };
        }
    }
}
