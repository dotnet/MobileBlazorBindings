// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class GestureRecognizer : NativeControlComponentBase
    {
        public XF.GestureRecognizer NativeControl => ((GestureRecognizerHandler)ElementHandler).GestureRecognizerControl;
    }
}
