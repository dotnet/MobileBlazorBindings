// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System;
using System.Collections.Generic;
using WVM = Microsoft.AspNetCore.Components.WebView.Maui;

namespace BlazorBindings.Maui.Elements
{
    public class RootComponent : NativeControlComponentBase
    {
        static RootComponent()
        {
            ElementHandlerRegistry.RegisterElementHandler<RootComponent>(
                _ => new RootComponentHandler(new WVM.RootComponent()));
        }

        [Parameter] public string Selector { get; set; }

        [Parameter] public Type ComponentType { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        [Parameter] public IDictionary<string, object> Parameters { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Selector != null)
            {
                builder.AddAttribute(nameof(Selector), Selector);
            }
            if (ComponentType != null)
            {
                builder.AddAttribute(nameof(ComponentType), AttributeHelper.ObjectToDelegate(ComponentType));
            }
            if (Parameters != null)
            {
                builder.AddAttribute(nameof(Parameters), AttributeHelper.ObjectToDelegate(Parameters));
            }
        }
    }
}