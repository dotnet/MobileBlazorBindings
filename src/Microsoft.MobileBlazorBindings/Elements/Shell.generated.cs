// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Shell : Page
    {
        static Shell()
        {
            ElementHandlerRegistry.RegisterElementHandler<Shell>(
                renderer => new ShellHandler(renderer, new XF.Shell()));
        }

        [Parameter] public XF.Color? FlyoutBackgroundColor { get; set; }
        [Parameter] public XF.ImageSource FlyoutBackgroundImage { get; set; }
        [Parameter] public XF.Aspect? FlyoutBackgroundImageAspect { get; set; }
        [Parameter] public XF.FlyoutBehavior? FlyoutBehavior { get; set; }
        [Parameter] public XF.FlyoutHeaderBehavior? FlyoutHeaderBehavior { get; set; }
        [Parameter] public XF.ImageSource FlyoutIcon { get; set; }
        [Parameter] public bool? FlyoutIsPresented { get; set; }

        public new XF.Shell NativeControl => ((ShellHandler)ElementHandler).ShellControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutBackgroundColor != null)
            {
                builder.AddAttribute(nameof(FlyoutBackgroundColor), AttributeHelper.ColorToString(FlyoutBackgroundColor.Value));
            }
            if (FlyoutBackgroundImage != null)
            {
                builder.AddAttribute(nameof(FlyoutBackgroundImage), AttributeHelper.ImageSourceToString(FlyoutBackgroundImage));
            }
            if (FlyoutBackgroundImageAspect != null)
            {
                builder.AddAttribute(nameof(FlyoutBackgroundImageAspect), (int)FlyoutBackgroundImageAspect.Value);
            }
            if (FlyoutBehavior != null)
            {
                builder.AddAttribute(nameof(FlyoutBehavior), (int)FlyoutBehavior.Value);
            }
            if (FlyoutHeaderBehavior != null)
            {
                builder.AddAttribute(nameof(FlyoutHeaderBehavior), (int)FlyoutHeaderBehavior.Value);
            }
            if (FlyoutIcon != null)
            {
                builder.AddAttribute(nameof(FlyoutIcon), AttributeHelper.ImageSourceToString(FlyoutIcon));
            }
            if (FlyoutIsPresented != null)
            {
                builder.AddAttribute(nameof(FlyoutIsPresented), FlyoutIsPresented.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
