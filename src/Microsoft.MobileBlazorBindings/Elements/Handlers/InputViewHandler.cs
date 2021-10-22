// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class InputViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "ontextchanged",
                setId: id => TextChangedEventHandlerId = id,
                clearId: id => { if (TextChangedEventHandlerId == id) { TextChangedEventHandlerId = 0; } });
            InputViewControl.TextChanged += (s, e) =>
            {
                if (TextChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = e.NewTextValue }));
                }
            };
        }

        public ulong TextChangedEventHandlerId { get; set; }
    }
}
