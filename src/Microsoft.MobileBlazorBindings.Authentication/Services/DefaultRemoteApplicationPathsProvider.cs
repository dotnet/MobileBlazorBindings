// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.MobileBlazorBindings.Authentication.Internal;
using Microsoft.Extensions.Options;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    internal class DefaultRemoteApplicationPathsProvider<TProviderOptions> : IRemoteAuthenticationPathsProvider where TProviderOptions : class, new()
    {
        private readonly IOptions<RemoteAuthenticationOptions<TProviderOptions>> _options;

        public DefaultRemoteApplicationPathsProvider(IOptions<RemoteAuthenticationOptions<TProviderOptions>> options)
        {
            _options = options;
        }

        public RemoteAuthenticationApplicationPathsOptions ApplicationPaths => _options.Value.AuthenticationPaths;
    }
}
