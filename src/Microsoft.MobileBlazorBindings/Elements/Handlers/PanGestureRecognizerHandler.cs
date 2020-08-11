// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class PanGestureRecognizerHandler : GestureRecognizerHandler
    {
        public PanGestureRecognizerHandler(NativeComponentRenderer renderer, XF.PanGestureRecognizer panGestureRecognizerControl) : base(renderer, panGestureRecognizerControl)
        {
            PanGestureRecognizerControl = panGestureRecognizerControl ?? throw new ArgumentNullException(nameof(panGestureRecognizerControl));

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
