// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class SwipeGestureRecognizerHandler : GestureRecognizerHandler
    {
        public SwipeGestureRecognizerHandler(NativeComponentRenderer renderer, XF.SwipeGestureRecognizer swipeGestureRecognizerControl) : base(renderer, swipeGestureRecognizerControl)
        {
            SwipeGestureRecognizerControl = swipeGestureRecognizerControl ?? throw new ArgumentNullException(nameof(swipeGestureRecognizerControl));

            ConfigureEvent(
                eventName: "onswiped",
                setId: id => SwipedEventHandlerId = id,
                clearId: id => { if (SwipedEventHandlerId == id) { SwipedEventHandlerId = 0; } });
            SwipeGestureRecognizerControl.Swiped += (s, e) =>
            {
                if (SwipedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SwipedEventHandlerId, null, e));
                }
            };
        }

        public XF.SwipeGestureRecognizer SwipeGestureRecognizerControl { get; }

        public ulong SwipedEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(SwipeGestureRecognizer.Direction):
                    SwipeGestureRecognizerControl.Direction = (XF.SwipeDirection)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(SwipeGestureRecognizer.Threshold):
                    SwipeGestureRecognizerControl.Threshold = AttributeHelper.StringToUInt32((string)attributeValue, 100u);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
