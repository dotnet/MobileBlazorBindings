// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SwitchHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            SwitchControl.Toggled += (s, e) =>
            {
                if (IsToggledChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsToggledChangedEventHandlerId, null, new ChangeEventArgs { Value = SwitchControl.IsToggled }));
                }
            };
        }

        public ulong IsToggledChangedEventHandlerId { get; set; }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "onistoggledchanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => IsToggledChangedEventHandlerId = 0);
                    IsToggledChangedEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
