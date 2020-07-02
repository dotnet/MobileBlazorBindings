// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class CheckBoxHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
                eventName: "onischeckedchanged",
                setId: id => IsCheckedChangedEventHandlerId = id,
                clearId: id => { if (IsCheckedChangedEventHandlerId == id) { IsCheckedChangedEventHandlerId = 0; } });
            CheckBoxControl.CheckedChanged += (s, e) =>
            {
                if (IsCheckedChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsCheckedChangedEventHandlerId, null, new ChangeEventArgs { Value = e.Value }));
                }
            };
        }

        public ulong IsCheckedChangedEventHandlerId { get; set; }
    }
}
