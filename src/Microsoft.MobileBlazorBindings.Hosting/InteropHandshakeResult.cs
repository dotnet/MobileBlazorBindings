// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public class InteropHandshakeResult
    {
#pragma warning disable CA1056 // URI-like properties should not be strings
        public string BaseUri { get; }
        public string InitialUri { get; }
#pragma warning restore CA1056 // URI-like properties should not be strings

#pragma warning disable CA1054 // URI-like parameters should not be strings
        public InteropHandshakeResult(string baseUri, string initialUri)
#pragma warning restore CA1054 // URI-like parameters should not be strings
        {
            BaseUri = baseUri;
            InitialUri = initialUri;
        }
    }
}
