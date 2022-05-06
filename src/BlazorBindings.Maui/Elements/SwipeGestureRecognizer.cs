// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class SwipeGestureRecognizer : GestureRecognizer
    {
        static SwipeGestureRecognizer()
        {
            ElementHandlerRegistry.RegisterElementHandler<SwipeGestureRecognizer>(
                renderer => new SwipeGestureRecognizerHandler(renderer, new MC.SwipeGestureRecognizer()));
        }

        [Parameter] public SwipeDirection? Direction { get; set; }
        [Parameter] public uint? Threshold { get; set; }

        [Parameter] public EventCallback<MC.SwipedEventArgs> OnSwiped { get; set; }

        public new MC.SwipeGestureRecognizer NativeControl => ((SwipeGestureRecognizerHandler)ElementHandler).SwipeGestureRecognizerControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Direction != null)
            {
                builder.AddAttribute(nameof(Direction), (int)Direction.Value);
            }
            if (Threshold != null)
            {
                builder.AddAttribute(nameof(Threshold), AttributeHelper.UInt32ToString(Threshold.Value));
            }

            builder.AddAttribute("onswiped", OnSwiped);
        }
    }
}
