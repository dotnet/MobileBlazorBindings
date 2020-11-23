// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Forms
{
    public partial class ValidationLabel<TValue> : ViewWrapper
    {
        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the label is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family to which the font for the label belongs.
        /// </summary>
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
        /// The <see cref="XF.LineBreakMode" /> value for the Label. The default is <see cref="XF.LineBreakMode.WordWrap" />
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
        /// Gets or sets the maximum number of lines allowed in the <see cref="XF.Label" />.
        /// </summary>
        [Parameter] public int? MaxLines { get; set; }
        [Parameter] public XF.Thickness? Padding { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="XF.Color" /> for the text of this Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="XF.Color" /> value.
        /// </value>
        [Parameter] public XF.Color? TextColor { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="XF.TextDecorations" /> applied to <see cref="XF.Label.Text" />.
        /// </summary>
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }
        [Parameter] public XF.TextType? TextType { get; set; }
        /// <summary>
        /// Gets or sets the vertical alignement of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }

        protected override IEnumerable<KeyValuePair<string, object>> AdditionalProperties
        {
            get
            {
                yield return new KeyValuePair<string, object>(nameof(CharacterSpacing), CharacterSpacing);
                yield return new KeyValuePair<string, object>(nameof(FontAttributes), FontAttributes);
                yield return new KeyValuePair<string, object>(nameof(FontFamily), FontFamily);
                yield return new KeyValuePair<string, object>(nameof(FontSize), FontSize);
                yield return new KeyValuePair<string, object>(nameof(HorizontalTextAlignment), HorizontalTextAlignment);
                yield return new KeyValuePair<string, object>(nameof(LineBreakMode), LineBreakMode);
                yield return new KeyValuePair<string, object>(nameof(LineHeight), LineHeight);
                yield return new KeyValuePair<string, object>(nameof(MaxLines), MaxLines);
                yield return new KeyValuePair<string, object>(nameof(Padding), Padding);
                yield return new KeyValuePair<string, object>(nameof(TextColor), TextColor);
                yield return new KeyValuePair<string, object>(nameof(TextDecorations), TextDecorations);
                yield return new KeyValuePair<string, object>(nameof(TextTransform), TextTransform);
                yield return new KeyValuePair<string, object>(nameof(TextType), TextType);
                yield return new KeyValuePair<string, object>(nameof(VerticalTextAlignment), VerticalTextAlignment);
            }
        }
    }
}
