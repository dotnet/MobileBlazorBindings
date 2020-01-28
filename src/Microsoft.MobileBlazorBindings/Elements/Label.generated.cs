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
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public XF.LineBreakMode? LineBreakMode { get; set; }
        [Parameter] public double? LineHeight { get; set; }
        [Parameter] public int? MaxLines { get; set; }
        [Parameter] public XF.Thickness? Padding { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }
        [Parameter] public XF.TextType? TextType { get; set; }
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
