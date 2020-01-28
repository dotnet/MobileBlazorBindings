// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Tab : ShellSection
    {
        static Tab()
        {
            ElementHandlerRegistry.RegisterElementHandler<Tab>(
                renderer => new TabHandler(renderer, new XF.Tab()));
        }

        public new XF.Tab NativeControl => ((TabHandler)ElementHandler).TabControl;
    }
}
