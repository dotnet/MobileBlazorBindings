// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GestureElementHandler : ElementHandler
    {

        public GestureElementHandler(NativeComponentRenderer renderer, XF.GestureElement gestureElementControl) : base(renderer, gestureElementControl)
        {
            GestureElementControl = gestureElementControl ?? throw new ArgumentNullException(nameof(gestureElementControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.GestureElement GestureElementControl { get; }
    }
}
