// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class FlyoutPageHandler : PageHandler
    {
        private static readonly MC.FlyoutLayoutBehavior FlyoutLayoutBehaviorDefaultValue = MC.FlyoutPage.FlyoutLayoutBehaviorProperty.DefaultValue is MC.FlyoutLayoutBehavior value ? value : default;
        private static readonly bool IsGestureEnabledDefaultValue = MC.FlyoutPage.IsGestureEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsPresentedDefaultValue = MC.FlyoutPage.IsPresentedProperty.DefaultValue is bool value ? value : default;

        public FlyoutPageHandler(NativeComponentRenderer renderer, MC.FlyoutPage flyoutPageControl) : base(renderer, flyoutPageControl)
        {
            FlyoutPageControl = flyoutPageControl ?? throw new ArgumentNullException(nameof(flyoutPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.FlyoutPage FlyoutPageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.FlyoutPage.FlyoutLayoutBehavior):
                    FlyoutPageControl.FlyoutLayoutBehavior = (MC.FlyoutLayoutBehavior)AttributeHelper.GetInt(attributeValue, (int)FlyoutLayoutBehaviorDefaultValue);
                    break;
                case nameof(MC.FlyoutPage.IsGestureEnabled):
                    FlyoutPageControl.IsGestureEnabled = AttributeHelper.GetBool(attributeValue, IsGestureEnabledDefaultValue);
                    break;
                case nameof(MC.FlyoutPage.IsPresented):
                    FlyoutPageControl.IsPresented = AttributeHelper.GetBool(attributeValue, IsPresentedDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
