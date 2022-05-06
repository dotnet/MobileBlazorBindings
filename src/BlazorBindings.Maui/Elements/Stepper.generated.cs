// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Stepper : View
    {
        static Stepper()
        {
            ElementHandlerRegistry.RegisterElementHandler<Stepper>(
                renderer => new StepperHandler(renderer, new MC.Stepper()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? Increment { get; set; }
        [Parameter] public double? Maximum { get; set; }
        [Parameter] public double? Minimum { get; set; }
        [Parameter] public double? Value { get; set; }

        public new MC.Stepper NativeControl => ((StepperHandler)ElementHandler).StepperControl;

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

        static partial void RegisterAdditionalHandlers();
    }
}
