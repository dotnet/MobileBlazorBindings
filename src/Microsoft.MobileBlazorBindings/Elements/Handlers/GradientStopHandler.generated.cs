// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GradientStopHandler : ElementHandler
    {
        private static readonly XF.Color ColorDefaultValue = XF.GradientStop.ColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly float OffsetDefaultValue = XF.GradientStop.OffsetProperty.DefaultValue is float value ? value : default;

        public GradientStopHandler(NativeComponentRenderer renderer, XF.GradientStop gradientStopControl) : base(renderer, gradientStopControl)
        {
            GradientStopControl = gradientStopControl ?? throw new ArgumentNullException(nameof(gradientStopControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.GradientStop GradientStopControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.GradientStop.Color):
                    GradientStopControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(XF.GradientStop.Offset):
                    GradientStopControl.Offset = AttributeHelper.StringToSingle((string)attributeValue, OffsetDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
