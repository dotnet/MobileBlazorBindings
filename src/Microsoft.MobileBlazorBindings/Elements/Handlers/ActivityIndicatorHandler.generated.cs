// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ActivityIndicatorHandler : ViewHandler
    {
        private static readonly XF.Color ColorDefaultValue = XF.ActivityIndicator.ColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly bool IsRunningDefaultValue = XF.ActivityIndicator.IsRunningProperty.DefaultValue is bool value ? value : default;

        public ActivityIndicatorHandler(NativeComponentRenderer renderer, XF.ActivityIndicator activityIndicatorControl) : base(renderer, activityIndicatorControl)
        {
            ActivityIndicatorControl = activityIndicatorControl ?? throw new ArgumentNullException(nameof(activityIndicatorControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ActivityIndicator ActivityIndicatorControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ActivityIndicator.Color):
                    ActivityIndicatorControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(XF.ActivityIndicator.IsRunning):
                    ActivityIndicatorControl.IsRunning = AttributeHelper.GetBool(attributeValue, IsRunningDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
