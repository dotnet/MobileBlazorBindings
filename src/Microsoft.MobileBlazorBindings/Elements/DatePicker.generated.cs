// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class DatePicker : View
    {
        static DatePicker()
        {
            ElementHandlerRegistry.RegisterElementHandler<DatePicker>(
                renderer => new DatePickerHandler(renderer, new MC.DatePicker()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public DateTime? Date { get; set; }
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        [Parameter] public bool? FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public string Format { get; set; }
        [Parameter] public DateTime? MaximumDate { get; set; }
        [Parameter] public DateTime? MinimumDate { get; set; }
        [Parameter] public Color TextColor { get; set; }

        public new MC.DatePicker NativeControl => ((DatePickerHandler)ElementHandler).DatePickerControl;

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
            if (FontAutoScalingEnabled != null)
            {
                builder.AddAttribute(nameof(FontAutoScalingEnabled), FontAutoScalingEnabled.Value);
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
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
