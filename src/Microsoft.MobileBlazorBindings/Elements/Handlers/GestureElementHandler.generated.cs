// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GestureElementHandler : ElementHandler
    {
        public GestureElementHandler(NativeComponentRenderer renderer, XF.GestureElement gestureElementControl) : base(renderer, gestureElementControl)
        {
            GestureElementControl = gestureElementControl ?? throw new System.ArgumentNullException(nameof(gestureElementControl));
        }

        public XF.GestureElement GestureElementControl { get; }
    }
}
