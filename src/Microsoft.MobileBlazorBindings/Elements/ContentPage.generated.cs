// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ContentPage : TemplatedPage
    {
        static ContentPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<ContentPage>(
                renderer => new ContentPageHandler(renderer, new MC.ContentPage()));

            RegisterAdditionalHandlers();
        }

        public new MC.ContentPage NativeControl => ((ContentPageHandler)ElementHandler).ContentPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
