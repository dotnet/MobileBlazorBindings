// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Class is used as generic parameter.
    internal class InitializedVerticalStackLayout : VerticalStackLayout
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        // ContentView was originally used here.
        // It was replaced with VerticalStackLayout as a workaround for bug
        // https://github.com/dotnet/maui/issues/5248.
        [Parameter] public new MC.VerticalStackLayout NativeControl { get; set; }

        static InitializedVerticalStackLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<InitializedVerticalStackLayout>(
                (renderer, _, component) => new VerticalStackLayoutHandler(renderer, component.NativeControl));
        }
    }
}
