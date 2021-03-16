// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ToolbarItemHandler : MenuItemHandler
    {

        public ToolbarItemHandler(NativeComponentRenderer renderer, XF.ToolbarItem toolbarItemControl) : base(renderer, toolbarItemControl)
        {
            ToolbarItemControl = toolbarItemControl ?? throw new ArgumentNullException(nameof(toolbarItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ToolbarItem ToolbarItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ToolbarItem.Order):
                    ToolbarItemControl.Order = (XF.ToolbarItemOrder)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.ToolbarItem.Priority):
                    ToolbarItemControl.Priority = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
