// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class VisualElementHandler : NavigableElementHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            VisualElementControl.Focused += (s, e) =>
            {
                if (FocusedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(FocusedEventHandlerId, null, e));
                }
            };
            VisualElementControl.SizeChanged += (s, e) =>
            {
                if (SizeChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SizeChangedEventHandlerId, null, e));
                }
            };
            VisualElementControl.Unfocused += (s, e) =>
            {
                if (UnfocusedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(UnfocusedEventHandlerId, null, e));
                }
            };
        }

        public ulong FocusedEventHandlerId { get; set; }
        public ulong SizeChangedEventHandlerId { get; set; }
        public ulong UnfocusedEventHandlerId { get; set; }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "onfocused":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => FocusedEventHandlerId = 0);
                    FocusedEventHandlerId = attributeEventHandlerId;
                    break;
                case "onsizechanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => SizeChangedEventHandlerId = 0);
                    SizeChangedEventHandlerId = attributeEventHandlerId;
                    break;
                case "onunfocused":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => UnfocusedEventHandlerId = 0);
                    UnfocusedEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
