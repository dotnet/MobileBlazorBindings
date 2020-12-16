// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.MobileBlazorBindings.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for remote authentication services.
    /// </summary>
    public static class RemoteAuthenticationBuilderExtensions
    {
        /// <summary>
        /// Replaces the existing <see cref="AccountClaimsPrincipalFactory{TAccount}"/> with the user factory defined by <typeparamref name="TAccountClaimsPrincipalFactory"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The account type.</typeparam>
        /// <typeparam name="TAccountClaimsPrincipalFactory">The new user factory type.</typeparam>
        /// <param name="builder">The <see cref="IRemoteAuthenticationBuilder{TRemoteAuthenticationState, TAccount}"/>.</param>
        /// <returns>The <see cref="IRemoteAuthenticationBuilder{TRemoteAuthenticationState, TAccount}"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddAccountClaimsPrincipalFactory<TRemoteAuthenticationState, TAccount, TAccountClaimsPrincipalFactory>(
            this IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> builder)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
            where TAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<TAccount>
        {
            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            builder.Services.Replace(ServiceDescriptor.Scoped<AccountClaimsPrincipalFactory<TAccount>, TAccountClaimsPrincipalFactory>());

            return builder;
        }

        /// <summary>
        /// Replaces the existing <see cref="AccountClaimsPrincipalFactory{Account}"/> with the user factory defined by <typeparamref name="TAccountClaimsPrincipalFactory"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The remote authentication state.</typeparam>
        /// <typeparam name="TAccountClaimsPrincipalFactory">The new user factory type.</typeparam>
        /// <param name="builder">The <see cref="IRemoteAuthenticationBuilder{TRemoteAuthenticationState, Account}"/>.</param>
        /// <returns>The <see cref="IRemoteAuthenticationBuilder{TRemoteAuthenticationState, Account}"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddAccountClaimsPrincipalFactory<TRemoteAuthenticationState, TAccountClaimsPrincipalFactory>(
            this IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> builder)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount> => builder.AddAccountClaimsPrincipalFactory<TRemoteAuthenticationState, RemoteUserAccount, TAccountClaimsPrincipalFactory>();

        /// <summary>
        /// Replaces the existing <see cref="AccountClaimsPrincipalFactory{TAccount}"/> with the user factory defined by <typeparamref name="TAccountClaimsPrincipalFactory"/>.
        /// </summary>
        /// <typeparam name="TAccountClaimsPrincipalFactory">The new user factory type.</typeparam>
        /// <param name="builder">The <see cref="IRemoteAuthenticationBuilder{RemoteAuthenticationState, Account}"/>.</param>
        /// <returns>The <see cref="IRemoteAuthenticationBuilder{RemoteAuthenticationState, Account}"/>.</returns>
        public static IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> AddAccountClaimsPrincipalFactory<TAccountClaimsPrincipalFactory>(
            this IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> builder)
            where TAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount> => builder.AddAccountClaimsPrincipalFactory<OidcAuthenticationState, RemoteUserAccount, TAccountClaimsPrincipalFactory>();
    }
}
