// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class DatePickerHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "ondatechanged",
                setId: id => DateChangedEventHandlerId = id,
                clearId: id => { if (DateChangedEventHandlerId == id) { DateChangedEventHandlerId = 0; } });
            DatePickerControl.DateSelected += (s, e) =>
            {
                if (DateChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(DateChangedEventHandlerId, null, new ChangeEventArgs { Value = DatePickerControl.Date }));
                }
            };
        }

        public ulong DateChangedEventHandlerId { get; set; }
    }
}
