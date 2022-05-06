// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Shell : Page
    {
        static Shell()
        {
            ElementHandlerRegistry.RegisterElementHandler<Shell>(
                renderer => new ShellHandler(renderer, new MC.Shell()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color FlyoutBackgroundColor { get; set; }
        [Parameter] public MC.ImageSource FlyoutBackgroundImage { get; set; }
        [Parameter] public Aspect? FlyoutBackgroundImageAspect { get; set; }
        [Parameter] public FlyoutBehavior? FlyoutBehavior { get; set; }
        [Parameter] public MC.FlyoutHeaderBehavior? FlyoutHeaderBehavior { get; set; }
        [Parameter] public double? FlyoutHeight { get; set; }
        [Parameter] public MC.ImageSource FlyoutIcon { get; set; }
        [Parameter] public bool? FlyoutIsPresented { get; set; }
        [Parameter] public MC.ScrollMode? FlyoutVerticalScrollMode { get; set; }
        [Parameter] public double? FlyoutWidth { get; set; }

        public new MC.Shell NativeControl => ((ShellHandler)ElementHandler).ShellControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutBackgroundColor != null)
            {
                builder.AddAttribute(nameof(FlyoutBackgroundColor), AttributeHelper.ColorToString(FlyoutBackgroundColor));
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

        static partial void RegisterAdditionalHandlers();
    }
}
