// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class GestureElement : Element
    {
        public new XF.GestureElement NativeControl => ((GestureElementHandler)ElementHandler).GestureElementControl;
    }
}
