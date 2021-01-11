// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ScrollViewHandler : LayoutHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as XF.View;
            ScrollViewControl.Content = childAsView;
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

        public override void RemoveChild(XF.Element child)
        {
            if (ScrollViewControl.Content == child)
            {
                ScrollViewControl.Content = null;
            }
        }
    }
}
