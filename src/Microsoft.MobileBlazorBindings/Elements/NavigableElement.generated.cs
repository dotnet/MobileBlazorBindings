// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class NavigableElement : Element
    {

        /// <summary>
        /// To be added.
        /// </summary>
        /// <value>
        /// To be added.
        /// </value>
        [Parameter] public string @class { get; set; }
        /// <summary>
        /// To be added.
        /// </summary>
        /// <value>
        /// To be added.
        /// </value>
        [Parameter] public string StyleClass { get; set; }

        public new XF.NavigableElement NativeControl => ((NavigableElementHandler)ElementHandler).NavigableElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (@class != null)
            {
                builder.AddAttribute(nameof(@class), @class);
            }
            if (StyleClass != null)
            {
                builder.AddAttribute(nameof(StyleClass), StyleClass);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
