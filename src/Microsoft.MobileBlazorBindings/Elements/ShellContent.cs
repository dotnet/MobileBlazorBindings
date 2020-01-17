// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ShellContent : BaseShellItem
    {
        static ShellContent()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellContent>(
                renderer => new ShellContentHandler(renderer, new XF.ShellContent()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        public new XF.ShellContent NativeControl => ((ShellContentHandler)ElementHandler).ShellContentControl;

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
