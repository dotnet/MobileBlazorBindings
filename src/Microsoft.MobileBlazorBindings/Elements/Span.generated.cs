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

        /// <summary>
        /// Gets or sets the Color of the span background.
        /// </summary>
        [Parameter] public XF.Color? BackgroundColor { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the span is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family to which the font for the text in the span belongs.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the text in the span.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Gets or sets the Color for the text in the span.
        /// </summary>
        [Parameter] public XF.Color? ForegroundColor { get; set; }
        /// <summary>
        /// Gets or sets the multiplier to apply to the default line height when displaying text.
        /// </summary>
        /// <value>
        /// The multiplier to apply to the default line height when displaying text.
        /// </value>
        [Parameter] public double? LineHeight { get; set; }
        /// <summary>
        /// Gets or sets the text of the span.
        /// </summary>
        [Parameter] public string Text { get; set; }
        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        [Parameter] public XF.Color? TextColor { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.TextDecorations" /> applied to this span.
        /// </summary>
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
