// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
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
                    // InputViewControl.Text is used here instead of TextChangedEventArgs.NewTextValue to prevent infinite loops.
                    // See https://github.com/dotnet/MobileBlazorBindings/issues/430.
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = InputViewControl.Text }));
                }
            };
        }

        public ulong TextChangedEventHandlerId { get; set; }
    }
}
