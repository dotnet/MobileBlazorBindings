// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SliderHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
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

            RegisterEvent(
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

            RegisterEvent(
                eventName: "onvaluechanged",
                setId: id => ValueChangedEventHandlerId = id,
                clearId: id => { if (ValueChangedEventHandlerId == id) { ValueChangedEventHandlerId = 0; } });
            SliderControl.ValueChanged += (s, e) =>
            {
                if (ValueChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ValueChangedEventHandlerId, null, new ChangeEventArgs { Value = e.NewValue }));
                }
            };
        }

        public ulong DragCompletedEventHandlerId { get; set; }
        public ulong DragStartedEventHandlerId { get; set; }
        public ulong ValueChangedEventHandlerId { get; set; }
    }
}
