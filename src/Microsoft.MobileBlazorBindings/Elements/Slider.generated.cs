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

        /// <summary>
        /// Gets or sets the maximum selectable value for the Slider. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
        [Parameter] public double? Maximum { get; set; }
        /// <summary>
        /// Gets or sets the color of the portion of the slider track that contains the maximum value of the slider.
        /// </summary>
        /// <value>
        /// Thhe color of the portion of the slider track that contains the maximum value of the slider.
        /// </value>
        [Parameter] public XF.Color? MaximumTrackColor { get; set; }
        /// <summary>
        /// Gets or sets the minimum selectable value for the Slider. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
        [Parameter] public double? Minimum { get; set; }
        /// <summary>
        /// Gets or sets the color of the portion of the slider track that contains the minimum value of the slider.
        /// </summary>
        /// <value>
        /// Thhe color of the portion of the slider track that contains the minimum value of the slider.
        /// </value>
        [Parameter] public XF.Color? MinimumTrackColor { get; set; }
        /// <summary>
        /// Gets or sets the color of the slider thumb button.
        /// </summary>
        /// <value>
        /// The color of the slider thumb button.
        /// </value>
        [Parameter] public XF.Color? ThumbColor { get; set; }
        [Parameter] public XF.ImageSource ThumbImageSource { get; set; }
        /// <summary>
        /// Gets or sets the current value. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
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
                builder.AddAttribute(nameof(ThumbImageSource), AttributeHelper.ObjectToDelegate(ThumbImageSource));
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
