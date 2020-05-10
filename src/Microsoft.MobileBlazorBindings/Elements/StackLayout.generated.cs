// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class StackLayout : Layout
    {
        static StackLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<StackLayout>(
                renderer => new StackLayoutHandler(renderer, new XF.StackLayout()));
        }

        /// <summary>
        /// Gets or sets the value which indicates the direction which child elements are positioned.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.StackOrientation" /> which indicates the direction children layouts flow. The default value is Vertical.
        /// </value>
        [Parameter] public XF.StackOrientation? Orientation { get; set; }
        /// <summary>
        /// Gets or sets a value which indicates the amount of space between each child element.
        /// </summary>
        /// <value>
        /// A value in device pixels which indicates the amount of space between each element. The default value is 6.0.
        /// </value>
        [Parameter] public double? Spacing { get; set; }

        public new XF.StackLayout NativeControl => ((StackLayoutHandler)ElementHandler).StackLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (Spacing != null)
            {
                builder.AddAttribute(nameof(Spacing), AttributeHelper.DoubleToString(Spacing.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
