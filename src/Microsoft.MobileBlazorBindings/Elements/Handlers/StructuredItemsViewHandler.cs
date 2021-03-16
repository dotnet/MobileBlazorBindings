// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class StructuredItemsViewHandler : ItemsViewHandler
    {
        public StructuredItemsViewHandler(NativeComponentRenderer renderer, XF.StructuredItemsView structuredItemsViewControl) : base(renderer, structuredItemsViewControl)
        {
            StructuredItemsViewControl = structuredItemsViewControl ?? throw new ArgumentNullException(nameof(structuredItemsViewControl));
        }

        public XF.StructuredItemsView StructuredItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.StructuredItemsView.ItemSizingStrategy):
                    StructuredItemsViewControl.ItemSizingStrategy = (XF.ItemSizingStrategy)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.StructuredItemsView.ItemsLayout):
                    StructuredItemsViewControl.ItemsLayout = AttributeHelper.DelegateToObject<XF.IItemsLayout>(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
