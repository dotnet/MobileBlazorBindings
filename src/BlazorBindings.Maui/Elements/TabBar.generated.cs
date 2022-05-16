// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TabBar : ShellItem
    {
        static TabBar()
        {
            ElementHandlerRegistry.RegisterElementHandler<TabBar>(
                renderer => new TabBarHandler(renderer, new MC.TabBar()));

            RegisterAdditionalHandlers();
        }

        public new MC.TabBar NativeControl => (ElementHandler as TabBarHandler)?.TabBarControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
