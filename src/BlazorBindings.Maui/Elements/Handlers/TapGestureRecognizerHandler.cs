// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class TapGestureRecognizerHandler : GestureRecognizerHandler
    {
        public TapGestureRecognizerHandler(NativeComponentRenderer renderer, MC.TapGestureRecognizer tapGestureRecognizerControl) : base(renderer, tapGestureRecognizerControl)
        {
            TapGestureRecognizerControl = tapGestureRecognizerControl ?? throw new ArgumentNullException(nameof(tapGestureRecognizerControl));

            ConfigureEvent(
                eventName: "ontapped",
                setId: id => TappedEventHandlerId = id,
                clearId: id => { if (TappedEventHandlerId == id) { TappedEventHandlerId = 0; } });
            TapGestureRecognizerControl.Tapped += (s, e) =>
            {
                if (TappedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TappedEventHandlerId, null, e));
                }
            };
        }

        public MC.TapGestureRecognizer TapGestureRecognizerControl { get; }

        public ulong TappedEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(TapGestureRecognizer.NumberOfTapsRequired):
                    TapGestureRecognizerControl.NumberOfTapsRequired = AttributeHelper.GetInt(attributeValue, 1);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
