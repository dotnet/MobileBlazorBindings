// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class MenuItemHandler : BaseMenuItemHandler
    {
        public MenuItemHandler(NativeComponentRenderer renderer, XF.MenuItem menuItemControl) : base(renderer, menuItemControl)
        {
            MenuItemControl = menuItemControl ?? throw new ArgumentNullException(nameof(menuItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.MenuItem MenuItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (attributeEventHandlerId != 0)
            {
                ApplyEventHandlerId(attributeName, attributeEventHandlerId);
            }

            switch (attributeName)
            {
                case nameof(XF.MenuItem.IconImageSource):
                    MenuItemControl.IconImageSource = AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                case nameof(XF.MenuItem.IsDestructive):
                    MenuItemControl.IsDestructive = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.MenuItem.Text):
                    MenuItemControl.Text = (string)attributeValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId);
    }
}
