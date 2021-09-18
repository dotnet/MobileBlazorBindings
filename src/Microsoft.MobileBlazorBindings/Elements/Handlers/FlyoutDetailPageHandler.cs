// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed class FlyoutDetailPageHandler : ContentPageHandler
    {
        public FlyoutDetailPageHandler(NativeComponentRenderer renderer, MC.ContentPage flyoutPageControl) : base(renderer, flyoutPageControl)
        {
            FlyoutPageControl = flyoutPageControl ?? throw new ArgumentNullException(nameof(flyoutPageControl));
        }

        public MC.ContentPage FlyoutPageControl { get; }
    }
}
