// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class EntryHandler : InputViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "oncompleted",
                setId: id => CompletedEventHandlerId = id,
                clearId: id => { if (CompletedEventHandlerId == id) { CompletedEventHandlerId = 0; } });
            EntryControl.Completed += (s, e) =>
            {
                if (CompletedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(CompletedEventHandlerId, null, e));
                }
            };
        }

        public ulong CompletedEventHandlerId { get; set; }
    }
}
