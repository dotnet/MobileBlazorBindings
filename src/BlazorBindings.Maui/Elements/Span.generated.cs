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
    public partial class Span : GestureElement
    {
        static Span()
        {
            ElementHandlerRegistry.RegisterElementHandler<Span>(
                renderer => new SpanHandler(renderer, new MC.Span()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BackgroundColor { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        [Parameter] public bool? FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public double? LineHeight { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextDecorations? TextDecorations { get; set; }
        [Parameter] public TextTransform? TextTransform { get; set; }

        public new MC.Span NativeControl => ((SpanHandler)ElementHandler).SpanControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundColor != null)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor));
            }
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
            if (LineHeight != null)
            {
                builder.AddAttribute(nameof(LineHeight), AttributeHelper.DoubleToString(LineHeight.Value));
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

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
