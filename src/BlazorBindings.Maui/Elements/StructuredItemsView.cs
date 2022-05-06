// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class StructuredItemsView<T> : ItemsView<T>
    {
        static StructuredItemsView()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<StructuredItemsView<T>>(nameof(Header),
                _ => new ContentPropertyHandler<MC.StructuredItemsView>((itemsView, valueElement) => itemsView.Header = valueElement));

            ElementHandlerRegistry.RegisterPropertyContentHandler<StructuredItemsView<T>>(nameof(Footer),
                _ => new ContentPropertyHandler<MC.StructuredItemsView>((itemsView, valueElement) => itemsView.Footer = valueElement));
        }

        [Parameter] public MC.ItemSizingStrategy? ItemSizingStrategy { get; set; }
        [Parameter] public MC.IItemsLayout ItemsLayout { get; set; }
        [Parameter] public RenderFragment Header { get; set; }
        [Parameter] public RenderFragment Footer { get; set; }

        public new MC.StructuredItemsView NativeControl => ((StructuredItemsViewHandler)ElementHandler).StructuredItemsViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (ItemSizingStrategy != null)
            {
                builder.AddAttribute(nameof(ItemSizingStrategy), (int)ItemSizingStrategy.Value);
            }
            if (ItemsLayout != null)
            {
                builder.AddAttribute(nameof(ItemsLayout), AttributeHelper.ObjectToDelegate(ItemsLayout));
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);

            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(StructuredItemsView<T>), nameof(Header), Header);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(StructuredItemsView<T>), nameof(Footer), Footer);
        }
    }
}
