// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ImageButtonHandler : ViewHandler
    {
        private static readonly Aspect AspectDefaultValue = MC.ImageButton.AspectProperty.DefaultValue is Aspect value ? value : default;
        private static readonly Color BorderColorDefaultValue = MC.ImageButton.BorderColorProperty.DefaultValue is Color value ? value : default;
        private static readonly double BorderWidthDefaultValue = MC.ImageButton.BorderWidthProperty.DefaultValue is double value ? value : default;
        private static readonly int CornerRadiusDefaultValue = MC.ImageButton.CornerRadiusProperty.DefaultValue is int value ? value : default;
        private static readonly bool IsOpaqueDefaultValue = MC.ImageButton.IsOpaqueProperty.DefaultValue is bool value ? value : default;
        private static readonly Thickness PaddingDefaultValue = MC.ImageButton.PaddingProperty.DefaultValue is Thickness value ? value : default;
        private static readonly MC.ImageSource SourceDefaultValue = MC.ImageButton.SourceProperty.DefaultValue is MC.ImageSource value ? value : default;

        public ImageButtonHandler(NativeComponentRenderer renderer, MC.ImageButton imageButtonControl) : base(renderer, imageButtonControl)
        {
            ImageButtonControl = imageButtonControl ?? throw new ArgumentNullException(nameof(imageButtonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ImageButton ImageButtonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ImageButton.Aspect):
                    ImageButtonControl.Aspect = (Aspect)AttributeHelper.GetInt(attributeValue, (int)AspectDefaultValue);
                    break;
                case nameof(MC.ImageButton.BorderColor):
                    ImageButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(MC.ImageButton.BorderWidth):
                    ImageButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, BorderWidthDefaultValue);
                    break;
                case nameof(MC.ImageButton.CornerRadius):
                    ImageButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(MC.ImageButton.IsOpaque):
                    ImageButtonControl.IsOpaque = AttributeHelper.GetBool(attributeValue, IsOpaqueDefaultValue);
                    break;
                case nameof(MC.ImageButton.Padding):
                    ImageButtonControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(MC.ImageButton.Source):
                    ImageButtonControl.Source = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, SourceDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
