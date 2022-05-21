// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellItem : ShellGroupItem
    {
        static ShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellItem>(
                renderer => new ShellItemHandler(renderer, new MC.ShellItem()));

            RegisterAdditionalHandlers();
        }

        public new MC.ShellItem NativeControl => (ElementHandler as ShellItemHandler)?.ShellItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}