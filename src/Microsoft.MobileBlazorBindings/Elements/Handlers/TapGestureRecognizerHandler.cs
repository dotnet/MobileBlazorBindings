// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class TapGestureRecognizerHandler : GestureRecognizerHandler
    {
        public TapGestureRecognizerHandler(NativeComponentRenderer renderer, XF.TapGestureRecognizer tapGestureRecognizerControl) : base(renderer, tapGestureRecognizerControl)
        {
            TapGestureRecognizerControl = tapGestureRecognizerControl ?? throw new ArgumentNullException(nameof(tapGestureRecognizerControl));
            tapGestureRecognizerControl.Tapped += (s, e) =>
            {
                if (TappedHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TappedHandlerId, null, e));
                }
            };
        }

        public XF.TapGestureRecognizer TapGestureRecognizerControl { get; }
        
        public ulong TappedHandlerId { get; set; }


        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(TapGestureRecognizer.NumberOfTapsRequired):
                    TapGestureRecognizerControl.NumberOfTapsRequired = AttributeHelper.GetInt(attributeValue);
                    break;
                case "ontapped":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => TappedHandlerId = 0);
                    TappedHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
