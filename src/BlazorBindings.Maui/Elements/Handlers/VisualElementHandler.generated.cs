// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class VisualElementHandler : NavigableElementHandler
    {
        private static readonly double AnchorXDefaultValue = MC.VisualElement.AnchorXProperty.DefaultValue is double value ? value : default;
        private static readonly double AnchorYDefaultValue = MC.VisualElement.AnchorYProperty.DefaultValue is double value ? value : default;
        private static readonly Color BackgroundColorDefaultValue = MC.VisualElement.BackgroundColorProperty.DefaultValue is Color value ? value : default;
        private static readonly FlowDirection FlowDirectionDefaultValue = MC.VisualElement.FlowDirectionProperty.DefaultValue is FlowDirection value ? value : default;
        private static readonly double HeightRequestDefaultValue = MC.VisualElement.HeightRequestProperty.DefaultValue is double value ? value : default;
        private static readonly bool InputTransparentDefaultValue = MC.VisualElement.InputTransparentProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsEnabledDefaultValue = MC.VisualElement.IsEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsVisibleDefaultValue = MC.VisualElement.IsVisibleProperty.DefaultValue is bool value ? value : default;
        private static readonly double MaximumHeightRequestDefaultValue = MC.VisualElement.MaximumHeightRequestProperty.DefaultValue is double value ? value : default;
        private static readonly double MaximumWidthRequestDefaultValue = MC.VisualElement.MaximumWidthRequestProperty.DefaultValue is double value ? value : default;
        private static readonly double MinimumHeightRequestDefaultValue = MC.VisualElement.MinimumHeightRequestProperty.DefaultValue is double value ? value : default;
        private static readonly double MinimumWidthRequestDefaultValue = MC.VisualElement.MinimumWidthRequestProperty.DefaultValue is double value ? value : default;
        private static readonly double OpacityDefaultValue = MC.VisualElement.OpacityProperty.DefaultValue is double value ? value : default;
        private static readonly double RotationDefaultValue = MC.VisualElement.RotationProperty.DefaultValue is double value ? value : default;
        private static readonly double RotationXDefaultValue = MC.VisualElement.RotationXProperty.DefaultValue is double value ? value : default;
        private static readonly double RotationYDefaultValue = MC.VisualElement.RotationYProperty.DefaultValue is double value ? value : default;
        private static readonly double ScaleDefaultValue = MC.VisualElement.ScaleProperty.DefaultValue is double value ? value : default;
        private static readonly double ScaleXDefaultValue = MC.VisualElement.ScaleXProperty.DefaultValue is double value ? value : default;
        private static readonly double ScaleYDefaultValue = MC.VisualElement.ScaleYProperty.DefaultValue is double value ? value : default;
        private static readonly double TranslationXDefaultValue = MC.VisualElement.TranslationXProperty.DefaultValue is double value ? value : default;
        private static readonly double TranslationYDefaultValue = MC.VisualElement.TranslationYProperty.DefaultValue is double value ? value : default;
        private static readonly double WidthRequestDefaultValue = MC.VisualElement.WidthRequestProperty.DefaultValue is double value ? value : default;

        public VisualElementHandler(NativeComponentRenderer renderer, MC.VisualElement visualElementControl) : base(renderer, visualElementControl)
        {
            VisualElementControl = visualElementControl ?? throw new ArgumentNullException(nameof(visualElementControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.VisualElement VisualElementControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.VisualElement.AnchorX):
                    VisualElementControl.AnchorX = AttributeHelper.StringToDouble((string)attributeValue, AnchorXDefaultValue);
                    break;
                case nameof(MC.VisualElement.AnchorY):
                    VisualElementControl.AnchorY = AttributeHelper.StringToDouble((string)attributeValue, AnchorYDefaultValue);
                    break;
                case nameof(MC.VisualElement.BackgroundColor):
                    VisualElementControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue, BackgroundColorDefaultValue);
                    break;
                case nameof(MC.VisualElement.FlowDirection):
                    VisualElementControl.FlowDirection = (FlowDirection)AttributeHelper.GetInt(attributeValue, (int)FlowDirectionDefaultValue);
                    break;
                case nameof(MC.VisualElement.Frame):
                    VisualElementControl.Frame = AttributeHelper.StringToRect(attributeValue);
                    break;
                case nameof(MC.VisualElement.HeightRequest):
                    VisualElementControl.HeightRequest = AttributeHelper.StringToDouble((string)attributeValue, HeightRequestDefaultValue);
                    break;
                case nameof(MC.VisualElement.InputTransparent):
                    VisualElementControl.InputTransparent = AttributeHelper.GetBool(attributeValue, InputTransparentDefaultValue);
                    break;
                case nameof(MC.VisualElement.IsEnabled):
                    VisualElementControl.IsEnabled = AttributeHelper.GetBool(attributeValue, IsEnabledDefaultValue);
                    break;
                case nameof(MC.VisualElement.IsVisible):
                    VisualElementControl.IsVisible = AttributeHelper.GetBool(attributeValue, IsVisibleDefaultValue);
                    break;
                case nameof(MC.VisualElement.MaximumHeightRequest):
                    VisualElementControl.MaximumHeightRequest = AttributeHelper.StringToDouble((string)attributeValue, MaximumHeightRequestDefaultValue);
                    break;
                case nameof(MC.VisualElement.MaximumWidthRequest):
                    VisualElementControl.MaximumWidthRequest = AttributeHelper.StringToDouble((string)attributeValue, MaximumWidthRequestDefaultValue);
                    break;
                case nameof(MC.VisualElement.MinimumHeightRequest):
                    VisualElementControl.MinimumHeightRequest = AttributeHelper.StringToDouble((string)attributeValue, MinimumHeightRequestDefaultValue);
                    break;
                case nameof(MC.VisualElement.MinimumWidthRequest):
                    VisualElementControl.MinimumWidthRequest = AttributeHelper.StringToDouble((string)attributeValue, MinimumWidthRequestDefaultValue);
                    break;
                case nameof(MC.VisualElement.Opacity):
                    VisualElementControl.Opacity = AttributeHelper.StringToDouble((string)attributeValue, OpacityDefaultValue);
                    break;
                case nameof(MC.VisualElement.Rotation):
                    VisualElementControl.Rotation = AttributeHelper.StringToDouble((string)attributeValue, RotationDefaultValue);
                    break;
                case nameof(MC.VisualElement.RotationX):
                    VisualElementControl.RotationX = AttributeHelper.StringToDouble((string)attributeValue, RotationXDefaultValue);
                    break;
                case nameof(MC.VisualElement.RotationY):
                    VisualElementControl.RotationY = AttributeHelper.StringToDouble((string)attributeValue, RotationYDefaultValue);
                    break;
                case nameof(MC.VisualElement.Scale):
                    VisualElementControl.Scale = AttributeHelper.StringToDouble((string)attributeValue, ScaleDefaultValue);
                    break;
                case nameof(MC.VisualElement.ScaleX):
                    VisualElementControl.ScaleX = AttributeHelper.StringToDouble((string)attributeValue, ScaleXDefaultValue);
                    break;
                case nameof(MC.VisualElement.ScaleY):
                    VisualElementControl.ScaleY = AttributeHelper.StringToDouble((string)attributeValue, ScaleYDefaultValue);
                    break;
                case nameof(MC.VisualElement.TranslationX):
                    VisualElementControl.TranslationX = AttributeHelper.StringToDouble((string)attributeValue, TranslationXDefaultValue);
                    break;
                case nameof(MC.VisualElement.TranslationY):
                    VisualElementControl.TranslationY = AttributeHelper.StringToDouble((string)attributeValue, TranslationYDefaultValue);
                    break;
                case nameof(MC.VisualElement.WidthRequest):
                    VisualElementControl.WidthRequest = AttributeHelper.StringToDouble((string)attributeValue, WidthRequestDefaultValue);
                    break;
                case nameof(MC.VisualElement.ZIndex):
                    VisualElementControl.ZIndex = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
