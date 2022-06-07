// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class EditorHandler : InputViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "oncompleted",
                setId: id => CompletedEventHandlerId = id,
                clearId: id => { if (CompletedEventHandlerId == id) { CompletedEventHandlerId = 0; } });
            EditorControl.Completed += (s, e) =>
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
