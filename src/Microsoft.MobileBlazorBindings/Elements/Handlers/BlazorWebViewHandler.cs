// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using WVM = Microsoft.AspNetCore.Components.WebView.Maui;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class BlazorWebViewHandler : ViewHandler
    {
        public BlazorWebViewHandler(NativeComponentRenderer renderer, WVM.BlazorWebView control)
            : base(renderer, control)
        {
            NativeControl = control;
        }

        public WVM.BlazorWebView NativeControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(WVM.BlazorWebView.HostPage):
                    NativeControl.HostPage = (string)attributeValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}