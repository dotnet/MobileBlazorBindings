// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class StepperHandler : ViewHandler
    {
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
                    StepperControl.Increment = AttributeHelper.StringToDouble((string)attributeValue, 1.00);
                    break;
                case nameof(XF.Stepper.Maximum):
                    StepperControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue, 100.00);
                    break;
                case nameof(XF.Stepper.Minimum):
                    StepperControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Stepper.StepperPosition):
                    StepperControl.StepperPosition = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Stepper.Value):
                    StepperControl.Value = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
