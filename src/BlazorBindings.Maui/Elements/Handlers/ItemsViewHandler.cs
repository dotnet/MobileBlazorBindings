// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using BlazorBindings.Core;
using System;
using System.Collections;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract class ItemsViewHandler : ViewHandler
    {
        public ItemsViewHandler(NativeComponentRenderer renderer, MC.ItemsView itemsViewControl) : base(renderer, itemsViewControl)
        {
            ItemsViewControl = itemsViewControl ?? throw new ArgumentNullException(nameof(itemsViewControl));

            InitializeEventHandlers(renderer);
        }
        public MC.ItemsView ItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ItemsView.HorizontalScrollBarVisibility):
                    ItemsViewControl.HorizontalScrollBarVisibility = (ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.ItemsView.ItemsUpdatingScrollMode):
                    ItemsViewControl.ItemsUpdatingScrollMode = (MC.ItemsUpdatingScrollMode)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.ItemsView.RemainingItemsThreshold):
                    ItemsViewControl.RemainingItemsThreshold = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.ItemsView.VerticalScrollBarVisibility):
                    ItemsViewControl.VerticalScrollBarVisibility = (ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.ItemsView.ItemsSource):
                    ItemsViewControl.ItemsSource = AttributeHelper.DelegateToObject<IEnumerable>(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private void InitializeEventHandlers(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onremainingitemsthresholdreached",
                setId: id => RemainingItemsThresholdReachedEventHandlerId = id,
                clearId: id => { if (RemainingItemsThresholdReachedEventHandlerId == id) { RemainingItemsThresholdReachedEventHandlerId = 0; } });
            ItemsViewControl.RemainingItemsThresholdReached += (s, e) =>
            {
                if (RemainingItemsThresholdReachedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(RemainingItemsThresholdReachedEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "onscrolled",
                setId: id => ScrolledEventHandlerId = id,
                clearId: id => { if (ScrolledEventHandlerId == id) { ScrolledEventHandlerId = 0; } });
            ItemsViewControl.Scrolled += (s, e) =>
            {
                if (ScrolledEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ScrolledEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "onscrolltorequested",
                setId: id => ScrollToRequestedEventHandlerId = id,
                clearId: id => { if (ScrollToRequestedEventHandlerId == id) { ScrollToRequestedEventHandlerId = 0; } });
            ItemsViewControl.ScrollToRequested += (s, e) =>
            {
                if (ScrollToRequestedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ScrollToRequestedEventHandlerId, null, e));
                }
            };
        }

        public ulong RemainingItemsThresholdReachedEventHandlerId { get; set; }
        public ulong ScrolledEventHandlerId { get; set; }
        public ulong ScrollToRequestedEventHandlerId { get; set; }
    }
}
