// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellContent : BaseShellItem
    {
        static ShellContent()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellContent>(
                renderer => new ShellContentHandler(renderer, new MC.ShellContent()));

            RegisterAdditionalHandlers();
        }

        public new MC.ShellContent NativeControl => ((ShellContentHandler)ElementHandler).ShellContentControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
