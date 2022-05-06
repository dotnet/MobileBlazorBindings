// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellHandler : PageHandler
    {
        private static readonly Color FlyoutBackgroundColorDefaultValue = MC.Shell.FlyoutBackgroundColorProperty.DefaultValue is Color value ? value : default;
        private static readonly MC.ImageSource FlyoutBackgroundImageDefaultValue = MC.Shell.FlyoutBackgroundImageProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly Aspect FlyoutBackgroundImageAspectDefaultValue = MC.Shell.FlyoutBackgroundImageAspectProperty.DefaultValue is Aspect value ? value : default;
        private static readonly FlyoutBehavior FlyoutBehaviorDefaultValue = MC.Shell.FlyoutBehaviorProperty.DefaultValue is FlyoutBehavior value ? value : default;
        private static readonly MC.FlyoutHeaderBehavior FlyoutHeaderBehaviorDefaultValue = MC.Shell.FlyoutHeaderBehaviorProperty.DefaultValue is MC.FlyoutHeaderBehavior value ? value : default;
        private static readonly double FlyoutHeightDefaultValue = MC.Shell.FlyoutHeightProperty.DefaultValue is double value ? value : default;
        private static readonly MC.ImageSource FlyoutIconDefaultValue = MC.Shell.FlyoutIconProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly bool FlyoutIsPresentedDefaultValue = MC.Shell.FlyoutIsPresentedProperty.DefaultValue is bool value ? value : default;
        private static readonly MC.ScrollMode FlyoutVerticalScrollModeDefaultValue = MC.Shell.FlyoutVerticalScrollModeProperty.DefaultValue is MC.ScrollMode value ? value : default;
        private static readonly double FlyoutWidthDefaultValue = MC.Shell.FlyoutWidthProperty.DefaultValue is double value ? value : default;

        public ShellHandler(NativeComponentRenderer renderer, MC.Shell shellControl) : base(renderer, shellControl)
        {
            ShellControl = shellControl ?? throw new ArgumentNullException(nameof(shellControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Shell ShellControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Shell.FlyoutBackgroundColor):
                    ShellControl.FlyoutBackgroundColor = AttributeHelper.StringToColor((string)attributeValue, FlyoutBackgroundColorDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutBackgroundImage):
                    ShellControl.FlyoutBackgroundImage = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, FlyoutBackgroundImageDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutBackgroundImageAspect):
                    ShellControl.FlyoutBackgroundImageAspect = (Aspect)AttributeHelper.GetInt(attributeValue, (int)FlyoutBackgroundImageAspectDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutBehavior):
                    ShellControl.FlyoutBehavior = (FlyoutBehavior)AttributeHelper.GetInt(attributeValue, (int)FlyoutBehaviorDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutHeaderBehavior):
                    ShellControl.FlyoutHeaderBehavior = (MC.FlyoutHeaderBehavior)AttributeHelper.GetInt(attributeValue, (int)FlyoutHeaderBehaviorDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutHeight):
                    ShellControl.FlyoutHeight = AttributeHelper.StringToDouble((string)attributeValue, FlyoutHeightDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutIcon):
                    ShellControl.FlyoutIcon = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, FlyoutIconDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutIsPresented):
                    ShellControl.FlyoutIsPresented = AttributeHelper.GetBool(attributeValue, FlyoutIsPresentedDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutVerticalScrollMode):
                    ShellControl.FlyoutVerticalScrollMode = (MC.ScrollMode)AttributeHelper.GetInt(attributeValue, (int)FlyoutVerticalScrollModeDefaultValue);
                    break;
                case nameof(MC.Shell.FlyoutWidth):
                    ShellControl.FlyoutWidth = AttributeHelper.StringToDouble((string)attributeValue, FlyoutWidthDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
