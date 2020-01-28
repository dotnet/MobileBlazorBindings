// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class StepperHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
                eventName: "onvaluechanged",
                setId: id => ValueChangedEventHandlerId = id,
                clearId: () => ValueChangedEventHandlerId = 0);
            StepperControl.ValueChanged += (s, e) =>
            {
                if (ValueChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ValueChangedEventHandlerId, null, new ChangeEventArgs { Value = StepperControl.Value }));
                }
            };
        }

        public ulong ValueChangedEventHandlerId { get; set; }
    }
}
