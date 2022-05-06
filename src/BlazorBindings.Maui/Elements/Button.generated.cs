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
    public partial class Button : View
    {
        static Button()
        {
            ElementHandlerRegistry.RegisterElementHandler<Button>(
                renderer => new ButtonHandler(renderer, new MC.Button()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BorderColor { get; set; }
        [Parameter] public double? BorderWidth { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public int? CornerRadius { get; set; }
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        [Parameter] public bool? FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public MC.ImageSource ImageSource { get; set; }
        [Parameter] public LineBreakMode? LineBreakMode { get; set; }
        [Parameter] public Thickness? Padding { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextTransform? TextTransform { get; set; }

        public new MC.Button NativeControl => ((ButtonHandler)ElementHandler).ButtonControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor));
            }
            if (BorderWidth != null)
            {
                builder.AddAttribute(nameof(BorderWidth), AttributeHelper.DoubleToString(BorderWidth.Value));
            }
            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), CornerRadius.Value);
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (FontAutoScalingEnabled != null)
            {
                builder.AddAttribute(nameof(FontAutoScalingEnabled), FontAutoScalingEnabled.Value);
            }
            if (FontFamily != null)
            {
                builder.AddAttribute(nameof(FontFamily), FontFamily);
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), AttributeHelper.DoubleToString(FontSize.Value));
            }
            if (ImageSource != null)
            {
                builder.AddAttribute(nameof(ImageSource), AttributeHelper.ObjectToDelegate(ImageSource));
            }
            if (LineBreakMode != null)
            {
                builder.AddAttribute(nameof(LineBreakMode), (int)LineBreakMode.Value);
            }
            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor));
            }
            if (TextTransform != null)
            {
                builder.AddAttribute(nameof(TextTransform), (int)TextTransform.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
