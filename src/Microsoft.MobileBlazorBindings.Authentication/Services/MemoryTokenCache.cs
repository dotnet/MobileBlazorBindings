// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// A memory token cache implementation.
    /// </summary>
    public class MemoryTokenCache : ITokenCache
    {
        private readonly ConcurrentDictionary<string, JwtSecurityToken> tokens = new ConcurrentDictionary<string, JwtSecurityToken>();

        /// <inheritdoc />
        public Task Add(string key, JwtSecurityToken token)
        {
            tokens[key] = token;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Clear()
        {
            tokens.Clear();
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<bool> TryGet(string key, out JwtSecurityToken token)
        {
            if (tokens.TryGetValue(key, out var foundToken))
            {
                if (foundToken.ValidTo < DateTime.UtcNow)
                {
                    tokens.TryRemove(key, out _);
                }
                else
                {
                    token = foundToken;
                    return Task.FromResult(true);
                }
            }

            token = null;
            return Task.FromResult(false);
        }
    }
}
