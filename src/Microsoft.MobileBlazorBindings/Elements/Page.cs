// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Page : VisualElement
    {
        static Page()
        {
            ElementHandlerRegistry.RegisterElementHandler<Page>(
                renderer => new PageHandler(renderer, new XF.Page()));
        }

        [Parameter] public XF.ImageSource IconImageSource { get; set; }
        [Parameter] public string Title { get; set; }

        public new XF.Page NativeControl => ((PageHandler)ElementHandler).PageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IconImageSource != null)
            {
                builder.AddAttribute(nameof(IconImageSource), AttributeHelper.ImageSourceToString(IconImageSource));
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
