// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.WebView.Elements
{
    public delegate Stream ResolveWebResourceDelegate(string url, out string contentType);

    public class WebViewExtended : XF.WebView
    {
        public EventHandler<string> OnWebMessageReceived { get; set; }
        public EventHandler<string> SendMessageFromJSToDotNetRequested { get; set; }
        public IDictionary<string, ResolveWebResourceDelegate> SchemeHandlers { get; }
            = new Dictionary<string, ResolveWebResourceDelegate>();

        // We can't destroy and recreate WebView instances because they hold nontrivial state
        // The ability for XF.Element to retain renderers would be a good framework feature
        public object RetainedNativeControl { get; set; }

        public void HandleWebMessageReceived(string webMessageAsString)
        {
            OnWebMessageReceived?.Invoke(this, webMessageAsString);
        }

        public void SendMessage(string message)
        {
            SendMessageFromJSToDotNetRequested?.Invoke(this, message);
        }
    }
}
