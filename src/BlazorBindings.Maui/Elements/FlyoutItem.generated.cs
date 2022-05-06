// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlyoutItem : ShellItem
    {
        static FlyoutItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<FlyoutItem>(
                renderer => new FlyoutItemHandler(renderer, new MC.FlyoutItem()));

            RegisterAdditionalHandlers();
        }

        public new MC.FlyoutItem NativeControl => ((FlyoutItemHandler)ElementHandler).FlyoutItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
