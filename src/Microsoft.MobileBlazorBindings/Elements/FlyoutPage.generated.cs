// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class FlyoutPage : Page
    {
        static FlyoutPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<FlyoutPage>(
                renderer => new FlyoutPageHandler(renderer, new XF.FlyoutPage()));
        }

        [Parameter] public XF.FlyoutLayoutBehavior? FlyoutLayoutBehavior { get; set; }
        [Parameter] public bool? IsGestureEnabled { get; set; }
        [Parameter] public bool? IsPresented { get; set; }

        public new XF.FlyoutPage NativeControl => ((FlyoutPageHandler)ElementHandler).FlyoutPageControl;

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
    }
}
