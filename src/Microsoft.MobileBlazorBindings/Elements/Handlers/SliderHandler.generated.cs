// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SliderHandler : ViewHandler
    {
        public SliderHandler(NativeComponentRenderer renderer, XF.Slider sliderControl) : base(renderer, sliderControl)
        {
            SliderControl = sliderControl ?? throw new ArgumentNullException(nameof(sliderControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Slider SliderControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Slider.Maximum):
                    SliderControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue, 1.00);
                    break;
                case nameof(XF.Slider.MaximumTrackColor):
                    SliderControl.MaximumTrackColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Slider.Minimum):
                    SliderControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Slider.MinimumTrackColor):
                    SliderControl.MinimumTrackColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Slider.ThumbColor):
                    SliderControl.ThumbColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Slider.ThumbImageSource):
                    SliderControl.ThumbImageSource = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                case nameof(XF.Slider.Value):
                    SliderControl.Value = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
