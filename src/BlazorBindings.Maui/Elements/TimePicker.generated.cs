// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TimePicker : View
    {
        static TimePicker()
        {
            ElementHandlerRegistry.RegisterElementHandler<TimePicker>(
                renderer => new TimePickerHandler(renderer, new MC.TimePicker()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? CharacterSpacing { get; set; }
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        [Parameter] public bool? FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public string Format { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TimeSpan? Time { get; set; }

        public new MC.TimePicker NativeControl => ((TimePickerHandler)ElementHandler).TimePickerControl;

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
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor));
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
