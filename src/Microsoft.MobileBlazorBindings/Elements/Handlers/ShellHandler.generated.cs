// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellHandler : PageHandler
    {
        public ShellHandler(NativeComponentRenderer renderer, XF.Shell shellControl) : base(renderer, shellControl)
        {
            ShellControl = shellControl ?? throw new ArgumentNullException(nameof(shellControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Shell ShellControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (attributeEventHandlerId != 0)
            {
                ApplyEventHandlerId(attributeName, attributeEventHandlerId);
            }

            switch (attributeName)
            {
                case nameof(XF.Shell.FlyoutBackgroundImage):
                    ShellControl.FlyoutBackgroundImage = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundImageAspect):
                    ShellControl.FlyoutBackgroundImageAspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundColor):
                    ShellControl.FlyoutBackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBehavior):
                    ShellControl.FlyoutBehavior = (XF.FlyoutBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutHeaderBehavior):
                    ShellControl.FlyoutHeaderBehavior = (XF.FlyoutHeaderBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutIcon):
                    ShellControl.FlyoutIcon = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId);
    }
}
