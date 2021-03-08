// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ContentPage : TemplatedPage
    {
        static ContentPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<ContentPage>(
                renderer => new ContentPageHandler(renderer, new XF.ContentPage()));

            RegisterAdditionalHandlers();
        }

        public new XF.ContentPage NativeControl => ((ContentPageHandler)ElementHandler).ContentPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
