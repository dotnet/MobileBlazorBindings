// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Forms
{
    public abstract class InputViewWrapper : ViewWrapper
    {
        /// <summary>
        /// Gets or sets a value that indicates the number of device-independent units that should be in between characters in the text displayed by the Entry. Applies to Text and Placeholder.
        /// </summary>
        /// <value>
        /// The number of device-independent units that should be in between characters in the text.
        /// </value>
        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates whether user should be prevented from modifying the text. Default is <see langword="false" />.
        /// </summary>
        /// <value>
        /// If <see langword="true" />, user cannot modify text. Else, <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsReadOnly { get; set; }
        /// <summary>
        /// Gets or sets a value that controls whether spell checking is enabled.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if spell checking is enabled. Otherwise <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsSpellCheckEnabled { get; set; }
        /// <summary>
        /// Gets or sets the maximum allowed length of input.
        /// </summary>
        /// <value>
        /// An integer in the interval [0,<c>int.MaxValue</c>]. The default value is <c>Int.MaxValue</c>.
        /// </value>
        [Parameter] public int? MaxLength { get; set; }
        /// <summary>
        /// Gets or sets the text that is displayed when the control is empty.
        /// </summary>
        /// <value>
        /// The text that is displayed when the control is empty.
        /// </value>
        [Parameter] public string Placeholder { get; set; }
        /// <summary>
        /// Gets or sets the color of the placeholder text.
        /// </summary>
        /// <value>
        /// The color of the placeholder text.
        /// </value>
        [Parameter] public XF.Color? PlaceholderColor { get; set; }
        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }

        private IEnumerable<KeyValuePair<string, object>> InputViewProperties
        {
            get
            {
                yield return new KeyValuePair<string, object>(nameof(CharacterSpacing), CharacterSpacing);
                yield return new KeyValuePair<string, object>(nameof(IsReadOnly), IsReadOnly);
                yield return new KeyValuePair<string, object>(nameof(IsSpellCheckEnabled), IsSpellCheckEnabled);
                yield return new KeyValuePair<string, object>(nameof(MaxLength), MaxLength);
                yield return new KeyValuePair<string, object>(nameof(Placeholder), Placeholder);
                yield return new KeyValuePair<string, object>(nameof(PlaceholderColor), PlaceholderColor);
                yield return new KeyValuePair<string, object>(nameof(TextColor), TextColor);
                yield return new KeyValuePair<string, object>(nameof(TextTransform), TextTransform);
            }
        }

        protected override IEnumerable<KeyValuePair<string, object>> WrappedProperties
        {
            get
            {
                return base.WrappedProperties.Concat(InputViewProperties);
            }
        }
    }
}
