// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ImageButtonHandler : ViewHandler
    {
        private static readonly XF.Aspect AspectDefaultValue = XF.ImageButton.AspectProperty.DefaultValue is XF.Aspect value ? value : default;
        private static readonly XF.Color BorderColorDefaultValue = XF.ImageButton.BorderColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly double BorderWidthDefaultValue = XF.ImageButton.BorderWidthProperty.DefaultValue is double value ? value : default;
        private static readonly int CornerRadiusDefaultValue = XF.ImageButton.CornerRadiusProperty.DefaultValue is int value ? value : default;
        private static readonly bool IsOpaqueDefaultValue = XF.ImageButton.IsOpaqueProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.Thickness PaddingDefaultValue = XF.ImageButton.PaddingProperty.DefaultValue is XF.Thickness value ? value : default;
        private static readonly XF.ImageSource SourceDefaultValue = XF.ImageButton.SourceProperty.DefaultValue is XF.ImageSource value ? value : default;

        public ImageButtonHandler(NativeComponentRenderer renderer, XF.ImageButton imageButtonControl) : base(renderer, imageButtonControl)
        {
            ImageButtonControl = imageButtonControl ?? throw new ArgumentNullException(nameof(imageButtonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ImageButton ImageButtonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ImageButton.Aspect):
                    ImageButtonControl.Aspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue, (int)AspectDefaultValue);
                    break;
                case nameof(XF.ImageButton.BorderColor):
                    ImageButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(XF.ImageButton.BorderWidth):
                    ImageButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, BorderWidthDefaultValue);
                    break;
                case nameof(XF.ImageButton.CornerRadius):
                    ImageButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(XF.ImageButton.IsOpaque):
                    ImageButtonControl.IsOpaque = AttributeHelper.GetBool(attributeValue, IsOpaqueDefaultValue);
                    break;
                case nameof(XF.ImageButton.Padding):
                    ImageButtonControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(XF.ImageButton.Source):
                    ImageButtonControl.Source = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, SourceDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
