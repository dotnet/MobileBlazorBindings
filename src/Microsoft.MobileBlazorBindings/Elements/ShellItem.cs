// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ShellItem : ShellGroupItem
    {
        static ShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellItem>(
                renderer => new ShellItemHandler(renderer, new XF.ShellItem()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        public new XF.ShellGroupItem NativeControl => ((ShellGroupItemHandler)ElementHandler).ShellGroupItemControl;

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
