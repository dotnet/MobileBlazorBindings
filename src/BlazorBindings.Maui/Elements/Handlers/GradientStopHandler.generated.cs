// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class GradientStopHandler : ElementHandler
    {
        private static readonly Color ColorDefaultValue = MC.GradientStop.ColorProperty.DefaultValue is Color value ? value : default;
        private static readonly float OffsetDefaultValue = MC.GradientStop.OffsetProperty.DefaultValue is float value ? value : default;

        public GradientStopHandler(NativeComponentRenderer renderer, MC.GradientStop gradientStopControl) : base(renderer, gradientStopControl)
        {
            GradientStopControl = gradientStopControl ?? throw new ArgumentNullException(nameof(gradientStopControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.GradientStop GradientStopControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.GradientStop.Color):
                    GradientStopControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(MC.GradientStop.Offset):
                    GradientStopControl.Offset = AttributeHelper.StringToSingle((string)attributeValue, OffsetDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
