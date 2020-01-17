// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TemplatedPage : Page
    {
        public new XF.TemplatedPage NativeControl => ((TemplatedPageHandler)ElementHandler).TemplatedPageControl;
    }
}
