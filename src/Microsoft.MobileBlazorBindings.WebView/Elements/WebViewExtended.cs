// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.WebView.Elements
{
    public delegate Stream ResolveWebResourceDelegate(string url, out string contentType);

    public class WebViewExtended : XF.WebView
    {
        public WebViewExtended(IBlazorErrorHandler errorHandler)
        {
            ErrorHandler = errorHandler;
        }

        public EventHandler<string> OnWebMessageReceived { get; set; }
        public EventHandler<string> SendMessageFromJSToDotNetRequested { get; set; }

        // Unfortunately since the orginal Navigating and Navigated event invoke methods are internal
        // the events cannot be invoked and we have to duplicate those.
        public EventHandler<Uri> OnNavigationStarting { get; set; }

        public EventHandler<Uri> OnNavigationFinished { get; set; }

        public IDictionary<string, ResolveWebResourceDelegate> SchemeHandlers { get; }
            = new Dictionary<string, ResolveWebResourceDelegate>();

        public IBlazorErrorHandler ErrorHandler { get; }

        public void HandleWebMessageReceived(string webMessageAsString)
        {
            OnWebMessageReceived?.Invoke(this, webMessageAsString);
        }

        public void SendMessage(string message)
        {
            SendMessageFromJSToDotNetRequested?.Invoke(this, message);
        }

        public void HandleNavigationStarting(Uri url)
        {
            OnNavigationStarting?.Invoke(this, url);
        }

        public void HandleNavigationFinished(Uri url)
        {
            OnNavigationFinished?.Invoke(this, url);
        }
    }
}
