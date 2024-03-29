// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ShellSection : ShellGroupItem
    {
        static ShellSection()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellSection>(
                renderer => new ShellSectionHandler(renderer, new MC.ShellSection()));

            RegisterAdditionalHandlers();
        }

        public new MC.ShellSection NativeControl => ((ShellSectionHandler)ElementHandler).ShellSectionControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
