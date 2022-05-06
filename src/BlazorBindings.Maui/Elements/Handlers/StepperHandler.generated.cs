// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class StepperHandler : ViewHandler
    {
        private static readonly double IncrementDefaultValue = MC.Stepper.IncrementProperty.DefaultValue is double value ? value : default;
        private static readonly double MaximumDefaultValue = MC.Stepper.MaximumProperty.DefaultValue is double value ? value : default;
        private static readonly double MinimumDefaultValue = MC.Stepper.MinimumProperty.DefaultValue is double value ? value : default;
        private static readonly double ValueDefaultValue = MC.Stepper.ValueProperty.DefaultValue is double value ? value : default;

        public StepperHandler(NativeComponentRenderer renderer, MC.Stepper stepperControl) : base(renderer, stepperControl)
        {
            StepperControl = stepperControl ?? throw new ArgumentNullException(nameof(stepperControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Stepper StepperControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Stepper.Increment):
                    StepperControl.Increment = AttributeHelper.StringToDouble((string)attributeValue, IncrementDefaultValue);
                    break;
                case nameof(MC.Stepper.Maximum):
                    StepperControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue, MaximumDefaultValue);
                    break;
                case nameof(MC.Stepper.Minimum):
                    StepperControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue, MinimumDefaultValue);
                    break;
                case nameof(MC.Stepper.Value):
                    StepperControl.Value = AttributeHelper.StringToDouble((string)attributeValue, ValueDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
