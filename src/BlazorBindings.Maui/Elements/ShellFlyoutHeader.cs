// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;

namespace BlazorBindings.Maui.Elements
{
    // TODO: The base class for this type isn't quite right. It should be something more like View, because that's
    // what Shell.FlyoutHeader requires. But View doesn't have a public ctor and isn't useful on its own, so
    // ContentView is used, which has a settable Content property.
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class ShellFlyoutHeader : ContentView
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        static ShellFlyoutHeader()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellFlyoutHeader>(
                renderer => new ShellFlyoutHeaderHandler(renderer, new DummyElement()));
        }
    }
}
