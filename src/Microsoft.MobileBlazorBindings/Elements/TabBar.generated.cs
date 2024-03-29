// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TabBar : ShellItem
    {
        static TabBar()
        {
            ElementHandlerRegistry.RegisterElementHandler<TabBar>(
                renderer => new TabBarHandler(renderer, new MC.TabBar()));

            RegisterAdditionalHandlers();
        }

        public new MC.TabBar NativeControl => ((TabBarHandler)ElementHandler).TabBarControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
