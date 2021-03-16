// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class GestureElement : Element
    {
        static GestureElement()
        {
            ElementHandlerRegistry.RegisterElementHandler<GestureElement>(
                renderer => new GestureElementHandler(renderer, new XF.GestureElement()));

            RegisterAdditionalHandlers();
        }

        public new XF.GestureElement NativeControl => ((GestureElementHandler)ElementHandler).GestureElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
