// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ActivityIndicatorHandler : ViewHandler
    {
        private static readonly Color ColorDefaultValue = MC.ActivityIndicator.ColorProperty.DefaultValue is Color value ? value : default;
        private static readonly bool IsRunningDefaultValue = MC.ActivityIndicator.IsRunningProperty.DefaultValue is bool value ? value : default;

        public ActivityIndicatorHandler(NativeComponentRenderer renderer, MC.ActivityIndicator activityIndicatorControl) : base(renderer, activityIndicatorControl)
        {
            ActivityIndicatorControl = activityIndicatorControl ?? throw new ArgumentNullException(nameof(activityIndicatorControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ActivityIndicator ActivityIndicatorControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ActivityIndicator.Color):
                    ActivityIndicatorControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(MC.ActivityIndicator.IsRunning):
                    ActivityIndicatorControl.IsRunning = AttributeHelper.GetBool(attributeValue, IsRunningDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
