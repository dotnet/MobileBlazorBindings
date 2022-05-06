// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using Xamarin.Forms.PancakeView;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.PancakeView.Elements
{
    public class PancakeView : ContentView
    {
        static PancakeView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<PancakeView>(renderer => new PancakeViewHandler(renderer, new XF.PancakeView.PancakeView()));
        }

        [Parameter] public int? BackgroundGradientAngle { get; set; }
        [Parameter] public XF.Color? BackgroundGradientEndColor { get; set; }
        [Parameter] public XF.Color? BackgroundGradientStartColor { get; set; }
        //[Parameter] public IEnumerable<GradientStop> BackgroundGradientStops { get; set; }
        [Parameter] public XF.Color? BorderColor { get; set; }
        [Parameter] public BorderDrawingStyle? BorderDrawingStyle { get; set; }
        [Parameter] public int? BorderGradientAngle { get; set; }
        [Parameter] public XF.Color? BorderGradientEndColor { get; set; }
        [Parameter] public XF.Color? BorderGradientStartColor { get; set; }
        //[Parameter] public IEnumerable<GradientStop> BorderGradientStops { get; set; }
        [Parameter] public bool? BorderIsDashed { get; set; }
        [Parameter] public float? BorderThickness { get; set; }
        [Parameter] public XF.CornerRadius? CornerRadius { get; set; }
        [Parameter] public int? Elevation { get; set; }
        [Parameter] public bool? HasShadow { get; set; }
        [Parameter] public double? OffsetAngle { get; set; }
        [Parameter] public int? Sides { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundGradientAngle != null)
            {
                builder.AddAttribute(nameof(BackgroundGradientAngle), BackgroundGradientAngle.Value);
            }
            if (BackgroundGradientEndColor != null)
            {
                builder.AddAttribute(nameof(BackgroundGradientEndColor), AttributeHelper.ColorToString(BackgroundGradientEndColor.Value));
            }
            if (BackgroundGradientStartColor != null)
            {
                builder.AddAttribute(nameof(BackgroundGradientStartColor), AttributeHelper.ColorToString(BackgroundGradientStartColor.Value));
            }
            //IEnumerable<GradientStop> BackgroundGradientStops
            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor.Value));
            }
            if (BorderDrawingStyle != null)
            {
                builder.AddAttribute(nameof(BorderDrawingStyle), (int)BorderDrawingStyle.Value);
            }
            if (BorderGradientAngle != null)
            {
                builder.AddAttribute(nameof(BorderGradientAngle), BorderGradientAngle.Value);
            }
            if (BorderGradientEndColor != null)
            {
                builder.AddAttribute(nameof(BorderGradientEndColor), AttributeHelper.ColorToString(BorderGradientEndColor.Value));
            }
            if (BorderGradientStartColor != null)
            {
                builder.AddAttribute(nameof(BorderGradientStartColor), AttributeHelper.ColorToString(BorderGradientStartColor.Value));
            }
            //IEnumerable<GradientStop> BorderGradientStops
            if (BorderIsDashed != null)
            {
                builder.AddAttribute(nameof(BorderIsDashed), BorderIsDashed.Value);
            }
            if (BorderThickness != null)
            {
                builder.AddAttribute(nameof(BorderThickness), AttributeHelper.SingleToString(BorderThickness.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.CornerRadiusToString(CornerRadius.Value));
            }
            if (Elevation != null)
            {
                builder.AddAttribute(nameof(Elevation), Elevation.Value);
            }
            if (HasShadow != null)
            {
                builder.AddAttribute(nameof(HasShadow), HasShadow.Value);
            }
            if (OffsetAngle != null)
            {
                builder.AddAttribute(nameof(OffsetAngle), AttributeHelper.DoubleToString(OffsetAngle.Value));
            }
            if (Sides != null)
            {
                builder.AddAttribute(nameof(Sides), Sides.Value);
            }
        }
    }
}
