// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
#pragma warning disable CA1724 // Type name conflicts with namespace
    public class WebView : View
#pragma warning restore CA1724 // Type name conflicts with namespace
    {
        static WebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<WebView>(renderer => new WebViewHandler(renderer, new WebViewExtended(new DefaulBlazorErrorHandler())));
        }

        [Parameter] public XF.WebViewSource Source { get; set; }
        [Parameter] public EventCallback<string> OnWebMessageReceived { get; set; }

        public void SendMessage(string message)
        {
            ((WebViewHandler)ElementHandler).Control.SendMessage(message);
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onwebmessagereceived", EventCallback.Factory.Create<WebMessageEventArgs>(this, HandleOnWebMessageReceived));

            switch (Source)
            {
                case XF.HtmlWebViewSource htmlWebViewSource:
                    builder.AddAttribute(nameof(XF.HtmlWebViewSource), htmlWebViewSource.Html);
                    break;
                case XF.UrlWebViewSource urlWebViewSource:
                    builder.AddAttribute(nameof(XF.UrlWebViewSource), urlWebViewSource.Url);
                    break;
            }
        }

        private Task HandleOnWebMessageReceived(WebMessageEventArgs args)
            => OnWebMessageReceived.InvokeAsync(args.Message);

        internal class WebMessageEventArgs : EventArgs
        {
            public string Message { get; set; }
        }
    }
}
