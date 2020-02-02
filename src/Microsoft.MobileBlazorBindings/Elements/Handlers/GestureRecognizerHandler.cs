// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.Text;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GestureRecognizerHandler : ElementHandler
    {
        public GestureRecognizerHandler(NativeComponentRenderer renderer, XF.GestureRecognizer elementControl) : base(renderer, elementControl)
        {
        }
    }
}
