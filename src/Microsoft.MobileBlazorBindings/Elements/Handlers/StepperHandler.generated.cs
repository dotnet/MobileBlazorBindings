// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class StepperHandler : ViewHandler
    {
        private static readonly double IncrementDefaultValue = XF.Stepper.IncrementProperty.DefaultValue is double value ? value : default;
        private static readonly double MaximumDefaultValue = XF.Stepper.MaximumProperty.DefaultValue is double value ? value : default;
        private static readonly double MinimumDefaultValue = XF.Stepper.MinimumProperty.DefaultValue is double value ? value : default;
        private static readonly double ValueDefaultValue = XF.Stepper.ValueProperty.DefaultValue is double value ? value : default;

        public StepperHandler(NativeComponentRenderer renderer, XF.Stepper stepperControl) : base(renderer, stepperControl)
        {
            StepperControl = stepperControl ?? throw new ArgumentNullException(nameof(stepperControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Stepper StepperControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Stepper.Increment):
                    StepperControl.Increment = AttributeHelper.StringToDouble((string)attributeValue, IncrementDefaultValue);
                    break;
                case nameof(XF.Stepper.Maximum):
                    StepperControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue, MaximumDefaultValue);
                    break;
                case nameof(XF.Stepper.Minimum):
                    StepperControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue, MinimumDefaultValue);
                    break;
                case nameof(XF.Stepper.Value):
                    StepperControl.Value = AttributeHelper.StringToDouble((string)attributeValue, ValueDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
