// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Slider : View
    {
        static Slider()
        {
            ElementHandlerRegistry.RegisterElementHandler<Slider>(
                renderer => new SliderHandler(renderer, new XF.Slider()));
        }

        [Parameter] public double? Maximum { get; set; }
        [Parameter] public XF.Color? MaximumTrackColor { get; set; }
        [Parameter] public double? Minimum { get; set; }
        [Parameter] public XF.Color? MinimumTrackColor { get; set; }
        [Parameter] public XF.Color? ThumbColor { get; set; }
        [Parameter] public XF.ImageSource ThumbImageSource { get; set; }
        [Parameter] public double? Value { get; set; }

        public new XF.Slider NativeControl => ((SliderHandler)ElementHandler).SliderControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Maximum != null)
            {
                builder.AddAttribute(nameof(Maximum), AttributeHelper.DoubleToString(Maximum.Value));
            }
            if (MaximumTrackColor != null)
            {
                builder.AddAttribute(nameof(MaximumTrackColor), AttributeHelper.ColorToString(MaximumTrackColor.Value));
            }
            if (Minimum != null)
            {
                builder.AddAttribute(nameof(Minimum), AttributeHelper.DoubleToString(Minimum.Value));
            }
            if (MinimumTrackColor != null)
            {
                builder.AddAttribute(nameof(MinimumTrackColor), AttributeHelper.ColorToString(MinimumTrackColor.Value));
            }
            if (ThumbColor != null)
            {
                builder.AddAttribute(nameof(ThumbColor), AttributeHelper.ColorToString(ThumbColor.Value));
            }
            if (ThumbImageSource != null)
            {
                builder.AddAttribute(nameof(ThumbImageSource), AttributeHelper.ImageSourceToString(ThumbImageSource));
            }
            if (Value != null)
            {
                builder.AddAttribute(nameof(Value), AttributeHelper.DoubleToString(Value.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
