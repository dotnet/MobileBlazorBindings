// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.MobileBlazorBindings.Core;

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
