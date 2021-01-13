// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed class FlyoutDetailPageHandler : ContentPageHandler
    {
        public FlyoutDetailPageHandler(NativeComponentRenderer renderer, XF.ContentPage flyoutPageControl) : base(renderer, flyoutPageControl)
        {
            FlyoutPageControl = flyoutPageControl ?? throw new ArgumentNullException(nameof(flyoutPageControl));
        }

        public XF.ContentPage FlyoutPageControl { get; }
    }
}
