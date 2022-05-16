// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TemplatedView : BlazorBindings.Maui.Elements.Compatibility.Layout
    {
        static TemplatedView()
        {
            ElementHandlerRegistry.RegisterElementHandler<TemplatedView>(
                renderer => new TemplatedViewHandler(renderer, new MC.TemplatedView()));

            RegisterAdditionalHandlers();
        }

        public new MC.TemplatedView NativeControl => (ElementHandler as TemplatedViewHandler)?.TemplatedViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
