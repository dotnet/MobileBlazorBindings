// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class VisualElementHandler : NavigableElementHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onfocused",
                setId: id => FocusedEventHandlerId = id,
                clearId: id => { if (FocusedEventHandlerId == id) { FocusedEventHandlerId = 0; } });
            VisualElementControl.Focused += (s, e) =>
            {
                if (FocusedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(FocusedEventHandlerId, null, e));
                }
            };
            ConfigureEvent(
                eventName: "onsizechanged",
                setId: id => SizeChangedEventHandlerId = id,
                clearId: id => { if (SizeChangedEventHandlerId == id) { SizeChangedEventHandlerId = 0; } });
            VisualElementControl.SizeChanged += (s, e) =>
            {
                if (SizeChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(SizeChangedEventHandlerId, null, e));
                }
            };
            ConfigureEvent(
                eventName: "onunfocused",
                setId: id => UnfocusedEventHandlerId = id,
                clearId: id => { if (UnfocusedEventHandlerId == id) { UnfocusedEventHandlerId = 0; } });
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
    }
}
