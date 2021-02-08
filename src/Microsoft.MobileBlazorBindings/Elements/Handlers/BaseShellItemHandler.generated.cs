// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class BaseShellItemHandler : NavigableElementHandler
    {
        private static readonly XF.ImageSource FlyoutIconDefaultValue = XF.BaseShellItem.FlyoutIconProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly XF.ImageSource IconDefaultValue = XF.BaseShellItem.IconProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly bool IsEnabledDefaultValue = XF.BaseShellItem.IsEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsTabStopDefaultValue = XF.BaseShellItem.IsTabStopProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsVisibleDefaultValue = XF.BaseShellItem.IsVisibleProperty.DefaultValue is bool value ? value : default;
        private static readonly int TabIndexDefaultValue = XF.BaseShellItem.TabIndexProperty.DefaultValue is int value ? value : default;
        private static readonly string TitleDefaultValue = XF.BaseShellItem.TitleProperty.DefaultValue is string value ? value : default;

        public BaseShellItemHandler(NativeComponentRenderer renderer, XF.BaseShellItem baseShellItemControl) : base(renderer, baseShellItemControl)
        {
            BaseShellItemControl = baseShellItemControl ?? throw new ArgumentNullException(nameof(baseShellItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.BaseShellItem BaseShellItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.BaseShellItem.FlyoutIcon):
                    BaseShellItemControl.FlyoutIcon = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, FlyoutIconDefaultValue);
                    break;
                case nameof(XF.BaseShellItem.FlyoutItemIsVisible):
                    BaseShellItemControl.FlyoutItemIsVisible = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.BaseShellItem.Icon):
                    BaseShellItemControl.Icon = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, IconDefaultValue);
                    break;
                case nameof(XF.BaseShellItem.IsEnabled):
                    BaseShellItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue, IsEnabledDefaultValue);
                    break;
                case nameof(XF.BaseShellItem.IsTabStop):
                    BaseShellItemControl.IsTabStop = AttributeHelper.GetBool(attributeValue, IsTabStopDefaultValue);
                    break;
                case nameof(XF.BaseShellItem.IsVisible):
                    BaseShellItemControl.IsVisible = AttributeHelper.GetBool(attributeValue, IsVisibleDefaultValue);
                    break;
                case nameof(XF.BaseShellItem.Route):
                    BaseShellItemControl.Route = (string)attributeValue;
                    break;
                case nameof(XF.BaseShellItem.TabIndex):
                    BaseShellItemControl.TabIndex = AttributeHelper.GetInt(attributeValue, TabIndexDefaultValue);
                    break;
                case nameof(XF.BaseShellItem.Title):
                    BaseShellItemControl.Title = (string)attributeValue ?? TitleDefaultValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
