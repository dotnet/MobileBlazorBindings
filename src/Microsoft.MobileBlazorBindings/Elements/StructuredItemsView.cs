// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class StructuredItemsView<T> : ItemsView<T>
    {
        static StructuredItemsView()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<StructuredItemsView<T>>(nameof(Header),
                _ => new ContentPropertyHandler<XF.StructuredItemsView>((itemsView, valueElement) => itemsView.Header = valueElement));

            ElementHandlerRegistry.RegisterPropertyContentHandler<StructuredItemsView<T>>(nameof(Footer),
                _ => new ContentPropertyHandler<XF.StructuredItemsView>((itemsView, valueElement) => itemsView.Footer = valueElement));
        }

        [Parameter] public XF.ItemSizingStrategy? ItemSizingStrategy { get; set; }
        [Parameter] public XF.IItemsLayout ItemsLayout { get; set; }
        [Parameter] public RenderFragment Header { get; set; }
        [Parameter] public RenderFragment Footer { get; set; }

        public new XF.StructuredItemsView NativeControl => ((StructuredItemsViewHandler)ElementHandler).StructuredItemsViewControl;

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
