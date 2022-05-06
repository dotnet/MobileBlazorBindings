// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using WVM = Microsoft.AspNetCore.Components.WebView.Maui;

namespace BlazorBindings.Maui.Elements
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