// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class GestureRecognizer : NativeControlComponentBase
    {
        public MC.GestureRecognizer NativeControl => ((GestureRecognizerHandler)ElementHandler).GestureRecognizerControl;
    }
}
