// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public class InteropHandshakeResult
    {
        public string BaseUri { get; }
        public string InitialUri { get; }

        public InteropHandshakeResult(string baseUri, string initialUri)
        {
            BaseUri = baseUri;
            InitialUri = initialUri;
        }
    }
}
