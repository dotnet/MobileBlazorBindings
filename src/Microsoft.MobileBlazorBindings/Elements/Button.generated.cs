// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Button : View
    {
        static Button()
        {
            ElementHandlerRegistry.RegisterElementHandler<Button>(
                renderer => new ButtonHandler(renderer, new XF.Button()));
        }

        [Parameter] public XF.Color? BorderColor { get; set; }
        [Parameter] public double? BorderWidth { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public int? CornerRadius { get; set; }
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public XF.ImageSource ImageSource { get; set; }
        [Parameter] public XF.Thickness? Padding { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }

        public new XF.Button NativeControl => ((ButtonHandler)ElementHandler).ButtonControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor.Value));
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
                builder.AddAttribute(nameof(ImageSource), AttributeHelper.ImageSourceToString(ImageSource));
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

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
