// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed class TwoPaneViewPane1Handler : ContentViewHandler
    {
        public TwoPaneViewPane1Handler(NativeComponentRenderer renderer, XF.ContentView twoPaneView1Control) : base(renderer, twoPaneView1Control)
        {
            TwoPaneView1Control = twoPaneView1Control ?? throw new ArgumentNullException(nameof(twoPaneView1Control));
        }

        public XF.ContentView TwoPaneView1Control { get; }
    }
}
