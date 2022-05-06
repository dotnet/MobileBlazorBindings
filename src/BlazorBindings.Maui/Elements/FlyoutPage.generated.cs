// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlyoutPage : Page
    {
        static FlyoutPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<FlyoutPage>(
                renderer => new FlyoutPageHandler(renderer, new MC.FlyoutPage()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.FlyoutLayoutBehavior? FlyoutLayoutBehavior { get; set; }
        [Parameter] public bool? IsGestureEnabled { get; set; }
        [Parameter] public bool? IsPresented { get; set; }

        public new MC.FlyoutPage NativeControl => ((FlyoutPageHandler)ElementHandler).FlyoutPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutLayoutBehavior != null)
            {
                builder.AddAttribute(nameof(FlyoutLayoutBehavior), (int)FlyoutLayoutBehavior.Value);
            }
            if (IsGestureEnabled != null)
            {
                builder.AddAttribute(nameof(IsGestureEnabled), IsGestureEnabled.Value);
            }
            if (IsPresented != null)
            {
                builder.AddAttribute(nameof(IsPresented), IsPresented.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
