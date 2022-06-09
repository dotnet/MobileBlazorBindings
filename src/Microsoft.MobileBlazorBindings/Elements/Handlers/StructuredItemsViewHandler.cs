// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class StructuredItemsViewHandler : ItemsViewHandler
    {
        public StructuredItemsViewHandler(NativeComponentRenderer renderer, MC.StructuredItemsView structuredItemsViewControl) : base(renderer, structuredItemsViewControl)
        {
            StructuredItemsViewControl = structuredItemsViewControl ?? throw new ArgumentNullException(nameof(structuredItemsViewControl));
        }

        public MC.StructuredItemsView StructuredItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.StructuredItemsView.ItemSizingStrategy):
                    StructuredItemsViewControl.ItemSizingStrategy = (MC.ItemSizingStrategy)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.StructuredItemsView.ItemsLayout):
                    StructuredItemsViewControl.ItemsLayout = AttributeHelper.DelegateToObject<MC.IItemsLayout>(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
