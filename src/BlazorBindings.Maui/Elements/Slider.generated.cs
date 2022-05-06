// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Slider : View
    {
        static Slider()
        {
            ElementHandlerRegistry.RegisterElementHandler<Slider>(
                renderer => new SliderHandler(renderer, new MC.Slider()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? Maximum { get; set; }
        [Parameter] public Color MaximumTrackColor { get; set; }
        [Parameter] public double? Minimum { get; set; }
        [Parameter] public Color MinimumTrackColor { get; set; }
        [Parameter] public Color ThumbColor { get; set; }
        [Parameter] public MC.ImageSource ThumbImageSource { get; set; }
        [Parameter] public double? Value { get; set; }

        public new MC.Slider NativeControl => ((SliderHandler)ElementHandler).SliderControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Maximum != null)
            {
                builder.AddAttribute(nameof(Maximum), AttributeHelper.DoubleToString(Maximum.Value));
            }
            if (MaximumTrackColor != null)
            {
                builder.AddAttribute(nameof(MaximumTrackColor), AttributeHelper.ColorToString(MaximumTrackColor));
            }
            if (Minimum != null)
            {
                builder.AddAttribute(nameof(Minimum), AttributeHelper.DoubleToString(Minimum.Value));
            }
            if (MinimumTrackColor != null)
            {
                builder.AddAttribute(nameof(MinimumTrackColor), AttributeHelper.ColorToString(MinimumTrackColor));
            }
            if (ThumbColor != null)
            {
                builder.AddAttribute(nameof(ThumbColor), AttributeHelper.ColorToString(ThumbColor));
            }
            if (ThumbImageSource != null)
            {
                builder.AddAttribute(nameof(ThumbImageSource), AttributeHelper.ObjectToDelegate(ThumbImageSource));
            }
            if (Value != null)
            {
                builder.AddAttribute(nameof(Value), AttributeHelper.DoubleToString(Value.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
