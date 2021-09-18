// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract class ItemsViewHandler : ViewHandler
    {
        public ItemsViewHandler(NativeComponentRenderer renderer, XF.ItemsView itemsViewControl) : base(renderer, itemsViewControl)
        {
            ItemsViewControl = itemsViewControl ?? throw new ArgumentNullException(nameof(itemsViewControl));

            InitializeEventHandlers(renderer);
        }
        public XF.ItemsView ItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ItemsView.HorizontalScrollBarVisibility):
                    ItemsViewControl.HorizontalScrollBarVisibility = (XF.ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.ItemsView.ItemsUpdatingScrollMode):
                    ItemsViewControl.ItemsUpdatingScrollMode = (XF.ItemsUpdatingScrollMode)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.ItemsView.RemainingItemsThreshold):
                    ItemsViewControl.RemainingItemsThreshold = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.ItemsView.VerticalScrollBarVisibility):
                    ItemsViewControl.VerticalScrollBarVisibility = (XF.ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.ItemsView.ItemsSource):
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
