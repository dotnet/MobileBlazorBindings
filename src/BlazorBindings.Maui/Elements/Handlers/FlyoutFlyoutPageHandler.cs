// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public sealed partial class FlyoutFlyoutPageHandler : ContentPageHandler
    {
        public FlyoutFlyoutPageHandler(NativeComponentRenderer renderer, MC.ContentPage flyoutPageControl) : base(renderer, flyoutPageControl)
        {
            FlyoutPageControl = flyoutPageControl ?? throw new ArgumentNullException(nameof(flyoutPageControl));

            // The Flyout page must have its Title set: https://github.com/xamarin/Xamarin.Forms/blob/5.0.0/Xamarin.Forms.Core/FlyoutPage.cs#L74
            ContentPageControl.Title = "Title";
        }

        public MC.ContentPage FlyoutPageControl { get; }
    }
}
