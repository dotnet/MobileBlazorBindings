// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;

namespace Microsoft.MobileBlazorBindings.Elements
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
