// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public sealed partial class FlyoutFlyoutPageHandler : ContentPageHandler
    {
        public FlyoutFlyoutPageHandler(NativeComponentRenderer renderer, XF.ContentPage flyoutPageControl) : base(renderer, flyoutPageControl)
        {
            FlyoutPageControl = flyoutPageControl ?? throw new ArgumentNullException(nameof(flyoutPageControl));

            // The Flyout page must have its Title set: https://github.com/xamarin/Xamarin.Forms/blob/5.0.0/Xamarin.Forms.Core/FlyoutPage.cs#L74
            ContentPageControl.Title = "Title";
        }

        public XF.ContentPage FlyoutPageControl { get; }
    }
}
