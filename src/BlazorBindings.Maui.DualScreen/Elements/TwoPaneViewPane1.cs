// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;

namespace Microsoft.MobileBlazorBindings.Elements
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class TwoPaneViewPane1 : ContentPage
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        static TwoPaneViewPane1()
        {
            // TODO: This is not quite right, but it works for now. The TwoPaneView's Pane1 and Pane2 should
            // be placeholder controls and use ParentChildManager to set their children as the values of the
            // Pane1 and Pane2 properties of the Xamarin.Forms control. Right now there is an XF.ContentView
            // that is always instantiated and set as the Pane1 and Pane2 properties, and then their children
            // are children of the XF.ContentView. This creates an unnecessary and unwanted intermediate
            // child. It's mostly harmless, so we'll go with it for now.
            ElementHandlerRegistry
                .RegisterElementHandler<TwoPaneViewPane1>(renderer => new TwoPaneViewPane1Handler(renderer, new TwoPaneViewPane1View()));
        }
    }
}
