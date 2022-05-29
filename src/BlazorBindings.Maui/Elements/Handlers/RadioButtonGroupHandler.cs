using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class RadioButtonGroupHandler : StackLayoutHandler
    {
        private readonly string _groupId = Guid.NewGuid().ToString();
        private ulong _selectedValueChangedEventId;

        public RadioButtonGroupHandler(NativeComponentRenderer renderer, MC.StackLayout stackLayoutControl) : base(renderer, stackLayoutControl)
        {
            Initialize();
        }

        public void Initialize()
        {
            MC.RadioButtonGroup.SetGroupName(StackLayoutControl, _groupId);

            StackLayoutControl.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "SelectedValue" && _selectedValueChangedEventId != default)
                {
                    Renderer.Dispatcher.InvokeAsync(() => Renderer.DispatchEventAsync(_selectedValueChangedEventId, null, new ChangeEventArgs
                    {
                        Value = MC.RadioButtonGroup.GetSelectedValue(StackLayoutControl)
                    }));
                }
            };

            ConfigureEvent("onselectedvaluechanged",
                setId: id => _selectedValueChangedEventId = id,
                clearId: id => { if (_selectedValueChangedEventId == id) { _selectedValueChangedEventId = 0; } });
        }

        public override void SetParent(MC.Element parent)
        {
            base.SetParent(parent);
        }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(RadioButtonGroup<object>.SelectedValue):
                    var selectedValue = AttributeHelper.DelegateToObject<object>(attributeValue);
                    MC.RadioButtonGroup.SetSelectedValue(StackLayoutControl, selectedValue);
                    break;

                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
