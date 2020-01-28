// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ShellItem : ShellGroupItem
    {
        static ShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellItem>(
                renderer => new ShellItemHandler(renderer, new XF.ShellItem()));
        }

        public new XF.ShellItem NativeControl => ((ShellItemHandler)ElementHandler).ShellItemControl;
    }
}
