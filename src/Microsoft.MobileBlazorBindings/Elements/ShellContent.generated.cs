// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ShellContent : BaseShellItem
    {
        static ShellContent()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellContent>(
                renderer => new ShellContentHandler(renderer, new XF.ShellContent()));
        }

        public new XF.ShellContent NativeControl => ((ShellContentHandler)ElementHandler).ShellContentControl;
    }
}
