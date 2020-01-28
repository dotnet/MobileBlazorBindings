// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class EntryHandler : InputViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            EntryControl.Completed += (s, e) =>
            {
                if (CompletedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(CompletedEventHandlerId, null, e));
                }
            };
            EntryControl.TextChanged += (s, e) =>
            {
                if (TextChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = EntryControl.Text }));
                }
            };
        }

        public ulong CompletedEventHandlerId { get; set; }
        public ulong TextChangedEventHandlerId { get; set; }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "oncompleted":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => CompletedEventHandlerId = 0);
                    CompletedEventHandlerId = attributeEventHandlerId;
                    break;
                case "ontextchanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => TextChangedEventHandlerId = 0);
                    TextChangedEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
