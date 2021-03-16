// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class DatePicker : View
    {
        static DatePicker()
        {
            ElementHandlerRegistry.RegisterElementHandler<DatePicker>(
                renderer => new DatePickerHandler(renderer, new XF.DatePicker()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets or sets the displayed date. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:System.DateTime" /> displayed in the DatePicker.
        /// </value>
        [Parameter] public DateTime? Date { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the date picker text is bold, italic, or neither.
        /// </summary>
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets or sets the font family for the picker text.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets or sets the size of the font for the text in the picker.
        /// </summary>
        /// <value>
        /// A <see langword="double" /> that indicates the size of the font.
        /// </value>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// The format of the date to display to the user. This is a dependency property.
        /// </summary>
        /// <value>
        /// A valid date format.
        /// </value>
        [Parameter] public string Format { get; set; }
        /// <summary>
        /// The highest date selectable for this DatePicker. This is a bindable property.
        /// </summary>
        /// <value>
        /// The maximum <see cref="T:System.DateTime" /> selectable for the DateEntry. Default December 31, 2100.
        /// </value>
        [Parameter] public DateTime? MaximumDate { get; set; }
        /// <summary>
        /// The lowest date selectable for this DatePicker. This is a bindable property.
        /// </summary>
        /// <value>
        /// The minimum <see cref="T:System.DateTime" /> selectable for the DateEntry. Default January 1, 1900.
        /// </value>
        [Parameter] public DateTime? MinimumDate { get; set; }
        /// <summary>
        /// Gets or sets the text color for the date picker.
        /// </summary>
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }

        public new XF.DatePicker NativeControl => ((DatePickerHandler)ElementHandler).DatePickerControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CharacterSpacing != null)
            {
                builder.AddAttribute(nameof(CharacterSpacing), AttributeHelper.DoubleToString(CharacterSpacing.Value));
            }
            if (Date != null)
            {
                builder.AddAttribute(nameof(Date), AttributeHelper.DateTimeToString(Date.Value));
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
            if (Format != null)
            {
                builder.AddAttribute(nameof(Format), Format);
            }
            if (MaximumDate != null)
            {
                builder.AddAttribute(nameof(MaximumDate), AttributeHelper.DateTimeToString(MaximumDate.Value));
            }
            if (MinimumDate != null)
            {
                builder.AddAttribute(nameof(MinimumDate), AttributeHelper.DateTimeToString(MinimumDate.Value));
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

        static partial void RegisterAdditionalHandlers();
    }
}
