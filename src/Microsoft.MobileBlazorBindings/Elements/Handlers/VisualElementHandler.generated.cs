// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class VisualElementHandler : NavigableElementHandler
    {
        public VisualElementHandler(NativeComponentRenderer renderer, XF.VisualElement visualElementControl) : base(renderer, visualElementControl)
        {
            VisualElementControl = visualElementControl ?? throw new ArgumentNullException(nameof(visualElementControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.VisualElement VisualElementControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.VisualElement.AnchorX):
                    VisualElementControl.AnchorX = AttributeHelper.StringToDouble((string)attributeValue, 0.50);
                    break;
                case nameof(XF.VisualElement.AnchorY):
                    VisualElementControl.AnchorY = AttributeHelper.StringToDouble((string)attributeValue, 0.50);
                    break;
                case nameof(XF.VisualElement.BackgroundColor):
                    VisualElementControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.FlowDirection):
                    VisualElementControl.FlowDirection = (XF.FlowDirection)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.VisualElement.HeightRequest):
                    VisualElementControl.HeightRequest = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.VisualElement.InputTransparent):
                    VisualElementControl.InputTransparent = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.VisualElement.IsEnabled):
                    VisualElementControl.IsEnabled = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.VisualElement.IsTabStop):
                    VisualElementControl.IsTabStop = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.VisualElement.IsVisible):
                    VisualElementControl.IsVisible = AttributeHelper.GetBool(attributeValue, true);
                    break;
                case nameof(XF.VisualElement.MinimumHeightRequest):
                    VisualElementControl.MinimumHeightRequest = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.VisualElement.MinimumWidthRequest):
                    VisualElementControl.MinimumWidthRequest = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.VisualElement.Opacity):
                    VisualElementControl.Opacity = AttributeHelper.StringToDouble((string)attributeValue, 1.00);
                    break;
                case nameof(XF.VisualElement.Rotation):
                    VisualElementControl.Rotation = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.RotationX):
                    VisualElementControl.RotationX = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.RotationY):
                    VisualElementControl.RotationY = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.Scale):
                    VisualElementControl.Scale = AttributeHelper.StringToDouble((string)attributeValue, 1.00);
                    break;
                case nameof(XF.VisualElement.ScaleX):
                    VisualElementControl.ScaleX = AttributeHelper.StringToDouble((string)attributeValue, 1.00);
                    break;
                case nameof(XF.VisualElement.ScaleY):
                    VisualElementControl.ScaleY = AttributeHelper.StringToDouble((string)attributeValue, 1.00);
                    break;
                case nameof(XF.VisualElement.TabIndex):
                    VisualElementControl.TabIndex = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.VisualElement.TranslationX):
                    VisualElementControl.TranslationX = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.TranslationY):
                    VisualElementControl.TranslationY = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.VisualElement.WidthRequest):
                    VisualElementControl.WidthRequest = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
