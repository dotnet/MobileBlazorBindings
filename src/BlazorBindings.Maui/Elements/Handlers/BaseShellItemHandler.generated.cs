// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class BaseShellItemHandler : NavigableElementHandler
    {
        private static readonly MC.ImageSource FlyoutIconDefaultValue = MC.BaseShellItem.FlyoutIconProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly MC.ImageSource IconDefaultValue = MC.BaseShellItem.IconProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly bool IsEnabledDefaultValue = MC.BaseShellItem.IsEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsVisibleDefaultValue = MC.BaseShellItem.IsVisibleProperty.DefaultValue is bool value ? value : default;
        private static readonly string TitleDefaultValue = MC.BaseShellItem.TitleProperty.DefaultValue is string value ? value : default;

        public BaseShellItemHandler(NativeComponentRenderer renderer, MC.BaseShellItem baseShellItemControl) : base(renderer, baseShellItemControl)
        {
            BaseShellItemControl = baseShellItemControl ?? throw new ArgumentNullException(nameof(baseShellItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.BaseShellItem BaseShellItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.BaseShellItem.FlyoutIcon):
                    BaseShellItemControl.FlyoutIcon = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, FlyoutIconDefaultValue);
                    break;
                case nameof(MC.BaseShellItem.FlyoutItemIsVisible):
                    BaseShellItemControl.FlyoutItemIsVisible = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(MC.BaseShellItem.Icon):
                    BaseShellItemControl.Icon = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, IconDefaultValue);
                    break;
                case nameof(MC.BaseShellItem.IsEnabled):
                    BaseShellItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue, IsEnabledDefaultValue);
                    break;
                case nameof(MC.BaseShellItem.IsVisible):
                    BaseShellItemControl.IsVisible = AttributeHelper.GetBool(attributeValue, IsVisibleDefaultValue);
                    break;
                case nameof(MC.BaseShellItem.Route):
                    BaseShellItemControl.Route = (string)attributeValue;
                    break;
                case nameof(MC.BaseShellItem.Title):
                    BaseShellItemControl.Title = (string)attributeValue ?? TitleDefaultValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
