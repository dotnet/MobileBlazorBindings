// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class PinchGestureRecognizerHandler : GestureRecognizerHandler
    {
        public PinchGestureRecognizerHandler(NativeComponentRenderer renderer, XF.PinchGestureRecognizer pinchGestureRecognizerControl) : base(renderer, pinchGestureRecognizerControl)
        {
            PinchGestureRecognizerControl = pinchGestureRecognizerControl ?? throw new ArgumentNullException(nameof(pinchGestureRecognizerControl));

            ConfigureEvent(
                eventName: "onpinchupdated",
                setId: id => PinchUpdatedEventHandlerId = id,
                clearId: id => { if (PinchUpdatedEventHandlerId == id) { PinchUpdatedEventHandlerId = 0; } });
            PinchGestureRecognizerControl.PinchUpdated += (s, e) =>
            {
                if (PinchUpdatedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(PinchUpdatedEventHandlerId, null, e));
                }
            };
        }

        public XF.PinchGestureRecognizer PinchGestureRecognizerControl { get; }

        public ulong PinchUpdatedEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
