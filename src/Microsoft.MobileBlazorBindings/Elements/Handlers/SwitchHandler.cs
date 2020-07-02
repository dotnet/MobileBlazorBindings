// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SwitchHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
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
