// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Forms
{
    public abstract class ViewWrapper : ComponentBase
    {
        /// <summary>
        /// Gets or sets the <see cref="XF.LayoutOptions" /> that define how the element gets laid in a layout cycle. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="XF.LayoutOptions" /> which defines how to lay out the element. Default value is <see cref="XF.LayoutOptions.Fill" /> unless otherwise documented.
        /// </value>
        [Parameter] public XF.LayoutOptions? HorizontalOptions { get; set; }
        /// <summary>
        /// Gets or sets the margin for the view.
        /// </summary>
        [Parameter] public XF.Thickness? Margin { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="XF.LayoutOptions" /> that define how the element gets laid in a layout cycle. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="XF.LayoutOptions" /> which defines how to lay out the element. Default value is <see cref="XF.LayoutOptions.Fill" /> unless otherwise documented.
        /// </value>
        [Parameter] public XF.LayoutOptions? VerticalOptions { get; set; }


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
        /// The color that is used to fill the background of a VisualElement. The default is <see cref="XF.Color.Default" />.
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
        /// Gets or sets the rotation about the Z-axis (affine rotation) when the element is rendered.
        /// </summary>
        /// <value>
        /// The rotation about the Z-axis in degrees.
        /// </value>
        [Parameter] public double? Rotation { get; set; }
        /// <summary>
        /// Gets or sets the rotation about the X-axis (perspective rotation) when the element is rendered.
        /// </summary>
        /// <value>
        /// The rotation about the X-axis in degrees.
        /// </value>
        [Parameter] public double? RotationX { get; set; }
        /// <summary>
        /// Gets or sets the rotation about the Y-axis (perspective rotation) when the element is rendered.
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

        [Parameter] public string @class { get; set; }
        [Parameter] public string StyleClass { get; set; }

        [Parameter] public string AutomationId { get; set; }
        [Parameter] public string ClassId { get; set; }
        [Parameter] public string StyleId { get; set; }

        protected virtual IEnumerable<KeyValuePair<string, object>> WrappedProperties
        {
            get
            {
                yield return new KeyValuePair<string, object>(nameof(HorizontalOptions), HorizontalOptions);
                yield return new KeyValuePair<string, object>(nameof(Margin), Margin);
                yield return new KeyValuePair<string, object>(nameof(VerticalOptions), VerticalOptions);

                yield return new KeyValuePair<string, object>(nameof(AnchorX), AnchorX);
                yield return new KeyValuePair<string, object>(nameof(AnchorY), AnchorY);
                yield return new KeyValuePair<string, object>(nameof(BackgroundColor), BackgroundColor);
                yield return new KeyValuePair<string, object>(nameof(FlowDirection), FlowDirection);
                yield return new KeyValuePair<string, object>(nameof(HeightRequest), HeightRequest);
                yield return new KeyValuePair<string, object>(nameof(InputTransparent), InputTransparent);
                yield return new KeyValuePair<string, object>(nameof(IsEnabled), IsEnabled);
                yield return new KeyValuePair<string, object>(nameof(IsTabStop), IsTabStop);
                yield return new KeyValuePair<string, object>(nameof(IsVisible), IsVisible);
                yield return new KeyValuePair<string, object>(nameof(MinimumHeightRequest), MinimumHeightRequest);
                yield return new KeyValuePair<string, object>(nameof(MinimumWidthRequest), MinimumWidthRequest);
                yield return new KeyValuePair<string, object>(nameof(Opacity), Opacity);
                yield return new KeyValuePair<string, object>(nameof(Rotation), Rotation);
                yield return new KeyValuePair<string, object>(nameof(RotationX), RotationX);
                yield return new KeyValuePair<string, object>(nameof(RotationY), RotationY);
                yield return new KeyValuePair<string, object>(nameof(Scale), Scale);
                yield return new KeyValuePair<string, object>(nameof(ScaleX), ScaleX);
                yield return new KeyValuePair<string, object>(nameof(ScaleY), ScaleY);
                yield return new KeyValuePair<string, object>(nameof(TabIndex), TabIndex);
                yield return new KeyValuePair<string, object>(nameof(TranslationX), TranslationX);
                yield return new KeyValuePair<string, object>(nameof(TranslationY), TranslationY);
                yield return new KeyValuePair<string, object>(nameof(WidthRequest), WidthRequest);

                yield return new KeyValuePair<string, object>(nameof(@class), @class);
                yield return new KeyValuePair<string, object>(nameof(StyleClass), StyleClass);

                yield return new KeyValuePair<string, object>(nameof(AutomationId), AutomationId);
                yield return new KeyValuePair<string, object>(nameof(ClassId), ClassId);
                yield return new KeyValuePair<string, object>(nameof(StyleId), StyleId);
            }
        }

        protected abstract IEnumerable<KeyValuePair<string, object>> AdditionalProperties { get; }

        protected virtual IEnumerable<KeyValuePair<string, object>> Properties
        {
            get
            {
                return WrappedProperties.Concat(AdditionalProperties).Where(kvp => kvp.Value != null);
            }
        }
    }
}
