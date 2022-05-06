// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ScrollViewHandler : IMauiContainerElementHandler
    {
        public void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as MC.View;
            ScrollViewControl.Content = childAsView;
        }

        public void RemoveChild(MC.Element child)
        {
            if (ScrollViewControl.Content == child)
            {
                ScrollViewControl.Content = null;
            }
        }

        public int GetChildIndex(MC.Element child)
        {
            return ScrollViewControl.Content == child ? 0 : -1;
        }

        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onscrolled",
                setId: id => ScrolledEventHandlerId = id,
                clearId: id => { if (ScrolledEventHandlerId == id) { ScrolledEventHandlerId = 0; } });
            ScrollViewControl.Scrolled += (s, e) =>
            {
                if (ScrolledEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ScrolledEventHandlerId, null, e));
                }
            };
        }

        public ulong ScrolledEventHandlerId { get; set; }
    }
}
