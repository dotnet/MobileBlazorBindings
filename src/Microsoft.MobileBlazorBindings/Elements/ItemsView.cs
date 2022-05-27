// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class ItemsView<T> : View
    {
        static ItemsView()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<ItemsView<T>>(nameof(ItemTemplate),
                (renderer, _, component) => new DataTemplatePropertyHandler<XF.ItemsView, T>(component,
                    (itemsView, dataTemplate) => itemsView.ItemTemplate = dataTemplate));

            ElementHandlerRegistry.RegisterPropertyContentHandler<ItemsView<T>>(nameof(EmptyView),
                renderer => new ContentPropertyHandler<XF.ItemsView>(
                    (itemsView, valueElement) => itemsView.EmptyView = valueElement));
        }

        [Parameter] public XF.ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public RenderFragment<T> ItemTemplate { get; set; }
        [Parameter] public RenderFragment EmptyView { get; set; }
        [Parameter] public IEnumerable<T> ItemsSource { get; set; }
        [Parameter] public XF.ItemsUpdatingScrollMode? ItemsUpdatingScrollMode { get; set; }
        [Parameter] public int? RemainingItemsThreshold { get; set; }
        [Parameter] public XF.ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        [Parameter] public EventCallback OnRemainingItemsThresholdReached { get; set; }
        [Parameter] public EventCallback<XF.ItemsViewScrolledEventArgs> OnScrolled { get; set; }
        [Parameter] public EventCallback<XF.ScrollToRequestEventArgs> OnScrollToRequested { get; set; }

        public new XF.ItemsView NativeControl => ((ItemsViewHandler)ElementHandler).ItemsViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(HorizontalScrollBarVisibility), (int)HorizontalScrollBarVisibility.Value);
            }
            if (ItemsSource != null)
            {
                builder.AddAttribute(nameof(ItemsSource), AttributeHelper.ObjectToDelegate(ItemsSource));
            }
            if (ItemsUpdatingScrollMode != null)
            {
                builder.AddAttribute(nameof(ItemsUpdatingScrollMode), (int)ItemsUpdatingScrollMode.Value);
            }
            if (RemainingItemsThreshold != null)
            {
                builder.AddAttribute(nameof(RemainingItemsThreshold), RemainingItemsThreshold.Value);
            }
            if (VerticalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(VerticalScrollBarVisibility), (int)VerticalScrollBarVisibility.Value);
            }
            builder.AddAttribute("onremainingitemsthresholdreached", OnRemainingItemsThresholdReached);
            builder.AddAttribute("onscrolled", OnScrolled);
            builder.AddAttribute("onscrolltorequested", OnScrollToRequested);
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(ItemsView<T>), nameof(ItemTemplate), ItemTemplate);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ItemsView<T>), nameof(EmptyView), EmptyView);
        }
    }
}
