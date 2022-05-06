// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ButtonHandler : ViewHandler, IHandleChildContentText
    {
        private readonly TextSpanContainer _textSpanContainer = new TextSpanContainer();

        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onclick",
                setId: id => ClickEventHandlerId = id,
                clearId: id => { if (ClickEventHandlerId == id) { ClickEventHandlerId = 0; } });
            ButtonControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "onpress",
                setId: id => PressEventHandlerId = id,
                clearId: id => { if (PressEventHandlerId == id) { PressEventHandlerId = 0; } });
            ButtonControl.Pressed += (s, e) =>
            {
                if (PressEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(PressEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "onrelease",
                setId: id => ReleaseEventHandlerId = id,
                clearId: id => { if (ReleaseEventHandlerId == id) { ReleaseEventHandlerId = 0; } });
            ButtonControl.Released += (s, e) =>
            {
                if (ReleaseEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ReleaseEventHandlerId, null, e));
                }
            };
        }

        public void HandleText(int index, string text)
        {
            ButtonControl.Text = _textSpanContainer.GetUpdatedText(index, text);
        }

        public ulong ClickEventHandlerId { get; set; }
        public ulong PressEventHandlerId { get; set; }
        public ulong ReleaseEventHandlerId { get; set; }
    }
}
