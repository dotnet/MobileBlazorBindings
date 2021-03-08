// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ShellItem : ShellGroupItem
    {
        static ShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellItem>(
                renderer => new ShellItemHandler(renderer, new XF.ShellItem()));

            RegisterAdditionalHandlers();
        }

        public new XF.ShellItem NativeControl => ((ShellItemHandler)ElementHandler).ShellItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
