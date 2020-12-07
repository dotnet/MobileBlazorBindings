// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Label : View
    {
        static Label()
        {
            ElementHandlerRegistry.RegisterElementHandler<Label>(
                renderer => new LabelHandler(renderer, new XF.Label()));
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the label is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets or sets the font family to which the font for the label belongs.
        /// </summary>
        /// <value>
        /// The font family, or null for the platform default font family.
        /// </value>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the label.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Gets or sets the horizontal alignment of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        /// <summary>
        /// Gets or sets the LineBreakMode for the Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.LineBreakMode" /> value for the Label. The default is <see cref="F:Xamarin.Forms.LineBreakMode.WordWrap" />
        /// </value>
        [Parameter] public XF.LineBreakMode? LineBreakMode { get; set; }
        /// <summary>
        /// Gets or sets the multiplier to apply to the default line height when displaying text.
        /// </summary>
        /// <value>
        /// The multiplier to apply to the default line height when displaying text.
        /// </value>
        [Parameter] public double? LineHeight { get; set; }
        /// <summary>
        /// Gets or sets the maximum number of lines allowed in the <see cref="T:Xamarin.Forms.Label" />.
        /// </summary>
        [Parameter] public int? MaxLines { get; set; }
        [Parameter] public XF.Thickness? Padding { get; set; }
        /// <summary>
        /// Gets or sets the text for the Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:System.String" /> value to be displayed inside of the Label.
        /// </value>
        [Parameter] public string Text { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> for the text of this Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Color" /> value.
        /// </value>
        [Parameter] public XF.Color? TextColor { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.TextDecorations" /> applied to <see cref="P:Xamarin.Forms.Label.Text" />.
        /// </summary>
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }
        /// <summary>
        /// Determines whether the Label should display plain text or HTML text.
        /// </summary>
        [Parameter] public XF.TextType? TextType { get; set; }
        /// <summary>
        /// Gets or sets the vertical alignement of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }

        public new XF.Label NativeControl => ((LabelHandler)ElementHandler).LabelControl;

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
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor.Value));
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
    }
}
