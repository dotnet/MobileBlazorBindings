// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TemplatedPage : Page
    {
        static TemplatedPage()
        {
            ElementHandlerRegistry.RegisterElementHandler<TemplatedPage>(
                renderer => new TemplatedPageHandler(renderer, new XF.TemplatedPage()));

            RegisterAdditionalHandlers();
        }

        public new XF.TemplatedPage NativeControl => ((TemplatedPageHandler)ElementHandler).TemplatedPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
