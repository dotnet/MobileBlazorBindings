// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Span : GestureElement
    {
        static Span()
        {
            ElementHandlerRegistry.RegisterElementHandler<Span>(
                renderer => new SpanHandler(renderer, new XF.Span()));
        }

        [Parameter] public XF.Color? BackgroundColor { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public XF.Color? ForegroundColor { get; set; }
        [Parameter] public double? LineHeight { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }

        public new XF.Span NativeControl => ((SpanHandler)ElementHandler).SpanControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundColor != null)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor.Value));
            }
            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (FontFamily != null)
            {
                builder.AddAttribute(nameof(FontFamily), FontFamily);
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), AttributeHelper.DoubleToString(FontSize.Value));
            }
            if (ForegroundColor != null)
            {
                builder.AddAttribute(nameof(ForegroundColor), AttributeHelper.ColorToString(ForegroundColor.Value));
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
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor.Value));
            }
            if (TextDecorations != null)
            {
                builder.AddAttribute(nameof(TextDecorations), (int)TextDecorations.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
