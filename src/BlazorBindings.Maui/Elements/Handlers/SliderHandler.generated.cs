// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class SliderHandler : ViewHandler
    {
        private static readonly double MaximumDefaultValue = MC.Slider.MaximumProperty.DefaultValue is double value ? value : default;
        private static readonly Color MaximumTrackColorDefaultValue = MC.Slider.MaximumTrackColorProperty.DefaultValue is Color value ? value : default;
        private static readonly double MinimumDefaultValue = MC.Slider.MinimumProperty.DefaultValue is double value ? value : default;
        private static readonly Color MinimumTrackColorDefaultValue = MC.Slider.MinimumTrackColorProperty.DefaultValue is Color value ? value : default;
        private static readonly Color ThumbColorDefaultValue = MC.Slider.ThumbColorProperty.DefaultValue is Color value ? value : default;
        private static readonly MC.ImageSource ThumbImageSourceDefaultValue = MC.Slider.ThumbImageSourceProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly double ValueDefaultValue = MC.Slider.ValueProperty.DefaultValue is double value ? value : default;

        public SliderHandler(NativeComponentRenderer renderer, MC.Slider sliderControl) : base(renderer, sliderControl)
        {
            SliderControl = sliderControl ?? throw new ArgumentNullException(nameof(sliderControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Slider SliderControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Slider.Maximum):
                    SliderControl.Maximum = AttributeHelper.StringToDouble((string)attributeValue, MaximumDefaultValue);
                    break;
                case nameof(MC.Slider.MaximumTrackColor):
                    SliderControl.MaximumTrackColor = AttributeHelper.StringToColor((string)attributeValue, MaximumTrackColorDefaultValue);
                    break;
                case nameof(MC.Slider.Minimum):
                    SliderControl.Minimum = AttributeHelper.StringToDouble((string)attributeValue, MinimumDefaultValue);
                    break;
                case nameof(MC.Slider.MinimumTrackColor):
                    SliderControl.MinimumTrackColor = AttributeHelper.StringToColor((string)attributeValue, MinimumTrackColorDefaultValue);
                    break;
                case nameof(MC.Slider.ThumbColor):
                    SliderControl.ThumbColor = AttributeHelper.StringToColor((string)attributeValue, ThumbColorDefaultValue);
                    break;
                case nameof(MC.Slider.ThumbImageSource):
                    SliderControl.ThumbImageSource = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, ThumbImageSourceDefaultValue);
                    break;
                case nameof(MC.Slider.Value):
                    SliderControl.Value = AttributeHelper.StringToDouble((string)attributeValue, ValueDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
