// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;

namespace BlazorBindings.Maui.Elements
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class FlyoutDetailPage : ContentPage
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        static FlyoutDetailPage()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<FlyoutDetailPage>(renderer => new FlyoutDetailPageHandler(renderer, new FlyoutDetailPageContentPage()));
        }
    }
}
