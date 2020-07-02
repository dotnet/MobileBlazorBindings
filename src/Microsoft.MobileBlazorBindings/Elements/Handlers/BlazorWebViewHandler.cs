// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.WebView.Elements;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class BlazorWebViewHandler : ViewHandler
    {
        public BlazorWebViewHandler(NativeComponentRenderer renderer, MobileBlazorBindingsBlazorWebView control)
            : base(renderer, control)
        {
        }
    }
}
