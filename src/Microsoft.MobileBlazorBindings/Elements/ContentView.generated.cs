// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ContentView : TemplatedView
    {
        static ContentView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ContentView>(
                renderer => new ContentViewHandler(renderer, new MC.ContentView()));

            RegisterAdditionalHandlers();
        }

        public new MC.ContentView NativeControl => ((ContentViewHandler)ElementHandler).ContentViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
