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
            switch (attributeName)
            {
                case nameof(XF.MenuItem.@class):
                    MenuItemControl.@class = AttributeHelper.GetStringList(attributeValue);
                    break;
                case nameof(XF.MenuItem.IconImageSource):
                    MenuItemControl.IconImageSource = AttributeHelper.StringToImageSource(attributeValue);
                    break;
                case nameof(XF.MenuItem.IsDestructive):
                    MenuItemControl.IsDestructive = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.MenuItem.IsEnabled):
                    MenuItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.MenuItem.StyleClass):
                    MenuItemControl.StyleClass = AttributeHelper.GetStringList(attributeValue);
                    break;
                case nameof(XF.MenuItem.Text):
                    MenuItemControl.Text = (string)attributeValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
