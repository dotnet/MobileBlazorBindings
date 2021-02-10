// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class VisualElementHandler : NavigableElementHandler
    {
        private static readonly double AnchorXDefaultValue = XF.VisualElement.AnchorXProperty.DefaultValue is double value ? value : default;
        private static readonly double AnchorYDefaultValue = XF.VisualElement.AnchorYProperty.DefaultValue is double value ? value : default;
        private static readonly XF.Color BackgroundColorDefaultValue = XF.VisualElement.BackgroundColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.FlowDirection FlowDirectionDefaultValue = XF.VisualElement.FlowDirectionProperty.DefaultValue is XF.FlowDirection value ? value : default;
        private static readonly double HeightRequestDefaultValue = XF.VisualElement.HeightRequestProperty.DefaultValue is double value ? value : default;
        private static readonly bool InputTransparentDefaultValue = XF.VisualElement.InputTransparentProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsEnabledDefaultValue = XF.VisualElement.IsEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsTabStopDefaultValue = XF.VisualElement.IsTabStopProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsVisibleDefaultValue = XF.VisualElement.IsVisibleProperty.DefaultValue is bool value ? value : default;
        private static readonly double MinimumHeightRequestDefaultValue = XF.VisualElement.MinimumHeightRequestProperty.DefaultValue is double value ? value : default;
        private static readonly double MinimumWidthRequestDefaultValue = XF.VisualElement.MinimumWidthRequestProperty.DefaultValue is double value ? value : default;
        private static readonly double OpacityDefaultValue = XF.VisualElement.OpacityProperty.DefaultValue is double value ? value : default;
        private static readonly double RotationDefaultValue = XF.VisualElement.RotationProperty.DefaultValue is double value ? value : default;
        private static readonly double RotationXDefaultValue = XF.VisualElement.RotationXProperty.DefaultValue is double value ? value : default;
        private static readonly double RotationYDefaultValue = XF.VisualElement.RotationYProperty.DefaultValue is double value ? value : default;
        private static readonly double ScaleDefaultValue = XF.VisualElement.ScaleProperty.DefaultValue is double value ? value : default;
        private static readonly double ScaleXDefaultValue = XF.VisualElement.ScaleXProperty.DefaultValue is double value ? value : default;
        private static readonly double ScaleYDefaultValue = XF.VisualElement.ScaleYProperty.DefaultValue is double value ? value : default;
        private static readonly int TabIndexDefaultValue = XF.VisualElement.TabIndexProperty.DefaultValue is int value ? value : default;
        private static readonly double TranslationXDefaultValue = XF.VisualElement.TranslationXProperty.DefaultValue is double value ? value : default;
        private static readonly double TranslationYDefaultValue = XF.VisualElement.TranslationYProperty.DefaultValue is double value ? value : default;
        private static readonly double WidthRequestDefaultValue = XF.VisualElement.WidthRequestProperty.DefaultValue is double value ? value : default;

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
                    VisualElementControl.AnchorX = AttributeHelper.StringToDouble((string)attributeValue, AnchorXDefaultValue);
                    break;
                case nameof(XF.VisualElement.AnchorY):
                    VisualElementControl.AnchorY = AttributeHelper.StringToDouble((string)attributeValue, AnchorYDefaultValue);
                    break;
                case nameof(XF.VisualElement.BackgroundColor):
                    VisualElementControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue, BackgroundColorDefaultValue);
                    break;
                case nameof(XF.VisualElement.FlowDirection):
                    VisualElementControl.FlowDirection = (XF.FlowDirection)AttributeHelper.GetInt(attributeValue, (int)FlowDirectionDefaultValue);
                    break;
                case nameof(XF.VisualElement.HeightRequest):
                    VisualElementControl.HeightRequest = AttributeHelper.StringToDouble((string)attributeValue, HeightRequestDefaultValue);
                    break;
                case nameof(XF.VisualElement.InputTransparent):
                    VisualElementControl.InputTransparent = AttributeHelper.GetBool(attributeValue, InputTransparentDefaultValue);
                    break;
                case nameof(XF.VisualElement.IsEnabled):
                    VisualElementControl.IsEnabled = AttributeHelper.GetBool(attributeValue, IsEnabledDefaultValue);
                    break;
                case nameof(XF.VisualElement.IsTabStop):
                    VisualElementControl.IsTabStop = AttributeHelper.GetBool(attributeValue, IsTabStopDefaultValue);
                    break;
                case nameof(XF.VisualElement.IsVisible):
                    VisualElementControl.IsVisible = AttributeHelper.GetBool(attributeValue, IsVisibleDefaultValue);
                    break;
                case nameof(XF.VisualElement.MinimumHeightRequest):
                    VisualElementControl.MinimumHeightRequest = AttributeHelper.StringToDouble((string)attributeValue, MinimumHeightRequestDefaultValue);
                    break;
                case nameof(XF.VisualElement.MinimumWidthRequest):
                    VisualElementControl.MinimumWidthRequest = AttributeHelper.StringToDouble((string)attributeValue, MinimumWidthRequestDefaultValue);
                    break;
                case nameof(XF.VisualElement.Opacity):
                    VisualElementControl.Opacity = AttributeHelper.StringToDouble((string)attributeValue, OpacityDefaultValue);
                    break;
                case nameof(XF.VisualElement.Rotation):
                    VisualElementControl.Rotation = AttributeHelper.StringToDouble((string)attributeValue, RotationDefaultValue);
                    break;
                case nameof(XF.VisualElement.RotationX):
                    VisualElementControl.RotationX = AttributeHelper.StringToDouble((string)attributeValue, RotationXDefaultValue);
                    break;
                case nameof(XF.VisualElement.RotationY):
                    VisualElementControl.RotationY = AttributeHelper.StringToDouble((string)attributeValue, RotationYDefaultValue);
                    break;
                case nameof(XF.VisualElement.Scale):
                    VisualElementControl.Scale = AttributeHelper.StringToDouble((string)attributeValue, ScaleDefaultValue);
                    break;
                case nameof(XF.VisualElement.ScaleX):
                    VisualElementControl.ScaleX = AttributeHelper.StringToDouble((string)attributeValue, ScaleXDefaultValue);
                    break;
                case nameof(XF.VisualElement.ScaleY):
                    VisualElementControl.ScaleY = AttributeHelper.StringToDouble((string)attributeValue, ScaleYDefaultValue);
                    break;
                case nameof(XF.VisualElement.TabIndex):
                    VisualElementControl.TabIndex = AttributeHelper.GetInt(attributeValue, TabIndexDefaultValue);
                    break;
                case nameof(XF.VisualElement.TranslationX):
                    VisualElementControl.TranslationX = AttributeHelper.StringToDouble((string)attributeValue, TranslationXDefaultValue);
                    break;
                case nameof(XF.VisualElement.TranslationY):
                    VisualElementControl.TranslationY = AttributeHelper.StringToDouble((string)attributeValue, TranslationYDefaultValue);
                    break;
                case nameof(XF.VisualElement.WidthRequest):
                    VisualElementControl.WidthRequest = AttributeHelper.StringToDouble((string)attributeValue, WidthRequestDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
