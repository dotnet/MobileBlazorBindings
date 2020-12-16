// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Microsoft.MobileBlazorBindings.Authentication.Msal")]

namespace Microsoft.MobileBlazorBindings.Authentication.Internal
{
    internal class AccessTokenProviderAccessor : IAccessTokenProviderAccessor
    {
        private readonly IServiceProvider _provider;
        private IAccessTokenProvider _tokenProvider;

        public AccessTokenProviderAccessor(IServiceProvider provider) => _provider = provider;

        public IAccessTokenProvider TokenProvider => _tokenProvider ??= _provider.GetRequiredService<IAccessTokenProvider>();
    }
}
