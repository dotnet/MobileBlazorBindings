// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SliderHandler : ViewHandler
    {
        private static readonly double MaximumDefaultValue = XF.Slider.MaximumProperty.DefaultValue is double value ? value : default;
        private static readonly XF.Color MaximumTrackColorDefaultValue = XF.Slider.MaximumTrackColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly double MinimumDefaultValue = XF.Slider.MinimumProperty.DefaultValue is double value ? value : default;
        private static readonly XF.Color MinimumTrackColorDefaultValue = XF.Slider.MinimumTrackColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.Color ThumbColorDefaultValue = XF.Slider.ThumbColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.ImageSource ThumbImageSourceDefaultValue = XF.Slider.ThumbImageSourceProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly double ValueDefaultValue = XF.Slider.ValueProperty.DefaultValue is double value ? value : default;

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
                    SliderControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue, MaximumDefaultValue);
                    break;
                case nameof(XF.Slider.MaximumTrackColor):
                    SliderControl.MaximumTrackColor = AttributeHelper.StringToColor((string)attributeValue, MaximumTrackColorDefaultValue);
                    break;
                case nameof(XF.Slider.Minimum):
                    SliderControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue, MinimumDefaultValue);
                    break;
                case nameof(XF.Slider.MinimumTrackColor):
                    SliderControl.MinimumTrackColor = AttributeHelper.StringToColor((string)attributeValue, MinimumTrackColorDefaultValue);
                    break;
                case nameof(XF.Slider.ThumbColor):
                    SliderControl.ThumbColor = AttributeHelper.StringToColor((string)attributeValue, ThumbColorDefaultValue);
                    break;
                case nameof(XF.Slider.ThumbImageSource):
                    SliderControl.ThumbImageSource = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, ThumbImageSourceDefaultValue);
                    break;
                case nameof(XF.Slider.Value):
                    SliderControl.Value = AttributeHelper.StringToDouble((string)attributeValue, ValueDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
