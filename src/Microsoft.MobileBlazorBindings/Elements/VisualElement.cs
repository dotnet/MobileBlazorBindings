﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class VisualElement : NavigableElement
    {
        [Parameter] public double? AnchorX { get; set; }
        [Parameter] public double? AnchorY { get; set; }
        [Parameter] public XF.Color? BackgroundColor { get; set; }
        [Parameter] public XF.FlowDirection? FlowDirection { get; set; }
        [Parameter] public double? HeightRequest { get; set; }
        [Parameter] public bool? InputTransparent { get; set; }
        [Parameter] public bool? IsEnabled { get; set; }
        [Parameter] public bool? IsTabStop { get; set; }
        [Parameter] public bool? IsVisible { get; set; }
        [Parameter] public double? MinimumHeightRequest { get; set; }
        [Parameter] public double? MinimumWidthRequest { get; set; }
        [Parameter] public double? Opacity { get; set; }
        [Parameter] public double? Rotation { get; set; }
        [Parameter] public double? RotationX { get; set; }
        [Parameter] public double? RotationY { get; set; }
        [Parameter] public double? Scale { get; set; }
        [Parameter] public double? ScaleX { get; set; }
        [Parameter] public double? ScaleY { get; set; }
        [Parameter] public int? TabIndex { get; set; }
        [Parameter] public double? TranslationX { get; set; }
        [Parameter] public double? TranslationY { get; set; }
        [Parameter] public double? WidthRequest { get; set; }

        public new XF.VisualElement NativeControl => ((VisualElementHandler)ElementHandler).VisualElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AnchorX != null)
            {
                builder.AddAttribute(nameof(AnchorX), AttributeHelper.DoubleToString(AnchorX.Value));
            }
            if (AnchorY != null)
            {
                builder.AddAttribute(nameof(AnchorY), AttributeHelper.DoubleToString(AnchorY.Value));
            }
            if (BackgroundColor != null)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor.Value));
            }
            if (FlowDirection != null)
            {
                builder.AddAttribute(nameof(FlowDirection), (int)FlowDirection.Value);
            }
            if (HeightRequest != null)
            {
                builder.AddAttribute(nameof(HeightRequest), AttributeHelper.DoubleToString(HeightRequest.Value));
            }
            if (InputTransparent != null)
            {
                builder.AddAttribute(nameof(InputTransparent), InputTransparent.Value);
            }
            if (IsEnabled != null)
            {
                builder.AddAttribute(nameof(IsEnabled), IsEnabled.Value);
            }
            if (IsTabStop != null)
            {
                builder.AddAttribute(nameof(IsTabStop), IsTabStop.Value);
            }
            if (IsVisible != null)
            {
                builder.AddAttribute(nameof(IsVisible), IsVisible.Value);
            }
            if (MinimumHeightRequest != null)
            {
                builder.AddAttribute(nameof(MinimumHeightRequest), AttributeHelper.DoubleToString(MinimumHeightRequest.Value));
            }
            if (MinimumWidthRequest != null)
            {
                builder.AddAttribute(nameof(MinimumWidthRequest), AttributeHelper.DoubleToString(MinimumWidthRequest.Value));
            }
            if (Opacity != null)
            {
                builder.AddAttribute(nameof(Opacity), AttributeHelper.DoubleToString(Opacity.Value));
            }
            if (Rotation != null)
            {
                builder.AddAttribute(nameof(Rotation), AttributeHelper.DoubleToString(Rotation.Value));
            }
            if (RotationX != null)
            {
                builder.AddAttribute(nameof(RotationX), AttributeHelper.DoubleToString(RotationX.Value));
            }
            if (RotationY != null)
            {
                builder.AddAttribute(nameof(RotationY), AttributeHelper.DoubleToString(RotationY.Value));
            }
            if (Scale != null)
            {
                builder.AddAttribute(nameof(Scale), AttributeHelper.DoubleToString(Scale.Value));
            }
            if (ScaleX != null)
            {
                builder.AddAttribute(nameof(ScaleX), AttributeHelper.DoubleToString(ScaleX.Value));
            }
            if (ScaleY != null)
            {
                builder.AddAttribute(nameof(ScaleY), AttributeHelper.DoubleToString(ScaleY.Value));
            }
            if (TabIndex != null)
            {
                builder.AddAttribute(nameof(TabIndex), TabIndex.Value);
            }
            if (TranslationX != null)
            {
                builder.AddAttribute(nameof(TranslationX), AttributeHelper.DoubleToString(TranslationX.Value));
            }
            if (TranslationY != null)
            {
                builder.AddAttribute(nameof(TranslationY), AttributeHelper.DoubleToString(TranslationY.Value));
            }
            if (WidthRequest != null)
            {
                builder.AddAttribute(nameof(WidthRequest), AttributeHelper.DoubleToString(WidthRequest.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
