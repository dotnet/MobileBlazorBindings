// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class LinearGradientBrushHandler : GradientBrushHandler
    {
        private static readonly XF.Point EndPointDefaultValue = XF.LinearGradientBrush.EndPointProperty.DefaultValue is XF.Point value ? value : default;
        private static readonly XF.Point StartPointDefaultValue = XF.LinearGradientBrush.StartPointProperty.DefaultValue is XF.Point value ? value : default;

        public LinearGradientBrushHandler(NativeComponentRenderer renderer, XF.LinearGradientBrush linearGradientBrushControl) : base(renderer, linearGradientBrushControl)
        {
            LinearGradientBrushControl = linearGradientBrushControl ?? throw new ArgumentNullException(nameof(linearGradientBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.LinearGradientBrush LinearGradientBrushControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.LinearGradientBrush.EndPoint):
                    LinearGradientBrushControl.EndPoint = AttributeHelper.StringToPoint(attributeValue, EndPointDefaultValue);
                    break;
                case nameof(XF.LinearGradientBrush.StartPoint):
                    LinearGradientBrushControl.StartPoint = AttributeHelper.StringToPoint(attributeValue, StartPointDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
