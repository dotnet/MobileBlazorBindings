// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ContentView : TemplatedView
    {
        static ContentView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ContentView>(
                renderer => new ContentViewHandler(renderer, new MC.ContentView()));

            RegisterAdditionalHandlers();
        }

        public new MC.ContentView NativeControl => (ElementHandler as ContentViewHandler)?.ContentViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
