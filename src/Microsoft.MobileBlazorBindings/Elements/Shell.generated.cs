// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
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

        //[Parameter] public ShellItem CurrentItem { get; set; }
        //[Parameter] public ShellNavigationState CurrentState { get; }
        [Parameter] public XF.ImageSource FlyoutBackgroundImage { get; set; }
        [Parameter] public XF.Aspect? FlyoutBackgroundImageAspect { get; set; }
        [Parameter] public XF.Color? FlyoutBackgroundColor { get; set; }
        [Parameter] public XF.FlyoutBehavior? FlyoutBehavior { get; set; }
        [Parameter] public RenderFragment FlyoutHeader { get; set; }
        [Parameter] public XF.FlyoutHeaderBehavior? FlyoutHeaderBehavior { get; set; }
        //[Parameter] public DataTemplate FlyoutHeaderTemplate { get; set; }
        [Parameter] public XF.ImageSource FlyoutIcon { get; set; }
        //[Parameter] public bool? FlyoutIsPresented { get; set; } // TODO: Two-way binding?
        //[Parameter] public IList<ShellItem> Items { get; } // TODO: Not needed? This is the Children collection
        //[Parameter] public DataTemplate ItemTemplate { get; set; }
        //[Parameter] public DataTemplate MenuItemTemplate { get; set; }

        public new XF.Shell NativeControl => ((ShellHandler)ElementHandler).ShellControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            //[Parameter] public ShellItem CurrentItem { get; set; }
            //[Parameter] public ShellNavigationState CurrentState { get; }
            if (FlyoutBackgroundImageAspect != null)
            {
                // NOTE: The Aspect must be set before the image or else an exception is thrown
                builder.AddAttribute(nameof(FlyoutBackgroundImageAspect), (int)FlyoutBackgroundImageAspect.Value);
            }
            if (FlyoutBackgroundImage != null)
            {
                builder.AddAttribute(nameof(FlyoutBackgroundImage), AttributeHelper.ImageSourceToString(FlyoutBackgroundImage));
            }
            if (FlyoutBackgroundColor != null)
            {
                builder.AddAttribute(nameof(FlyoutBackgroundColor), AttributeHelper.ColorToString(FlyoutBackgroundColor.Value));
            }
            if (FlyoutBehavior != null)
            {
                builder.AddAttribute(nameof(FlyoutBehavior), (int)FlyoutBehavior.Value);
            }
            if (FlyoutHeaderBehavior != null)
            {
                builder.AddAttribute(nameof(FlyoutHeaderBehavior), (int)FlyoutHeaderBehavior.Value);
            }
            //[Parameter] public DataTemplate FlyoutHeaderTemplate { get; set; }
            if (FlyoutIcon != null)
            {
                builder.AddAttribute(nameof(FlyoutIcon), AttributeHelper.ImageSourceToString(FlyoutIcon));
            }
            //[Parameter] public bool? FlyoutIsPresented { get; set; } // TODO: Two-way binding?
            //[Parameter] public DataTemplate ItemTemplate { get; set; }
            //[Parameter] public DataTemplate MenuItemTemplate { get; set; }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
