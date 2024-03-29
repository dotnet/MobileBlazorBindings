// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class PinchGestureRecognizer : GestureRecognizer
    {
        static PinchGestureRecognizer()
        {
            ElementHandlerRegistry.RegisterElementHandler<PinchGestureRecognizer>(
                renderer => new PinchGestureRecognizerHandler(renderer, new MC.PinchGestureRecognizer()));
        }

        [Parameter] public EventCallback<MC.PinchGestureUpdatedEventArgs> OnPinchUpdated { get; set; }

        public new MC.PinchGestureRecognizer NativeControl => ((PinchGestureRecognizerHandler)ElementHandler).PinchGestureRecognizerControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onpinchupdated", OnPinchUpdated);
        }
    }
}
