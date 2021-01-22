// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public interface IBlazorErrorHandler
    {
        void HandleException(Exception exception);
    }
}
