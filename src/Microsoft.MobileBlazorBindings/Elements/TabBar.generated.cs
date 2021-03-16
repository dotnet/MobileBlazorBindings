// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TabBar : ShellItem
    {
        static TabBar()
        {
            ElementHandlerRegistry.RegisterElementHandler<TabBar>(
                renderer => new TabBarHandler(renderer, new XF.TabBar()));

            RegisterAdditionalHandlers();
        }

        public new XF.TabBar NativeControl => ((TabBarHandler)ElementHandler).TabBarControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
