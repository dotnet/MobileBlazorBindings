﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class WebViewHandler : ViewHandler
    {
        public WebViewExtended Control { get; }

        private ulong _onWebMessageReceivedEventHandlerId;
        private ulong _onNavigationStartingEventHandlerId;
        private ulong _onNavigationFinishedEventHandlerId;

        public WebViewHandler(NativeComponentRenderer renderer, WebViewExtended control)
            : base(renderer, control)
        {
            Control = control;

            ConfigureEvent(
                eventName: "onwebmessagereceived",
                setId: id => _onWebMessageReceivedEventHandlerId = id,
                clearId: id => { if (_onWebMessageReceivedEventHandlerId == id) { _onWebMessageReceivedEventHandlerId = 0; } });

            Control.OnWebMessageReceived += (sender, message) =>
            {
                if (_onWebMessageReceivedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(_onWebMessageReceivedEventHandlerId, null, new WebView.WebMessageEventArgs { Message = message }));
                }
            };

            ConfigureEvent(
                eventName: "onnavigationstarting",
                setId: id => _onNavigationStartingEventHandlerId = id,
                clearId: id => { if (_onNavigationStartingEventHandlerId == id) { _onNavigationStartingEventHandlerId = 0; } });

            Control.OnNavigationStarting += (sender, uri) =>
            {
                if (_onNavigationStartingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(_onNavigationStartingEventHandlerId, null, new WebView.NavigationEventArgs() { Uri = uri }));
                }
            };

            ConfigureEvent(
                eventName: "onnavigationfinished",
                setId: id => _onNavigationFinishedEventHandlerId = id,
                clearId: id => { if (_onNavigationFinishedEventHandlerId == id) { _onNavigationFinishedEventHandlerId = 0; } });

            Control.OnNavigationFinished += (sender, uri) =>
            {
                if (_onNavigationFinishedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(_onNavigationFinishedEventHandlerId, null, new WebView.NavigationEventArgs() { Uri = uri }));
                }
            };
            
        }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.HtmlWebViewSource):
                    Control.Source = new XF.HtmlWebViewSource { Html = (string)attributeValue };
                    break;
                case nameof(XF.UrlWebViewSource):
                    Control.Source = new XF.UrlWebViewSource { Url = (string)attributeValue };
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
