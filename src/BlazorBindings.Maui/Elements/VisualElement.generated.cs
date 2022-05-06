// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class VisualElement : NavigableElement
    {
        static VisualElement()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double? AnchorX { get; set; }
        [Parameter] public double? AnchorY { get; set; }
        [Parameter] public Color BackgroundColor { get; set; }
        [Parameter] public FlowDirection? FlowDirection { get; set; }
        [Parameter] public Rect? Frame { get; set; }
        [Parameter] public double? HeightRequest { get; set; }
        [Parameter] public bool? InputTransparent { get; set; }
        [Parameter] public bool? IsEnabled { get; set; }
        [Parameter] public bool? IsVisible { get; set; }
        [Parameter] public double? MaximumHeightRequest { get; set; }
        [Parameter] public double? MaximumWidthRequest { get; set; }
        [Parameter] public double? MinimumHeightRequest { get; set; }
        [Parameter] public double? MinimumWidthRequest { get; set; }
        [Parameter] public double? Opacity { get; set; }
        [Parameter] public double? Rotation { get; set; }
        [Parameter] public double? RotationX { get; set; }
        [Parameter] public double? RotationY { get; set; }
        [Parameter] public double? Scale { get; set; }
        [Parameter] public double? ScaleX { get; set; }
        [Parameter] public double? ScaleY { get; set; }
        [Parameter] public double? TranslationX { get; set; }
        [Parameter] public double? TranslationY { get; set; }
        [Parameter] public double? WidthRequest { get; set; }
        [Parameter] public int? ZIndex { get; set; }

        public new MC.VisualElement NativeControl => ((VisualElementHandler)ElementHandler).VisualElementControl;

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
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor));
            }
            if (FlowDirection != null)
            {
                builder.AddAttribute(nameof(FlowDirection), (int)FlowDirection.Value);
            }
            if (Frame != null)
            {
                builder.AddAttribute(nameof(Frame), AttributeHelper.RectToString(Frame.Value));
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
            if (IsVisible != null)
            {
                builder.AddAttribute(nameof(IsVisible), IsVisible.Value);
            }
            if (MaximumHeightRequest != null)
            {
                builder.AddAttribute(nameof(MaximumHeightRequest), AttributeHelper.DoubleToString(MaximumHeightRequest.Value));
            }
            if (MaximumWidthRequest != null)
            {
                builder.AddAttribute(nameof(MaximumWidthRequest), AttributeHelper.DoubleToString(MaximumWidthRequest.Value));
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
            if (ZIndex != null)
            {
                builder.AddAttribute(nameof(ZIndex), ZIndex.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
