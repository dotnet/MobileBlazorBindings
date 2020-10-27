// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class BlazorWebView : View
    {
        static BlazorWebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<BlazorWebView>(renderer => new BlazorWebViewHandler(renderer, new MobileBlazorBindingsBlazorWebView(renderer.Dispatcher)));
        }

        [Inject] internal IHost Host { get; private set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
        [Parameter] public string ContentRoot { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var element = (MobileBlazorBindingsBlazorWebView)NativeControl;

            if (firstRender)
            {
                element.Host = Host;
                await element.InitAsync().ConfigureAwait(false);
            }

            await element.Render(ChildContent).ConfigureAwait(false);
        }
    }
}
