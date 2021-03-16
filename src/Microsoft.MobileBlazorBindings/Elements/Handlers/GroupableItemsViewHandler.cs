// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GroupableItemsViewHandler : SelectableItemsViewHandler
    {
        public GroupableItemsViewHandler(NativeComponentRenderer renderer, XF.GroupableItemsView groupableItemsViewControl) : base(renderer, groupableItemsViewControl)
        {
            GroupableItemsViewControl = groupableItemsViewControl ?? throw new ArgumentNullException(nameof(groupableItemsViewControl));
        }

        public XF.GroupableItemsView GroupableItemsViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.GroupableItemsView.IsGrouped):
                    GroupableItemsViewControl.IsGrouped = AttributeHelper.GetBool(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
