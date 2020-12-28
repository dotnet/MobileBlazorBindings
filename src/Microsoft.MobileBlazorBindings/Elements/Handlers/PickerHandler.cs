using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class PickerHandler : ViewHandler
    {

        public PickerHandler(NativeComponentRenderer renderer, XF.Picker pickerControl) : base(renderer, pickerControl)
        {
            PickerControl = pickerControl ?? throw new ArgumentNullException(nameof(pickerControl));
            Initialize(renderer);
        }
        void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onselecteditemchanged",
                setId: id => SelectedItemChangedEventHandlerId = id,
                clearId: id => { if (SelectedItemChangedEventHandlerId == id) { SelectedItemChangedEventHandlerId = 0; } });
            PickerControl.SelectedIndexChanged += (s, e) =>
            {
                if (SelectedItemChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SelectedItemChangedEventHandlerId, null, new ChangeEventArgs { Value = PickerControl.SelectedItem }));
                }
            };
        }
        public ulong SelectedItemChangedEventHandlerId { get; set; }

        public XF.Picker PickerControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Picker.Title):
                    PickerControl.Title = (string)attributeValue;
                    break;
                case nameof(XF.Picker.ItemsSource):
                    PickerControl.ItemsSource = AttributeHelper.DelegateToIList(attributeValue);
                    break;
                case nameof(XF.Picker.ItemDisplayBinding):
                    PickerControl.ItemDisplayBinding = new XF.Binding((string)attributeValue);
                    break;
                case nameof(XF.Picker.SelectedItem):
                    var item = AttributeHelper.DelegateToObject(attributeValue);

                    if(PickerControl.SelectedItem == null)
                    {
                        PickerControl.SelectedItem = item;
                    }
                    else
                    {
                        Debug.WriteLine("item already set");
                    }
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;

            }
        }
    }
}
