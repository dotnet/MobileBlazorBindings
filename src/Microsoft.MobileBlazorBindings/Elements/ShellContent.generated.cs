// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
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
