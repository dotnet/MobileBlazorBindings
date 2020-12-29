// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Interface that defines a token cache.
    /// </summary>
    public interface ITokenCache
    {
        /// <summary>
        /// Adds a token to the cache using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task Add(string key, JwtSecurityToken token);

        /// <summary>
        /// Tries to get the token from the cache using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<bool> TryGet(string key, out JwtSecurityToken token);

        /// <summary>
        /// Clears the token cache.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Clear();
    }
}
