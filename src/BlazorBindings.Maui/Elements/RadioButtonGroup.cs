using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class RadioButtonGroup<T> : StackLayout
    {
        static RadioButtonGroup()
        {
            ElementHandlerRegistry.RegisterElementHandler<RadioButtonGroup<T>>(renderer =>
                new RadioButtonGroupHandler(renderer, new MC.StackLayout()));
        }

        [Parameter] public T SelectedValue { get; set; }
        [Parameter] public EventCallback<T> SelectedValueChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (SelectedValue != null)
            {
                builder.AddAttribute(nameof(SelectedValue), AttributeHelper.ObjectToDelegate(SelectedValue));
            }
            if (SelectedValueChanged.HasDelegate)
            {
                builder.AddAttribute("onselectedvaluechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleSelectedValueChanged));
            }

            base.RenderAttributes(builder);
        }

        private Task HandleSelectedValueChanged(ChangeEventArgs args)
        {
            return SelectedValueChanged.InvokeAsync((T)args.Value);
        }
    }
}
