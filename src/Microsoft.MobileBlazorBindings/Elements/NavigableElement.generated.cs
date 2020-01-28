// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class NavigableElement : Element
    {
        /// <summary>
        /// A comma-separated list of style classes that should be applied to this element.
        /// </summary>
        [Parameter] public string StyleClass { get; set; }

        public new XF.NavigableElement NativeControl => ((NavigableElementHandler)ElementHandler).NavigableElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (StyleClass != null)
            {
                builder.AddAttribute(nameof(StyleClass), StyleClass);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
