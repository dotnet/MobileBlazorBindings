// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class SwitchHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onistoggledchanged",
                setId: id => IsToggledChangedEventHandlerId = id,
                clearId: id => { if (IsToggledChangedEventHandlerId == id) { IsToggledChangedEventHandlerId = 0; } });
            SwitchControl.Toggled += (s, e) =>
            {
                if (IsToggledChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsToggledChangedEventHandlerId, null, new ChangeEventArgs { Value = SwitchControl.IsToggled }));
                }
            };
        }

        public ulong IsToggledChangedEventHandlerId { get; set; }
    }
}
