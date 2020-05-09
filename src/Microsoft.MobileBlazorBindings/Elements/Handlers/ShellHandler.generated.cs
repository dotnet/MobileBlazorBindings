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
            switch (attributeName)
            {
                case nameof(XF.Shell.FlyoutBackgroundColor):
                    ShellControl.FlyoutBackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundImage):
                    ShellControl.FlyoutBackgroundImage = AttributeHelper.StringToImageSource(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundImageAspect):
                    ShellControl.FlyoutBackgroundImageAspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutBehavior):
                    ShellControl.FlyoutBehavior = (XF.FlyoutBehavior)AttributeHelper.GetInt(attributeValue, (int)XF.FlyoutBehavior.Flyout);
                    break;
                case nameof(XF.Shell.FlyoutHeaderBehavior):
                    ShellControl.FlyoutHeaderBehavior = (XF.FlyoutHeaderBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutIcon):
                    ShellControl.FlyoutIcon = AttributeHelper.StringToImageSource(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutIsPresented):
                    ShellControl.FlyoutIsPresented = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Shell.FlyoutVerticalScrollMode):
                    ShellControl.FlyoutVerticalScrollMode = (XF.ScrollMode)AttributeHelper.GetInt(attributeValue, (int)XF.ScrollMode.Auto);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
