// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class View : VisualElement
    {

        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.LayoutOptions" /> that define how the element gets laid in a layout cycle. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.LayoutOptions" /> which defines how to lay out the element. Default value is <see cref="F:Xamarin.Forms.LayoutOptions.Fill" /> unless otherwise documented.
        /// </value>
        [Parameter] public XF.LayoutOptions? HorizontalOptions { get; set; }
        /// <summary>
        /// Gets or sets the margin for the view.
        /// </summary>
        [Parameter] public XF.Thickness? Margin { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.LayoutOptions" /> that define how the element gets laid in a layout cycle. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.LayoutOptions" /> which defines how to lay out the element. Default value is <see cref="F:Xamarin.Forms.LayoutOptions.Fill" /> unless otherwise documented.
        /// </value>
        [Parameter] public XF.LayoutOptions? VerticalOptions { get; set; }

        public new XF.View NativeControl => ((ViewHandler)ElementHandler).ViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalOptions != null)
            {
                builder.AddAttribute(nameof(HorizontalOptions), AttributeHelper.LayoutOptionsToString(HorizontalOptions.Value));
            }
            if (Margin != null)
            {
                builder.AddAttribute(nameof(Margin), AttributeHelper.ThicknessToString(Margin.Value));
            }
            if (VerticalOptions != null)
            {
                builder.AddAttribute(nameof(VerticalOptions), AttributeHelper.LayoutOptionsToString(VerticalOptions.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
