// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class LinearGradientBrushHandler : GradientBrushHandler
    {
        private static readonly Point EndPointDefaultValue = MC.LinearGradientBrush.EndPointProperty.DefaultValue is Point value ? value : default;
        private static readonly Point StartPointDefaultValue = MC.LinearGradientBrush.StartPointProperty.DefaultValue is Point value ? value : default;

        public LinearGradientBrushHandler(NativeComponentRenderer renderer, MC.LinearGradientBrush linearGradientBrushControl) : base(renderer, linearGradientBrushControl)
        {
            LinearGradientBrushControl = linearGradientBrushControl ?? throw new ArgumentNullException(nameof(linearGradientBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.LinearGradientBrush LinearGradientBrushControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.LinearGradientBrush.EndPoint):
                    LinearGradientBrushControl.EndPoint = AttributeHelper.StringToPoint(attributeValue, EndPointDefaultValue);
                    break;
                case nameof(MC.LinearGradientBrush.StartPoint):
                    LinearGradientBrushControl.StartPoint = AttributeHelper.StringToPoint(attributeValue, StartPointDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
