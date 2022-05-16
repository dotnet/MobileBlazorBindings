// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class VerticalStackLayout : StackBase
    {
        static VerticalStackLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<VerticalStackLayout>(
                renderer => new VerticalStackLayoutHandler(renderer, new MC.VerticalStackLayout()));

            RegisterAdditionalHandlers();
        }

        public new MC.VerticalStackLayout NativeControl => (ElementHandler as VerticalStackLayoutHandler)?.VerticalStackLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
