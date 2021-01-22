// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public abstract class IPCBase
    {
        public abstract void Send(string eventName, params object[] args);
    }
}
