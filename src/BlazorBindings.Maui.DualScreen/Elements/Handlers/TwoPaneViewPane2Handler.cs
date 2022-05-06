// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed class TwoPaneViewPane2Handler : ContentViewHandler
    {
        public TwoPaneViewPane2Handler(NativeComponentRenderer renderer, XF.ContentView twoPaneView2Control) : base(renderer, twoPaneView2Control)
        {
            TwoPaneView2Control = twoPaneView2Control ?? throw new ArgumentNullException(nameof(twoPaneView2Control));
        }

        public XF.ContentView TwoPaneView2Control { get; }
    }
}
