// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using WVM = Microsoft.AspNetCore.Components.WebView.Maui;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class BlazorWebView : View
    {
        static BlazorWebView()
        {
            ElementHandlerRegistry.RegisterElementHandler<BlazorWebView>(
                renderer => new BlazorWebViewHandler(renderer, new WVM.BlazorWebView()));
        }

        [Parameter] public string HostPage { get; set; }
        [Parameter] public RenderFragment RootComponents { get; set; }

        protected override RenderFragment GetChildContent() => RootComponents;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HostPage != null)
            {
                builder.AddAttribute(nameof(HostPage), HostPage);
            }
        }
    }
}