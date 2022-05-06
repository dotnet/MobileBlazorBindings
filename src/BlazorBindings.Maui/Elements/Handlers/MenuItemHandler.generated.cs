// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class MenuItemHandler : BaseMenuItemHandler
    {
        private static readonly MC.ImageSource IconImageSourceDefaultValue = MC.MenuItem.IconImageSourceProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly bool IsDestructiveDefaultValue = MC.MenuItem.IsDestructiveProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsEnabledDefaultValue = MC.MenuItem.IsEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string TextDefaultValue = MC.MenuItem.TextProperty.DefaultValue is string value ? value : default;

        public MenuItemHandler(NativeComponentRenderer renderer, MC.MenuItem menuItemControl) : base(renderer, menuItemControl)
        {
            MenuItemControl = menuItemControl ?? throw new ArgumentNullException(nameof(menuItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.MenuItem MenuItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.MenuItem.@class):
                    MenuItemControl.@class = AttributeHelper.GetStringList(attributeValue);
                    break;
                case nameof(MC.MenuItem.IconImageSource):
                    MenuItemControl.IconImageSource = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, IconImageSourceDefaultValue);
                    break;
                case nameof(MC.MenuItem.IsDestructive):
                    MenuItemControl.IsDestructive = AttributeHelper.GetBool(attributeValue, IsDestructiveDefaultValue);
                    break;
                case nameof(MC.MenuItem.IsEnabled):
                    MenuItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue, IsEnabledDefaultValue);
                    break;
                case nameof(MC.MenuItem.StyleClass):
                    MenuItemControl.StyleClass = AttributeHelper.GetStringList(attributeValue);
                    break;
                case nameof(MC.MenuItem.Text):
                    MenuItemControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
