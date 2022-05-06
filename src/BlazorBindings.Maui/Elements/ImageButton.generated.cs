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
    public partial class ImageButton : View
    {
        static ImageButton()
        {
            ElementHandlerRegistry.RegisterElementHandler<ImageButton>(
                renderer => new ImageButtonHandler(renderer, new MC.ImageButton()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Aspect? Aspect { get; set; }
        [Parameter] public Color BorderColor { get; set; }
        [Parameter] public double? BorderWidth { get; set; }
        [Parameter] public int? CornerRadius { get; set; }
        [Parameter] public bool? IsOpaque { get; set; }
        [Parameter] public Thickness? Padding { get; set; }
        [Parameter] public MC.ImageSource Source { get; set; }

        public new MC.ImageButton NativeControl => ((ImageButtonHandler)ElementHandler).ImageButtonControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Aspect != null)
            {
                builder.AddAttribute(nameof(Aspect), (int)Aspect.Value);
            }
            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor));
            }
            if (BorderWidth != null)
            {
                builder.AddAttribute(nameof(BorderWidth), AttributeHelper.DoubleToString(BorderWidth.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), CornerRadius.Value);
            }
            if (IsOpaque != null)
            {
                builder.AddAttribute(nameof(IsOpaque), IsOpaque.Value);
            }
            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }
            if (Source != null)
            {
                builder.AddAttribute(nameof(Source), AttributeHelper.ObjectToDelegate(Source));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
