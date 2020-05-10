// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class BoxView : View
    {
        static BoxView()
        {
            ElementHandlerRegistry.RegisterElementHandler<BoxView>(
                renderer => new BoxViewHandler(renderer, new XF.BoxView()));
        }

        /// <summary>
        /// Gets or sets the color which will fill the rectangle. This is a bindable property.
        /// </summary>
        /// <value>
        /// The color that is used to fill the rectangle. The default is <see cref="P:Xamarin.Forms.Color.Default" />.
        /// </value>
        [Parameter] public XF.Color? Color { get; set; }
        /// <summary>
        /// Gets or sets the corner radius for the box view.
        /// </summary>
        /// <value>
        /// The corner radius for the box view.
        /// </value>
        [Parameter] public XF.CornerRadius? CornerRadius { get; set; }

        public new XF.BoxView NativeControl => ((BoxViewHandler)ElementHandler).BoxViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.CornerRadiusToString(CornerRadius.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
