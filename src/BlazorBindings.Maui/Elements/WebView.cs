// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class WebView : View
    {
        static WebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<WebView>(renderer => new WebViewHandler(renderer, new MC.WebView()));
        }

        [Parameter] public MC.WebViewSource Source { get; set; }
        [Parameter] public EventCallback<string> OnWebMessageReceived { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onwebmessagereceived", EventCallback.Factory.Create<WebMessageEventArgs>(this, HandleOnWebMessageReceived));

            switch (Source)
            {
                case MC.HtmlWebViewSource htmlWebViewSource:
                    builder.AddAttribute(nameof(MC.HtmlWebViewSource), htmlWebViewSource.Html);
                    break;
                case MC.UrlWebViewSource urlWebViewSource:
                    builder.AddAttribute(nameof(MC.UrlWebViewSource), urlWebViewSource.Url);
                    break;
            }
        }

        private Task HandleOnWebMessageReceived(WebMessageEventArgs args)
            => OnWebMessageReceived.InvokeAsync(args.Message);

#pragma warning disable CA1812 // Avoid uninstantiated internal classes
        internal class WebMessageEventArgs : EventArgs
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
        {
            public string Message { get; set; }
        }
    }
}
