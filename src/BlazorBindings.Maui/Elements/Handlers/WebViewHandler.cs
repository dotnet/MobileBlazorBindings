// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class WebViewHandler : ViewHandler
    {
        public MC.WebView Control { get; }

        private ulong _onWebMessageReceivedEventHandlerId;

        public WebViewHandler(NativeComponentRenderer renderer, MC.WebView control)
            : base(renderer, control)
        {
            Control = control;

            ConfigureEvent(
                eventName: "onwebmessagereceived",
                setId: id => _onWebMessageReceivedEventHandlerId = id,
                clearId: id => { if (_onWebMessageReceivedEventHandlerId == id) { _onWebMessageReceivedEventHandlerId = 0; } });

            // Is that still needed ?
            //Control.OnWebMessageReceived += (sender, message) =>
            //{
            //    if (_onWebMessageReceivedEventHandlerId != default)
            //    {
            //        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(_onWebMessageReceivedEventHandlerId, null, new WebView.WebMessageEventArgs { Message = message }));
            //    }
            //};
        }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.HtmlWebViewSource):
                    Control.Source = new MC.HtmlWebViewSource { Html = (string)attributeValue };
                    break;
                case nameof(MC.UrlWebViewSource):
                    Control.Source = new MC.UrlWebViewSource { Url = (string)attributeValue };
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
