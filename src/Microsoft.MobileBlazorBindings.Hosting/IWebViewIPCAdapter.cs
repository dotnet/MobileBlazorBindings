// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public interface IWebViewIPCAdapter
    {
        EventHandler<string> OnWebMessageReceived { get; set; }
        void SendMessage(string message);
        void BeginInvoke(Action action);


        // [JSInvokable] calls
        Task DispatchEvent(WebEventDescriptor eventDescriptor, string eventArgsJson);
#pragma warning disable CA1054 // URI-like parameters should not be strings
        void NotifyLocationChanged(string uri, bool isInterceptedLink);
#pragma warning restore CA1054 // URI-like parameters should not be strings
        Task OnRenderCompleted(long renderId, string errorMessageOrNull);
    }
}
