using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Model
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Picker : View
    {
        public new XF.Picker NativeControl => base.NativeControl as XF.Picker;

        [Parameter] public IList ItemsSource { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string ItemDisplayBinding { get; set; }
        [Parameter] public object SelectedItem { get; set; }
        [Parameter] public EventCallback<Model> SelectedItemChanged { get; set; }
        static Picker()
        {
            ElementHandlerRegistry.RegisterElementHandler<Picker>(renderer => new PickerHandler(renderer, new XF.Picker()));
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (ItemsSource != null)
            {
                builder.AddAttribute(nameof(ItemsSource), AttributeHelper.IListToDelegate(ItemsSource));
            }
            if(Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }
            if (ItemDisplayBinding != null)
            {
                builder.AddAttribute(nameof(ItemDisplayBinding), ItemDisplayBinding);
            }
            if (SelectedItem != null)
            {
                builder.AddAttribute(nameof(SelectedItem), AttributeHelper.ObjectToDelegate(SelectedItem));
            }

            builder.AddAttribute("onselecteditemchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleSelectedItemChanged));
        }

        private Task HandleSelectedItemChanged(ChangeEventArgs evt)
        {
            return SelectedItemChanged.InvokeAsync((Model)evt.Value);
        }
    }
}
