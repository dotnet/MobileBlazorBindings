// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class SelectableItemsViewHandler : StructuredItemsViewHandler
    {
        public SelectableItemsViewHandler(NativeComponentRenderer renderer, MC.SelectableItemsView selectableItemsViewControl) : base(renderer, selectableItemsViewControl)
        {
            SelectableItemsViewControl = selectableItemsViewControl ?? throw new ArgumentNullException(nameof(selectableItemsViewControl));
            InitializeEventHandlers(renderer);
        }

        public MC.SelectableItemsView SelectableItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.SelectableItemsView.SelectedItem):
                    SelectableItemsViewControl.SelectedItem = AttributeHelper.DelegateToObject<object>(attributeValue);
                    break;
                case nameof(MC.SelectableItemsView.SelectedItems):
                    // Don't assign value if lists content is equal. Otherwise leads to infinite loop when binded. 
                    var listValue = AttributeHelper.DelegateToObject<IList<object>>(attributeValue);
                    if (!Enumerable.SequenceEqual(listValue, SelectableItemsViewControl.SelectedItems))
                    {
                        SelectableItemsViewControl.SelectedItems = listValue;
                    }
                    break;
                case nameof(MC.SelectableItemsView.SelectionMode):
                    SelectableItemsViewControl.SelectionMode = (MC.SelectionMode)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private void InitializeEventHandlers(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onselectionchanged",
                setId: id => SelectionChangedEventHandlerId = id,
                clearId: id => { if (SelectionChangedEventHandlerId == id) { SelectionChangedEventHandlerId = 0; } });

            ConfigureEvent(
                eventName: "onselecteditemchanged",
                setId: id => SelectedItemChangedEventHandlerId = id,
                clearId: id => { if (SelectedItemChangedEventHandlerId == id) { SelectedItemChangedEventHandlerId = 0; } });

            ConfigureEvent(
                eventName: "onselecteditemschanged",
                setId: id => SelectedItemsChangedEventHandlerId = id,
                clearId: id => { if (SelectedItemsChangedEventHandlerId == id) { SelectedItemsChangedEventHandlerId = 0; } });

            SelectableItemsViewControl.SelectionChanged += (s, e) =>
            {
                if (SelectionChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SelectionChangedEventHandlerId, null, e));
                }

                if (SelectedItemChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SelectedItemChangedEventHandlerId, null,
                        new ChangeEventArgs { Value = SelectableItemsViewControl.SelectedItem }));
                }

                if (SelectedItemsChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SelectedItemsChangedEventHandlerId, null,
                        new ChangeEventArgs { Value = SelectableItemsViewControl.SelectedItems }));
                }
            };
        }

        public ulong SelectionChangedEventHandlerId { get; set; }
        public ulong SelectedItemChangedEventHandlerId { get; set; }
        public ulong SelectedItemsChangedEventHandlerId { get; set; }
    }
}
