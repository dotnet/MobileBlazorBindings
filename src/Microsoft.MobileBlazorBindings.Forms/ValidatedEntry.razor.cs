// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Forms
{
    public partial class ValidatedEntry : InputViewWrapper
    {
        [Parameter] public XF.ClearButtonVisibility? ClearButtonVisibility { get; set; }
        /// <summary>
        /// Gets or sets the position of the cursor.
        /// </summary>
        /// <value>
        /// The position of the cursor.
        /// </value>
        [Parameter] public int? CursorPosition { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the Entry element text is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family for the Entry element text.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the Entry element text.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Gets or sets the horizontal text alignment.
        /// </summary>
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates if the entry should visually obscure typed text.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the element is a password box; otherwise, <see langword="false" />. Default value is <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsPassword { get; set; }
        /// <summary>
        /// Gets or sets a value that controls whether text prediction and automatic text correction is on or off.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if text correction is on. Otherwise, <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsTextPredictionEnabled { get; set; }
        /// <summary>
        /// Gets or sets an enumeration value that controls the appearance of the return button.
        /// </summary>
        /// <value>
        /// An enumeration value that controls the appearance of the return button.
        /// </value>
        [Parameter] public XF.ReturnType? ReturnType { get; set; }
        /// <summary>
        /// Gets the length of the selection.
        /// </summary>
        /// <value>
        /// The length of the selection.
        /// </value>
        [Parameter] public int? SelectionLength { get; set; }
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }

        protected override IEnumerable<KeyValuePair<string, object>> AdditionalProperties
        {
            get
            {
                yield return new KeyValuePair<string, object>(nameof(ClearButtonVisibility), ClearButtonVisibility);
                yield return new KeyValuePair<string, object>(nameof(CursorPosition), CursorPosition);
                yield return new KeyValuePair<string, object>(nameof(FontAttributes), FontAttributes);
                yield return new KeyValuePair<string, object>(nameof(FontFamily), FontFamily);
                yield return new KeyValuePair<string, object>(nameof(FontSize), FontSize);
                yield return new KeyValuePair<string, object>(nameof(HorizontalTextAlignment), HorizontalTextAlignment);
                yield return new KeyValuePair<string, object>(nameof(IsPassword), IsPassword);
                yield return new KeyValuePair<string, object>(nameof(IsTextPredictionEnabled), IsTextPredictionEnabled);
                yield return new KeyValuePair<string, object>(nameof(ReturnType), ReturnType);
                yield return new KeyValuePair<string, object>(nameof(SelectionLength), SelectionLength);
                yield return new KeyValuePair<string, object>(nameof(VerticalTextAlignment), VerticalTextAlignment);
            }
        }
    }
}
