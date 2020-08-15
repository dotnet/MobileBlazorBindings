// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class SwipeGestureRecognizer : GestureRecognizer
    {
        static SwipeGestureRecognizer()
        {
            ElementHandlerRegistry.RegisterElementHandler<SwipeGestureRecognizer>(
                renderer => new SwipeGestureRecognizerHandler(renderer, new XF.SwipeGestureRecognizer()));
        }

        [Parameter] public XF.SwipeDirection? Direction { get; set; }
        [Parameter] public uint? Threshold { get; set; }

        [Parameter] public EventCallback<XF.SwipedEventArgs> OnSwiped { get; set; }

        public new XF.SwipeGestureRecognizer NativeControl => ((SwipeGestureRecognizerHandler)ElementHandler).SwipeGestureRecognizerControl;

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
