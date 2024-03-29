// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Tab : ShellSection
    {
        static Tab()
        {
            ElementHandlerRegistry.RegisterElementHandler<Tab>(
                renderer => new TabHandler(renderer, new MC.Tab()));

            RegisterAdditionalHandlers();
        }

        public new MC.Tab NativeControl => ((TabHandler)ElementHandler).TabControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
