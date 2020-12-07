// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class VisualElement : NavigableElement
    {

        /// <summary>
        /// Gets or sets the X component of the center point for any transform, relative to the bounds of the element. This is a bindable property.
        /// </summary>
        /// <value>
        /// The value that declares the X component of the transform. The default value is 0.5.
        /// </value>
        [Parameter] public double? AnchorX { get; set; }
        /// <summary>
        /// Gets or sets the Y component of the center point for any transform, relative to the bounds of the element. This is a bindable property.
        /// </summary>
        /// <value>
        /// The value that declares the Y component of the transform. The default value is 0.5.
        /// </value>
        [Parameter] public double? AnchorY { get; set; }
        /// <summary>
        /// Gets or sets the color which will fill the background of a VisualElement. This is a bindable property.
        /// </summary>
        /// <value>
        /// The color that is used to fill the background of a VisualElement. The default is <see cref="P:Xamarin.Forms.Color.Default" />.
        /// </value>
        [Parameter] public XF.Color? BackgroundColor { get; set; }
        /// <summary>
        /// Gets or sets the layout flow direction.
        /// </summary>
        /// <value>
        /// The layout flow direction.
        /// </value>
        [Parameter] public XF.FlowDirection? FlowDirection { get; set; }
        /// <summary>
        /// Gets or sets the desired height override of this element.
        /// </summary>
        /// <value>
        /// The height this element desires to be.
        /// </value>
        [Parameter] public double? HeightRequest { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this element should be involved in the user interaction cycle. This is a bindable property.
        /// </summary>
        /// <value>
        /// <see langword="false" /> if the element and its children should receive input; <see langword="true" /> if neither the element nor its children should receive input and should, instead, pass inputs to the elements that are visually behind the current visual element. Default is <see langword="false" />.
        /// </value>
        [Parameter] public bool? InputTransparent { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this element is enabled in the user interface. This is a bindable property.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the element is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />
        /// </value>
        [Parameter] public bool? IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates whether this element is included in tab navigation. This is a bindable property.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the element is included in tab navigation; otherwise, <see langword="false" />. Default value is <see langword="true" />.
        /// </value>
        [Parameter] public bool? IsTabStop { get; set; }
        /// <summary>
        /// Gets or sets a value that determines whether this elements should be part of the visual tree or not. This is a bindable property.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the element should be rendered; otherwise, <see langword="false" />. Default value is <see langword="true" />.
        /// </value>
        [Parameter] public bool? IsVisible { get; set; }
        /// <summary>
        /// Gets or sets a value which overrides the minimum height the element will request during layout.
        /// </summary>
        /// <value>
        /// The minimum height the element requires. Default value is -1.
        /// </value>
        [Parameter] public double? MinimumHeightRequest { get; set; }
        /// <summary>
        /// Gets or sets a value which overrides the minimum width the element will request during layout.
        /// </summary>
        /// <value>
        /// The minimum width the element requires. Default value is -1.
        /// </value>
        [Parameter] public double? MinimumWidthRequest { get; set; }
        /// <summary>
        /// Gets or sets the opacity value applied to the element when it is rendered. This is a bindable property.
        /// </summary>
        /// <value>
        /// The opacity value. Default opacity is 1.0. Values will be clamped between 0 and 1 on set.
        /// </value>
        [Parameter] public double? Opacity { get; set; }
        /// <summary>
        /// Gets or sets the rotation (in degrees) about the Z-axis (affine rotation) when the element is rendered.
        /// </summary>
        /// <value>
        /// The rotation about the Z-axis in degrees.
        /// </value>
        [Parameter] public double? Rotation { get; set; }
        /// <summary>
        /// Gets or sets the rotation (in degrees) about the X-axis (perspective rotation) when the element is rendered.
        /// </summary>
        /// <value>
        /// The rotation about the X-axis in degrees.
        /// </value>
        [Parameter] public double? RotationX { get; set; }
        /// <summary>
        /// Gets or sets the rotation (in degrees) about the Y-axis (perspective rotation) when the element is rendered.
        /// </summary>
        /// <value>
        /// The rotation about the Y-axis in degrees.
        /// </value>
        [Parameter] public double? RotationY { get; set; }
        /// <summary>
        /// Gets or sets the scale factor applied to the element.
        /// </summary>
        /// <value>
        /// The scale factor of the element. Default value is 1.0.
        /// </value>
        [Parameter] public double? Scale { get; set; }
        /// <summary>
        /// Gets or sets a scale value to apply to the X direction.
        /// </summary>
        /// <value>
        /// The scale value to apply to the X direction.
        /// </value>
        [Parameter] public double? ScaleX { get; set; }
        /// <summary>
        /// Gets or sets a scale value to apply to the Y direction.
        /// </summary>
        /// <value>
        /// The scale value to apply to the Y direction.
        /// </value>
        [Parameter] public double? ScaleY { get; set; }
        [Parameter] public int? TabIndex { get; set; }
        /// <summary>
        /// Gets or sets the X translation delta of the element.
        /// </summary>
        /// <value>
        /// The amount to translate the element.
        /// </value>
        [Parameter] public double? TranslationX { get; set; }
        /// <summary>
        /// Gets or sets the Y translation delta of the element.
        /// </summary>
        /// <value>
        /// The amount to translate the element.
        /// </value>
        [Parameter] public double? TranslationY { get; set; }
        /// <summary>
        /// Gets or sets the desired width override of this element.
        /// </summary>
        /// <value>
        /// The width this element desires to be.
        /// </value>
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
