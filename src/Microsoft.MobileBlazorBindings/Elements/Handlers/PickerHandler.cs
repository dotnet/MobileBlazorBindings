// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class PickerHandler : ViewHandler
    {
        public PickerHandler(NativeComponentRenderer renderer, MC.Picker pickerControl) : base(renderer, pickerControl)
        {
            PickerControl = pickerControl ?? throw new ArgumentNullException(nameof(pickerControl));
            Initialize(renderer);
        }

        private void Initialize(NativeComponentRenderer renderer)
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

            ConfigureEvent(
                eventName: "onselectedindexchanged",
                setId: id => SelectedIndexChangedEventHandlerId = id,
                clearId: id => { if (SelectedIndexChangedEventHandlerId == id) { SelectedIndexChangedEventHandlerId = 0; } });
            PickerControl.SelectedIndexChanged += (s, e) =>
            {
                if (SelectedItemChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SelectedIndexChangedEventHandlerId, null, new ChangeEventArgs { Value = PickerControl.SelectedIndex }));
                }
            };
        }
        public ulong SelectedItemChangedEventHandlerId { get; set; }
        public ulong SelectedIndexChangedEventHandlerId { get; set; }
        public MC.Picker PickerControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Picker.CharacterSpacing):
                    PickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, 0.0);
                    break;
                case nameof(MC.Picker.FontAttributes):
                    PickerControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.Picker.FontFamily):
                    PickerControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(MC.Picker.FontSize):
                    PickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(MC.Picker.HorizontalTextAlignment):
                    PickerControl.HorizontalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.Picker.ItemDisplayBinding):
                    PickerControl.ItemDisplayBinding = new MC.Binding((string)attributeValue);
                    break;
                case nameof(MC.Picker.ItemsSource):
                    var items = AttributeHelper.DelegateToObject<IList>(attributeValue);
                    // IMPORTANT! don't set ItemsSource again if it is already set
                    // Resetting ItemsSource will confuse the SelectedItem property and cause and infinite loop
                    if (PickerControl.ItemsSource != items)
                    {
                        PickerControl.ItemsSource = items;
                    }
                    break;
                case nameof(MC.Picker.TextColor):
                    PickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(MC.Picker.Title):
                    PickerControl.Title = (string)attributeValue;
                    break;
                case nameof(MC.Picker.TitleColor):
                    PickerControl.TitleColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(MC.Picker.SelectedIndex):
                    var index = AttributeHelper.GetInt(attributeValue);
                    if (PickerControl.SelectedIndex != index)
                    {
                        PickerControl.SelectedIndex = index;
                    }
                    break;
                case nameof(MC.Picker.SelectedItem):
                    var item = AttributeHelper.DelegateToObject<object>(attributeValue);
                    if (PickerControl.SelectedItem != item)
                    {
                        PickerControl.SelectedItem = item;
                    }
                    break;
                case nameof(MC.Label.VerticalTextAlignment):
                    PickerControl.VerticalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
