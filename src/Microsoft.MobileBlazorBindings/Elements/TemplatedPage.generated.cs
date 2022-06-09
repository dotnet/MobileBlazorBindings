// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TemplatedPage : Page
    {
        static TemplatedPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<TemplatedPage>(
                renderer => new TemplatedPageHandler(renderer, new MC.TemplatedPage()));

            RegisterAdditionalHandlers();
        }

        public new MC.TemplatedPage NativeControl => ((TemplatedPageHandler)ElementHandler).TemplatedPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
