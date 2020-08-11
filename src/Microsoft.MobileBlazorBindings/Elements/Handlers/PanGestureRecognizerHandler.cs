// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class PanGestureRecognizerHandler : GestureRecognizerHandler
    {
        public PanGestureRecognizerHandler(NativeComponentRenderer renderer, XF.PanGestureRecognizer PanGestureRecognizerControl) : base(renderer, PanGestureRecognizerControl)
        {
            PanGestureRecognizerControl = PanGestureRecognizerControl ?? throw new ArgumentNullException(nameof(PanGestureRecognizerControl));

            ConfigureEvent(
                eventName: "onpanupdated",
                setId: id => PanUpdatedEventHandlerId = id,
                clearId: id => { if (PanUpdatedEventHandlerId == id) { PanUpdatedEventHandlerId = 0; } });
            PanGestureRecognizerControl.PanUpdated += (s, e) =>
            {
                if (PanUpdatedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(PanUpdatedEventHandlerId, null, e));
                }
            };
        }

        public XF.PanGestureRecognizer PanGestureRecognizerControl { get; }

        public ulong PanUpdatedEventHandlerId { get; set; }

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
