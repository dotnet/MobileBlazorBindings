// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GestureElementHandler : ElementHandler
    {

        public GestureElementHandler(NativeComponentRenderer renderer, MC.GestureElement gestureElementControl) : base(renderer, gestureElementControl)
        {
            GestureElementControl = gestureElementControl ?? throw new ArgumentNullException(nameof(gestureElementControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.GestureElement GestureElementControl { get; }
    }
}
