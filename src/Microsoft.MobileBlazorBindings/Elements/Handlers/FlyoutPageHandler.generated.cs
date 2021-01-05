// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FlyoutPageHandler : PageHandler
    {
        public FlyoutPageHandler(NativeComponentRenderer renderer, XF.FlyoutPage flyoutPageControl) : base(renderer, flyoutPageControl)
        {
            FlyoutPageControl = flyoutPageControl ?? throw new ArgumentNullException(nameof(flyoutPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.FlyoutPage FlyoutPageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.FlyoutPage.FlyoutLayoutBehavior):
                    FlyoutPageControl.FlyoutLayoutBehavior = (XF.FlyoutLayoutBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.FlyoutPage.IsGestureEnabled):
                    FlyoutPageControl.IsGestureEnabled = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.FlyoutPage.IsPresented):
                    FlyoutPageControl.IsPresented = AttributeHelper.GetBool(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
