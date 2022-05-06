// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class GestureRecognizer : NativeControlComponentBase
    {
        public MC.GestureRecognizer NativeControl => ((GestureRecognizerHandler)ElementHandler).GestureRecognizerControl;
    }
}
