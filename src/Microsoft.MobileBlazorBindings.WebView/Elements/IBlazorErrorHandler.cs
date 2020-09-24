// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Microsoft.MobileBlazorBindings.WebView.Elements
{
    public interface IBlazorErrorHandler
    {
        void HandleException(Exception exception);
    }
}
