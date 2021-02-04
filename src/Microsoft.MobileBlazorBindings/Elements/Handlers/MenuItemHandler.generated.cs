// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class MenuItemHandler : BaseMenuItemHandler
    {
        private static readonly XF.ImageSource IconImageSourceDefaultValue = XF.MenuItem.IconImageSourceProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly bool IsDestructiveDefaultValue = XF.MenuItem.IsDestructiveProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsEnabledDefaultValue = XF.MenuItem.IsEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string TextDefaultValue = XF.MenuItem.TextProperty.DefaultValue is string value ? value : default;

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
                    MenuItemControl.IconImageSource = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, IconImageSourceDefaultValue);
                    break;
                case nameof(XF.MenuItem.IsDestructive):
                    MenuItemControl.IsDestructive = AttributeHelper.GetBool(attributeValue, IsDestructiveDefaultValue);
                    break;
                case nameof(XF.MenuItem.IsEnabled):
                    MenuItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue, IsEnabledDefaultValue);
                    break;
                case nameof(XF.MenuItem.StyleClass):
                    MenuItemControl.StyleClass = AttributeHelper.GetStringList(attributeValue);
                    break;
                case nameof(XF.MenuItem.Text):
                    MenuItemControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
