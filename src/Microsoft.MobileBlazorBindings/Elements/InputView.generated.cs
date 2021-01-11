// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class InputView : View
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
        /// Gets or sets the Keyboard for the InputView. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Keyboard" /> to use for the InputView.
        /// </value>
        [Parameter] public XF.Keyboard Keyboard { get; set; }
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
        /// Gets or sets the text of the input view. This is a bindable property.
        /// </summary>
        /// <value>
        /// A string containing the text of the input view. The default value is null.
        /// </value>
        [Parameter] public string Text { get; set; }
        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }

        public new XF.InputView NativeControl => ((InputViewHandler)ElementHandler).InputViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
            }
            if (IsReadOnly != null)
            {
                builder.AddAttribute(nameof(IsReadOnly), IsReadOnly.Value);
            }
            if (IsSpellCheckEnabled != null)
            {
                builder.AddAttribute(nameof(IsSpellCheckEnabled), IsSpellCheckEnabled.Value);
            }
            if (Keyboard != null)
            {
                builder.AddAttribute(nameof(Keyboard), AttributeHelper.ObjectToDelegate(Keyboard));
            }
            if (MaxLength != null)
            {
                builder.AddAttribute(nameof(MaxLength), MaxLength.Value);
            }
            if (Placeholder != null)
            {
                builder.AddAttribute(nameof(Placeholder), Placeholder);
            }
            if (PlaceholderColor != null)
            {
                builder.AddAttribute(nameof(PlaceholderColor), AttributeHelper.ColorToString(PlaceholderColor.Value));
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
