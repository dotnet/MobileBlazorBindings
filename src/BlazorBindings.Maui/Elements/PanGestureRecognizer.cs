// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class PanGestureRecognizer : GestureRecognizer
    {
        static PanGestureRecognizer()
        {
            ElementHandlerRegistry.RegisterElementHandler<PanGestureRecognizer>(
                renderer => new PanGestureRecognizerHandler(renderer, new MC.PanGestureRecognizer()));
        }

        [Parameter] public EventCallback<MC.PanUpdatedEventArgs> OnPanUpdated { get; set; }

        public new MC.PanGestureRecognizer NativeControl => ((PanGestureRecognizerHandler)ElementHandler).PanGestureRecognizerControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onpanupdated", OnPanUpdated);
        }
    }
}
