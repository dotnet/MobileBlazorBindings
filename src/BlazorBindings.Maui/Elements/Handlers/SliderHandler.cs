// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class SliderHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "ondragcompleted",
                setId: id => DragCompletedEventHandlerId = id,
                clearId: id => { if (DragCompletedEventHandlerId == id) { DragCompletedEventHandlerId = 0; } });
            SliderControl.DragCompleted += (s, e) =>
            {
                if (DragCompletedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(DragCompletedEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "ondragstarted",
                setId: id => DragStartedEventHandlerId = id,
                clearId: id => { if (DragStartedEventHandlerId == id) { DragStartedEventHandlerId = 0; } });
            SliderControl.DragStarted += (s, e) =>
            {
                if (DragStartedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(DragStartedEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "onvaluechanged",
                setId: id => ValueChangedEventHandlerId = id,
                clearId: id => { if (ValueChangedEventHandlerId == id) { ValueChangedEventHandlerId = 0; } });
            SliderControl.ValueChanged += (s, e) =>
            {
                if (ValueChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ValueChangedEventHandlerId, null, new ChangeEventArgs { Value = SliderControl.Value }));
                }
            };
        }

        public ulong DragCompletedEventHandlerId { get; set; }
        public ulong DragStartedEventHandlerId { get; set; }
        public ulong ValueChangedEventHandlerId { get; set; }
    }
}
