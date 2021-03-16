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
    public partial class TimePicker : View
    {
        static TimePicker()
        {
            ElementHandlerRegistry.RegisterElementHandler<TimePicker>(
                renderer => new TimePickerHandler(renderer, new XF.TimePicker()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// TGets a value that indicates whether the font for the searchbar text is bold, italic, or neither.
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
        /// The format of the time to display to the user. This is a bindable property.
        /// </summary>
        /// <value>
        /// A valid time format string.
        /// </value>
        [Parameter] public string Format { get; set; }
        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextTransform? TextTransform { get; set; }
        /// <summary>
        /// Gets or sets the displayed time. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:System.TimeSpan" /> displayed in the TimePicker.
        /// </value>
        [Parameter] public TimeSpan? Time { get; set; }

        public new XF.TimePicker NativeControl => ((TimePickerHandler)ElementHandler).TimePickerControl;

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
            if (Format != null)
            {
                builder.AddAttribute(nameof(Format), Format);
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor.Value));
            }
            if (TextTransform != null)
            {
                builder.AddAttribute(nameof(TextTransform), (int)TextTransform.Value);
            }
            if (Time != null)
            {
                builder.AddAttribute(nameof(Time), AttributeHelper.TimeSpanToString(Time.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
