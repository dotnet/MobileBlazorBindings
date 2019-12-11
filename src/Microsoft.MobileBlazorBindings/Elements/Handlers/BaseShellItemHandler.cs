// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class BaseShellItemHandler : NavigableElementHandler
    {
        public BaseShellItemHandler(NativeComponentRenderer renderer, XF.BaseShellItem baseShellItemControl) : base(renderer, baseShellItemControl)
        {
            BaseShellItemControl = baseShellItemControl ?? throw new ArgumentNullException(nameof(baseShellItemControl));

            BaseShellItemControl.Appearing += (s, e) =>
            {
                if (AppearingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(AppearingEventHandlerId, null, e));
                }
            };
            BaseShellItemControl.Disappearing += (s, e) =>
            {
                if (DisappearingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(DisappearingEventHandlerId, null, e));
                }
            };
        }

        public XF.BaseShellItem BaseShellItemControl { get; }
        public ulong AppearingEventHandlerId { get; set; }
        public ulong DisappearingEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.BaseShellItem.FlyoutIcon):
                    BaseShellItemControl.FlyoutIcon = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                case nameof(XF.BaseShellItem.Icon):
                    BaseShellItemControl.Icon = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                case nameof(XF.BaseShellItem.IsEnabled):
                    BaseShellItemControl.IsEnabled = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.BaseShellItem.IsTabStop):
                    BaseShellItemControl.IsTabStop = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.BaseShellItem.Route):
                    BaseShellItemControl.Route = (string)attributeValue;
                    break;
                case nameof(XF.BaseShellItem.Title):
                    BaseShellItemControl.Title = (string)attributeValue;
                    break;
                case nameof(XF.BaseShellItem.TabIndex):
                    BaseShellItemControl.TabIndex = AttributeHelper.GetInt(attributeValue);
                    break;

                case "onappearing":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => AppearingEventHandlerId = 0);
                    AppearingEventHandlerId = attributeEventHandlerId;
                    break;
                case "ondisappearing":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => DisappearingEventHandlerId = 0);
                    DisappearingEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
