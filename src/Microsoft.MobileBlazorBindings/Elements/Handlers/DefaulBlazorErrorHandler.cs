// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class DefaulBlazorErrorHandler : IBlazorErrorHandler
    {
        public void HandleException(Exception exception)
        {
            ErrorPageHelper.ShowExceptionPage(exception);
        }
    }
}
