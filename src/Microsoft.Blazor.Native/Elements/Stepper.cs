using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Blazor.Native.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class Stepper : View
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

        [Parameter] public EventCallback<double> ValueChanged { get; set; }

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

            builder.AddAttribute("onvaluechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleValueChanged));
        }

        private Task HandleValueChanged(ChangeEventArgs evt)
        {
            return ValueChanged.InvokeAsync((double)evt.Value);
        }
    }
}
