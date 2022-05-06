// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ToolbarItemHandler : MenuItemHandler
    {

        public ToolbarItemHandler(NativeComponentRenderer renderer, MC.ToolbarItem toolbarItemControl) : base(renderer, toolbarItemControl)
        {
            ToolbarItemControl = toolbarItemControl ?? throw new ArgumentNullException(nameof(toolbarItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ToolbarItem ToolbarItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ToolbarItem.Order):
                    ToolbarItemControl.Order = (MC.ToolbarItemOrder)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(MC.ToolbarItem.Priority):
                    ToolbarItemControl.Priority = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
