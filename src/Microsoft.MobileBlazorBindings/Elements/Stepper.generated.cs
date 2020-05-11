// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Stepper : View
    {
        static Stepper()
        {
            ElementHandlerRegistry.RegisterElementHandler<Stepper>(
                renderer => new StepperHandler(renderer, new XF.Stepper()));
        }

        /// <summary>
        /// Gets or sets the increment by which Value is increased or decreased. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
        [Parameter] public double? Increment { get; set; }
        /// <summary>
        /// Gets or sets the maximum selectable value. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
        [Parameter] public double? Maximum { get; set; }
        /// <summary>
        /// Gets or sets the minimum selectabel value. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
        [Parameter] public double? Minimum { get; set; }
        [Parameter] public int? StepperPosition { get; set; }
        /// <summary>
        /// Gets or sets the current value. This is a bindable property.
        /// </summary>
        /// <value>
        /// A double.
        /// </value>
        [Parameter] public double? Value { get; set; }

        public new XF.Stepper NativeControl => ((StepperHandler)ElementHandler).StepperControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Increment != null)
            {
                builder.AddAttribute(nameof(Increment), AttributeHelper.DoubleToString(Increment.Value));
            }
            if (Maximum != null)
            {
                builder.AddAttribute(nameof(Maximum), AttributeHelper.DoubleToString(Maximum.Value));
            }
            if (Minimum != null)
            {
                builder.AddAttribute(nameof(Minimum), AttributeHelper.DoubleToString(Minimum.Value));
            }
            if (StepperPosition != null)
            {
                builder.AddAttribute(nameof(StepperPosition), StepperPosition.Value);
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
