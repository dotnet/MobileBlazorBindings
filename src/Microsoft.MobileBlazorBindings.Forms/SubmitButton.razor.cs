// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Forms
{
    public partial class SubmitButton : ViewWrapper
    {
        /// <summary>
        /// Gets or sets a color that describes the border stroke color of the button. This is a bindable property.
        /// </summary>
        /// <value>
        /// The color that is used as the border stroke color; the default is <see cref="XF.Color.Default" />.
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
        /// Gets or sets the <see cref="XF.Color" /> for the text of the button. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="XF.Color" /> value.
        /// </value>
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }

        protected override IEnumerable<KeyValuePair<string, object>> AdditionalProperties
        {
            get
            {
                yield return new KeyValuePair<string, object>(nameof(BorderColor), BorderColor);
                yield return new KeyValuePair<string, object>(nameof(BorderWidth), BorderWidth);
                yield return new KeyValuePair<string, object>(nameof(CharacterSpacing), CharacterSpacing);
                yield return new KeyValuePair<string, object>(nameof(CornerRadius), CornerRadius);
                yield return new KeyValuePair<string, object>(nameof(FontAttributes), FontAttributes);
                yield return new KeyValuePair<string, object>(nameof(FontFamily), FontFamily);
                yield return new KeyValuePair<string, object>(nameof(FontSize), FontSize);
                yield return new KeyValuePair<string, object>(nameof(ImageSource), ImageSource);
                yield return new KeyValuePair<string, object>(nameof(Padding), Padding);
                yield return new KeyValuePair<string, object>(nameof(Text), Text);
                yield return new KeyValuePair<string, object>(nameof(TextColor), TextColor);
                yield return new KeyValuePair<string, object>(nameof(TextTransform), TextTransform);
            }
        }
    }
}
