// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TemplatedView : Layout
    {
        static TemplatedView()
        {
            ElementHandlerRegistry.RegisterElementHandler<TemplatedView>(
                renderer => new TemplatedViewHandler(renderer, new XF.TemplatedView()));

            RegisterAdditionalHandlers();
        }

        public new XF.TemplatedView NativeControl => ((TemplatedViewHandler)ElementHandler).TemplatedViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
