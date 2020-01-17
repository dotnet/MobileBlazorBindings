// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class BaseMenuItem : Element
    {
        public new XF.BaseMenuItem NativeControl => ((BaseMenuItemHandler)ElementHandler).BaseMenuItemControl;
    }
}
