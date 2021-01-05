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

        /// <summary>
        /// Gets or sets the background color of the <see cref="P:Xamarin.Forms.Shell" /> Flyout.
        /// </summary>
        [Parameter] public XF.Color? FlyoutBackgroundColor { get; set; }
        [Parameter] public XF.ImageSource FlyoutBackgroundImage { get; set; }
        [Parameter] public XF.Aspect? FlyoutBackgroundImageAspect { get; set; }
        /// <summary>
        /// Setting the <see cref="T:Xamarin.Forms.FlyoutBehavior" /> property to Disabled hides the flyout, which is useful when you only have one ShellItem. The other valid FlyoutBehavior values are Flyout (default), and Locked.
        /// </summary>
        [Parameter] public XF.FlyoutBehavior? FlyoutBehavior { get; set; }
        /// <summary>
        /// Setting the FlyoutHeaderBehavior to CollapseOnScroll collapses the flyout as scrolling occurs. The other valid FlyoutHeaderBehavior values are Default, Fixed, and Scroll (scroll with the menu items).
        /// </summary>
        [Parameter] public XF.FlyoutHeaderBehavior? FlyoutHeaderBehavior { get; set; }
        [Parameter] public double? FlyoutHeight { get; set; }
        /// <summary>
        /// Gets or sets the icon that, when pressed, opens the <see cref="P:Xamarin.Forms.Shell" /> Flyout.
        /// </summary>
        /// <value>
        /// The default icon is a "hamburger" icon.
        /// </value>
        [Parameter] public XF.ImageSource FlyoutIcon { get; set; }
        /// <summary>
        /// Gets or sets the visible status of the <see cref="P:Xamarin.Forms.Shell" /> Flyout.
        /// </summary>
        [Parameter] public bool? FlyoutIsPresented { get; set; }
        [Parameter] public XF.ScrollMode? FlyoutVerticalScrollMode { get; set; }
        [Parameter] public double? FlyoutWidth { get; set; }

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
                builder.AddAttribute(nameof(FlyoutBackgroundImage), AttributeHelper.ObjectToDelegate(FlyoutBackgroundImage));
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
            if (FlyoutHeight != null)
            {
                builder.AddAttribute(nameof(FlyoutHeight), AttributeHelper.DoubleToString(FlyoutHeight.Value));
            }
            if (FlyoutIcon != null)
            {
                builder.AddAttribute(nameof(FlyoutIcon), AttributeHelper.ObjectToDelegate(FlyoutIcon));
            }
            if (FlyoutIsPresented != null)
            {
                builder.AddAttribute(nameof(FlyoutIsPresented), FlyoutIsPresented.Value);
            }
            if (FlyoutVerticalScrollMode != null)
            {
                builder.AddAttribute(nameof(FlyoutVerticalScrollMode), (int)FlyoutVerticalScrollMode.Value);
            }
            if (FlyoutWidth != null)
            {
                builder.AddAttribute(nameof(FlyoutWidth), AttributeHelper.DoubleToString(FlyoutWidth.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
