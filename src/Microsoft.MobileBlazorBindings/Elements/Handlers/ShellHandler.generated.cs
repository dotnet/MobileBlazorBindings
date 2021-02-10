// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellHandler : PageHandler
    {
        private static readonly XF.Color FlyoutBackgroundColorDefaultValue = XF.Shell.FlyoutBackgroundColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.ImageSource FlyoutBackgroundImageDefaultValue = XF.Shell.FlyoutBackgroundImageProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly XF.Aspect FlyoutBackgroundImageAspectDefaultValue = XF.Shell.FlyoutBackgroundImageAspectProperty.DefaultValue is XF.Aspect value ? value : default;
        private static readonly XF.FlyoutBehavior FlyoutBehaviorDefaultValue = XF.Shell.FlyoutBehaviorProperty.DefaultValue is XF.FlyoutBehavior value ? value : default;
        private static readonly XF.FlyoutHeaderBehavior FlyoutHeaderBehaviorDefaultValue = XF.Shell.FlyoutHeaderBehaviorProperty.DefaultValue is XF.FlyoutHeaderBehavior value ? value : default;
        private static readonly double FlyoutHeightDefaultValue = XF.Shell.FlyoutHeightProperty.DefaultValue is double value ? value : default;
        private static readonly XF.ImageSource FlyoutIconDefaultValue = XF.Shell.FlyoutIconProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly bool FlyoutIsPresentedDefaultValue = XF.Shell.FlyoutIsPresentedProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.ScrollMode FlyoutVerticalScrollModeDefaultValue = XF.Shell.FlyoutVerticalScrollModeProperty.DefaultValue is XF.ScrollMode value ? value : default;
        private static readonly double FlyoutWidthDefaultValue = XF.Shell.FlyoutWidthProperty.DefaultValue is double value ? value : default;

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
                    ShellControl.FlyoutBackgroundColor = AttributeHelper.StringToColor((string)attributeValue, FlyoutBackgroundColorDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundImage):
                    ShellControl.FlyoutBackgroundImage = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, FlyoutBackgroundImageDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutBackgroundImageAspect):
                    ShellControl.FlyoutBackgroundImageAspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue, (int)FlyoutBackgroundImageAspectDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutBehavior):
                    ShellControl.FlyoutBehavior = (XF.FlyoutBehavior)AttributeHelper.GetInt(attributeValue, (int)FlyoutBehaviorDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutHeaderBehavior):
                    ShellControl.FlyoutHeaderBehavior = (XF.FlyoutHeaderBehavior)AttributeHelper.GetInt(attributeValue, (int)FlyoutHeaderBehaviorDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutHeight):
                    ShellControl.FlyoutHeight = AttributeHelper.StringToDouble((string)attributeValue, FlyoutHeightDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutIcon):
                    ShellControl.FlyoutIcon = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, FlyoutIconDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutIsPresented):
                    ShellControl.FlyoutIsPresented = AttributeHelper.GetBool(attributeValue, FlyoutIsPresentedDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutVerticalScrollMode):
                    ShellControl.FlyoutVerticalScrollMode = (XF.ScrollMode)AttributeHelper.GetInt(attributeValue, (int)FlyoutVerticalScrollModeDefaultValue);
                    break;
                case nameof(XF.Shell.FlyoutWidth):
                    ShellControl.FlyoutWidth = AttributeHelper.StringToDouble((string)attributeValue, FlyoutWidthDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
