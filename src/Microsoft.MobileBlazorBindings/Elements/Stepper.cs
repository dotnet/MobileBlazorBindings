// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
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

        [Parameter] public double? Increment { get; set; }
        [Parameter] public double? Maximum { get; set; }
        [Parameter] public double? Minimum { get; set; }
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
            if (Value != null)
            {
                builder.AddAttribute(nameof(Value), AttributeHelper.DoubleToString(Value.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
