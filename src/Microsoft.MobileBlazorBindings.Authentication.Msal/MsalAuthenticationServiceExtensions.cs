// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.MobileBlazorBindings.Authentication;
using Microsoft.MobileBlazorBindings.Authentication.Internal;
using Microsoft.MobileBlazorBindings.Authentication.Msal;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to add MSAL authentication to Mobile Blazor Bindings applications.
    /// </summary>
    public static class MsalAuthenticationServiceExtensions
    {
        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configure">A callback to configure the <see cref="RemoteAuthenticationOptions{MsalProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> AddMsalAuthentication(this IServiceCollection services, Action<RemoteAuthenticationOptions<PublicClientApplicationOptions>> configure)
        {
            return AddMsalAuthentication<OidcAuthenticationState>(services, configure);
        }

        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configure">A callback to configure the <see cref="RemoteAuthenticationOptions{MsalProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddMsalAuthentication<TRemoteAuthenticationState>(this IServiceCollection services, Action<RemoteAuthenticationOptions<PublicClientApplicationOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
        {
            return AddMsalAuthentication<TRemoteAuthenticationState, RemoteUserAccount>(services, configure);
        }

        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount"/>.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configure">A callback to configure the <see cref="RemoteAuthenticationOptions{MsalProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddMsalAuthentication<TRemoteAuthenticationState, TAccount>(this IServiceCollection services, Action<RemoteAuthenticationOptions<PublicClientApplicationOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount, new()
        {
            var builder = AddMsalAuthentication<TRemoteAuthenticationState, TAccount>(services);

            if (configure != null)
            {
                services.Configure(configure);
            }

            return builder;
        }

        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount"/>.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddMsalAuthentication<TRemoteAuthenticationState, TAccount>(this IServiceCollection services)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount, new()
        {
            services.AddOptions();
            services.AddAuthorizationCore();
            services.TryAddScoped<AuthenticationStateProvider, MsalAuthenticationService<TAccount, PublicClientApplicationOptions>>();
            return RegisterDependencies<TRemoteAuthenticationState, TAccount>(services);
        }

        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="create">A callback to create the configured <see cref="PublicClientApplication"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> AddMsalAuthentication(this IServiceCollection services, Func<IServiceProvider, IPublicClientApplication> create)
        {
            return AddMsalAuthentication<OidcAuthenticationState>(services, create);
        }

        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="create">A callback to create the configured <see cref="PublicClientApplication"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddMsalAuthentication<TRemoteAuthenticationState>(this IServiceCollection services, Func<IServiceProvider, IPublicClientApplication> create)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
        {
            return AddMsalAuthentication<TRemoteAuthenticationState, RemoteUserAccount>(services, create);
        }

        /// <summary>
        /// Adds authentication using msal.js to Blazor applications.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount"/>.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="create">A callback to create the configured <see cref="PublicClientApplication"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddMsalAuthentication<TRemoteAuthenticationState, TAccount>(this IServiceCollection services, Func<IServiceProvider, IPublicClientApplication> create)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount, new()
        {
            services.AddOptions();
            services.AddAuthorizationCore();
            services.TryAddScoped<AuthenticationStateProvider>(
                sp => new MsalAuthenticationService<TAccount, PublicClientApplicationOptions>(
                    (PublicClientApplication)create(sp),
                    sp.GetRequiredService<IProtectedStorage>(),
                    sp.GetRequiredService<AccountClaimsPrincipalFactory<TAccount>>()));
            return RegisterDependencies<TRemoteAuthenticationState, TAccount>(services);
        }


        private static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> RegisterDependencies<TRemoteAuthenticationState, TAccount>(IServiceCollection services)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
        {
            services.TryAddScoped(sp =>
            {
                return (IAuthenticationService)sp.GetRequiredService<AuthenticationStateProvider>();
            });

            services.TryAddTransient<AuthorizationMessageHandler>();

            services.TryAddScoped<IRemoteAuthenticationPathsProvider, DefaultRemoteApplicationPathsProvider<PublicClientApplicationOptions>>();

            services.TryAddScoped(sp =>
            {
                return (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>();
            });

            services.TryAddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>();

            services.TryAddScoped<AccountClaimsPrincipalFactory<TAccount>>();

            services.TryAddEnumerable(ServiceDescriptor.Scoped<IPostConfigureOptions<RemoteAuthenticationOptions<PublicClientApplicationOptions>>, MsalDefaultOptionsConfiguration>());

            return new MsalRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount>(services);
        }

        internal class MsalRemoteAuthenticationBuilder<TRemoteAuthenticationState, TRemoteUserAccount> : IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TRemoteUserAccount>
                where TRemoteAuthenticationState : OidcAuthenticationState, new()
                where TRemoteUserAccount : RemoteUserAccount
        {
            public MsalRemoteAuthenticationBuilder(IServiceCollection services) => Services = services;

            public IServiceCollection Services { get; }
        }
    }
}
