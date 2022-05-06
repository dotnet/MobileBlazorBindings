// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
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
