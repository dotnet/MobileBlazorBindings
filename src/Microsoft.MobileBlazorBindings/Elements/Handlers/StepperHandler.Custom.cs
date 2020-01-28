// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class StepperHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            StepperControl.ValueChanged += (s, e) =>
            {
                if (ValueChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ValueChangedEventHandlerId, null, new ChangeEventArgs { Value = StepperControl.Value }));
                }
            };
        }

        public ulong ValueChangedEventHandlerId { get; set; }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "onvaluechanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => ValueChangedEventHandlerId = 0);
                    ValueChangedEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
