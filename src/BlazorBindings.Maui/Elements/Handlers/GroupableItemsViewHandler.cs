// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class GroupableItemsViewHandler : SelectableItemsViewHandler
    {
        public GroupableItemsViewHandler(NativeComponentRenderer renderer, MC.GroupableItemsView groupableItemsViewControl) : base(renderer, groupableItemsViewControl)
        {
            GroupableItemsViewControl = groupableItemsViewControl ?? throw new ArgumentNullException(nameof(groupableItemsViewControl));
        }

        public MC.GroupableItemsView GroupableItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.GroupableItemsView.IsGrouped):
                    GroupableItemsViewControl.IsGrouped = AttributeHelper.GetBool(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
