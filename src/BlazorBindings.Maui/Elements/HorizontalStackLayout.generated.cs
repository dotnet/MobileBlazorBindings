// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class HorizontalStackLayout : StackBase
    {
        static HorizontalStackLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<HorizontalStackLayout>(
                renderer => new HorizontalStackLayoutHandler(renderer, new MC.HorizontalStackLayout()));

            RegisterAdditionalHandlers();
        }

        public new MC.HorizontalStackLayout NativeControl => (ElementHandler as HorizontalStackLayoutHandler)?.HorizontalStackLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
