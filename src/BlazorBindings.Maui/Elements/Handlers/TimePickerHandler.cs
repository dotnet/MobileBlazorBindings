// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class TimePickerHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "ontimechanged",
                setId: id => TimeChangedEventHandlerId = id,
                clearId: id => { if (TimeChangedEventHandlerId == id) { TimeChangedEventHandlerId = 0; } });
            TimePickerControl.PropertyChanged += (s, e) =>
            {
                // MC.TimePicker doesn't have a TimeSelected or TimeChanged event, so we use the generic PropertyChanged event instead
                if (e.PropertyName == nameof(TimePickerControl.Time))
                {
                    if (TimeChangedEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TimeChangedEventHandlerId, null, new ChangeEventArgs { Value = TimePickerControl.Time }));
                    }
                }
            };
        }

        public ulong TimeChangedEventHandlerId { get; set; }
    }
}
