// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract partial class Layout : View
    {

        /// <summary>
        /// Gets or sets a value that controls whether child elements inherit the input transparency of <see langword="this" /> layout when the tranparency is <see langword="true" />.
        /// </summary>
        /// <value>
        /// <see langword="true" /> to cause child elements to inherit the input transparency of <see langword="this" /> layout, when <see langword="this" /> layout's <see cref="P:Xamarin.Forms.VisualElement.InputTransparent" /> is true. <see langword="false" /> to cause child elements to ignore the input tranparency of <see langword="this" /> layout.
        /// </value>
        [Parameter] public bool? CascadeInputTransparent { get; set; }
        /// <summary>
        /// Gets or sets a value which determines if the Layout should clip its children to its bounds.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the Layout is clipped; otherwise, <see langword="false" />. The default value is <see langword="false" />.
        /// </value>
        [Parameter] public bool? IsClippedToBounds { get; set; }
        /// <summary>
        /// Gets or sets the inner padding of the Layout.
        /// </summary>
        /// <value>
        /// The Thickness values for the layout. The default value is a Thickness with all values set to 0.
        /// </value>
        [Parameter] public XF.Thickness? Padding { get; set; }

        public new XF.Layout NativeControl => ((LayoutHandler)ElementHandler).LayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CascadeInputTransparent != null)
            {
                builder.AddAttribute(nameof(CascadeInputTransparent), CascadeInputTransparent.Value);
            }
            if (IsClippedToBounds != null)
            {
                builder.AddAttribute(nameof(IsClippedToBounds), IsClippedToBounds.Value);
            }
            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
