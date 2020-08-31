// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ImageButtonHandler : ViewHandler
    {
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
                    ImageButtonControl.Aspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.ImageButton.BorderColor):
                    ImageButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.ImageButton.BorderWidth):
                    ImageButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.ImageButton.CornerRadius):
                    ImageButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, -1);
                    break;
                case nameof(XF.ImageButton.IsOpaque):
                    ImageButtonControl.IsOpaque = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.ImageButton.Padding):
                    ImageButtonControl.Padding = AttributeHelper.StringToThickness(attributeValue);
                    break;
                case nameof(XF.ImageButton.Source):
                    ImageButtonControl.Source = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
