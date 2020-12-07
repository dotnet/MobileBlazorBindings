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

        /// <summary>
        /// Gets or sets a color that describes the border stroke color of the button. This is a bindable property.
        /// </summary>
        /// <value>
        /// The color that is used as the border stroke color; the default is <see cref="P:Xamarin.Forms.Color.Default" />.
        /// </value>
        [Parameter] public XF.Color? BorderColor { get; set; }
        /// <summary>
        /// Gets or sets the width of the border. This is a bindable property.
        /// </summary>
        /// <value>
        /// The width of the button border; the default is 0.
        /// </value>
        [Parameter] public double? BorderWidth { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets or sets the corner radius for the button, in device-independent units.
        /// </summary>
        /// <value>
        /// The corner radius for the button, in device-independent units.
        /// </value>
        [Parameter] public int? CornerRadius { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the button text is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family to which the font for the button text belongs.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets or sets the size of the font of the button text.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Allows you to display a bitmap image on the Button.
        /// </summary>
        [Parameter] public XF.ImageSource ImageSource { get; set; }
        /// <summary>
        /// Gets or sets the padding for the button.
        /// </summary>
        /// <value>
        /// The padding for the button.
        /// </value>
        [Parameter] public XF.Thickness? Padding { get; set; }
        /// <summary>
        /// Gets or sets the Text displayed as the content of the button. This is a bindable property.
        /// </summary>
        /// <value>
        /// The text displayed in the button. The default value is <see langword="null" />.
        /// </value>
        [Parameter] public string Text { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> for the text of the button. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Color" /> value.
        /// </value>
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }

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
                builder.AddAttribute(nameof(ImageSource), AttributeHelper.ObjectToDelegate(ImageSource));
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
            if (TextTransform != null)
            {
                builder.AddAttribute(nameof(TextTransform), (int)TextTransform.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
