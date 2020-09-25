// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class BaseShellItemHandler : NavigableElementHandler
    {
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
                    BaseShellItemControl.FlyoutIcon = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                case nameof(XF.BaseShellItem.Icon):
                    BaseShellItemControl.Icon = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                case nameof(XF.BaseShellItem.IsEnabled):
                    BaseShellItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.BaseShellItem.IsTabStop):
                    BaseShellItemControl.IsTabStop = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.BaseShellItem.IsVisible):
                    BaseShellItemControl.IsVisible = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.BaseShellItem.Route):
                    BaseShellItemControl.Route = (string)attributeValue;
                    break;
                case nameof(XF.BaseShellItem.TabIndex):
                    BaseShellItemControl.TabIndex = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.BaseShellItem.Title):
                    BaseShellItemControl.Title = (string)attributeValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
