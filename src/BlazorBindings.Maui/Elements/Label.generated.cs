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
    public partial class Label : View
    {
        static Label()
        {
            ElementHandlerRegistry.RegisterElementHandler<Label>(
                renderer => new LabelHandler(renderer, new MC.Label()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        [Parameter] public bool? FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public LineBreakMode? LineBreakMode { get; set; }
        [Parameter] public double? LineHeight { get; set; }
        [Parameter] public int? MaxLines { get; set; }
        [Parameter] public Thickness? Padding { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextDecorations? TextDecorations { get; set; }
        [Parameter] public TextTransform? TextTransform { get; set; }
        [Parameter] public TextType? TextType { get; set; }
        [Parameter] public TextAlignment? VerticalTextAlignment { get; set; }

        public new MC.Label NativeControl => ((LabelHandler)ElementHandler).LabelControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
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
            if (HorizontalTextAlignment != null)
            {
                builder.AddAttribute(nameof(HorizontalTextAlignment), (int)HorizontalTextAlignment.Value);
            }
            if (LineBreakMode != null)
            {
                builder.AddAttribute(nameof(LineBreakMode), (int)LineBreakMode.Value);
            }
            if (LineHeight != null)
            {
                builder.AddAttribute(nameof(LineHeight), AttributeHelper.DoubleToString(LineHeight.Value));
            }
            if (MaxLines != null)
            {
                builder.AddAttribute(nameof(MaxLines), MaxLines.Value);
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
            if (TextDecorations != null)
            {
                builder.AddAttribute(nameof(TextDecorations), (int)TextDecorations.Value);
            }
            if (TextTransform != null)
            {
                builder.AddAttribute(nameof(TextTransform), (int)TextTransform.Value);
            }
            if (TextType != null)
            {
                builder.AddAttribute(nameof(TextType), (int)TextType.Value);
            }
            if (VerticalTextAlignment != null)
            {
                builder.AddAttribute(nameof(VerticalTextAlignment), (int)VerticalTextAlignment.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
