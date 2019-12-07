using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class StepperHandler : ViewHandler
    {
        public StepperHandler(NativeComponentRenderer renderer, XF.Stepper stepperControl) : base(renderer, stepperControl)
        {
            StepperControl = stepperControl ?? throw new ArgumentNullException(nameof(stepperControl));
            StepperControl.ValueChanged += (s, e) =>
            {
                if (ValueChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ValueChangedEventHandlerId, null, new ChangeEventArgs { Value = StepperControl.Value }));
                }
            };
        }

        public ulong ValueChangedEventHandlerId { get; set; }
        public XF.Stepper StepperControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Stepper.Increment):
                    StepperControl.Increment = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Stepper.Maximum):
                    StepperControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Stepper.Minimum):
                    StepperControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Stepper.Value):
                    StepperControl.Value = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case "onvaluechanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => ValueChangedEventHandlerId = 0);
                    ValueChangedEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
