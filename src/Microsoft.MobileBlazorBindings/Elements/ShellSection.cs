// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ShellSection : ShellGroupItem
    {
        static ShellSection()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellSection>(
                renderer => new ShellSectionHandler(renderer, new XF.ShellSection()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        public new XF.ShellSection NativeControl => ((ShellSectionHandler)ElementHandler).ShellSectionControl;

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
