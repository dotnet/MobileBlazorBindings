// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.MobileBlazorBindings.Authentication;
using Microsoft.MobileBlazorBindings.Authentication.Internal;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to add authentication to Mobile Blazor Bindings applications.
    /// </summary>
    public static class MobileBlazorBindingsAuthenticationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds support for authentication for SPA applications using the given <typeparamref name="TProviderOptions"/> and
        /// <typeparamref name="TRemoteAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The state to be persisted across authentication operations.</typeparam>
        /// <typeparam name="TAccount">The account type.</typeparam>
        /// <typeparam name="TProviderOptions">The configuration options of the underlying provider being used for handling the authentication operations.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddRemoteAuthentication<TRemoteAuthenticationState, TAccount, TProviderOptions>(this IServiceCollection services)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
            where TProviderOptions : class, new()
        {
            services.AddOptions();
            services.AddAuthorizationCore();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.macOS:
                    services.TryAddScoped<AuthenticationStateProvider, AppleAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>>();
                    break;
                case Device.Android:
                    services.TryAddScoped<AuthenticationStateProvider, AndroidAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>>();
                    break;
                case Device.WPF:
                    services.TryAddScoped<AuthenticationStateProvider, WindowsAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>>();
                    break;
                case Device.Tizen:
                    services.TryAddScoped<AuthenticationStateProvider, TizenAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>>();
                    break;
                default:
                    throw new PlatformNotSupportedException($"Platform {Device.RuntimePlatform} is not supported by {ThisAssembly.AssemblyName}");
            }

            services.TryAddScoped(sp =>
            {
                return (IAuthenticationService)sp.GetRequiredService<AuthenticationStateProvider>();
            });

            services.TryAddTransient<AuthorizationMessageHandler>();

            services.TryAddScoped(sp =>
            {
                return (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>();
            });

            services.TryAddScoped<IRemoteAuthenticationPathsProvider, DefaultRemoteApplicationPathsProvider<TProviderOptions>>();
            services.TryAddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>();
            services.TryAddScoped<ITokenCache, MemoryTokenCache>();

            services.TryAddScoped<AccountClaimsPrincipalFactory<TAccount>>();

            return new RemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount>(services);
        }
        /// <summary>
        /// Adds support for authentication for SPA applications using the given <typeparamref name="TProviderOptions"/> and
        /// <typeparamref name="TRemoteAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The state to be persisted across authentication operations.</typeparam>
        /// <typeparam name="TAccount">The account type.</typeparam>
        /// <typeparam name="TProviderOptions">The configuration options of the underlying provider being used for handling the authentication operations.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{TProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddRemoteAuthentication<TRemoteAuthenticationState, TAccount, TProviderOptions>(this IServiceCollection services, Action<RemoteAuthenticationOptions<TProviderOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
            where TProviderOptions : class, new()
        {
            services.AddRemoteAuthentication<TRemoteAuthenticationState, TAccount, TProviderOptions>();
            if (configure != null)
            {
                services.Configure(configure);
            }

            return new RemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount>(services);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="OidcProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{TProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> AddOidcAuthentication(this IServiceCollection services, Action<RemoteAuthenticationOptions<OidcProviderOptions>> configure)
        {
            return AddOidcAuthentication<OidcAuthenticationState>(services, configure);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="OidcProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{TProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddOidcAuthentication<TRemoteAuthenticationState>(this IServiceCollection services, Action<RemoteAuthenticationOptions<OidcProviderOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
        {
            return AddOidcAuthentication<TRemoteAuthenticationState, RemoteUserAccount>(services, configure);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="OidcProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The account type.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{TProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddOidcAuthentication<TRemoteAuthenticationState, TAccount>(this IServiceCollection services, Action<RemoteAuthenticationOptions<OidcProviderOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
        {
            services.TryAddEnumerable(ServiceDescriptor.Scoped<IPostConfigureOptions<RemoteAuthenticationOptions<OidcProviderOptions>>, DefaultOidcOptionsConfiguration>(sp => new DefaultOidcOptionsConfiguration(AuthenticationBaseUri)));

            return AddRemoteAuthentication<TRemoteAuthenticationState, TAccount, OidcProviderOptions>(services, configure);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="ApiAuthorizationProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="apiBaseUrl">The base URL for the api.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> AddApiAuthorization(this IServiceCollection services, string apiBaseUrl)
        {
            return AddApiAuthorizationCore<OidcAuthenticationState, RemoteUserAccount>(services, configure: null, apiBaseUrl, Assembly.GetCallingAssembly().GetName().Name);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="ApiAuthorizationProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="apiBaseUrl">The base URL for the api.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddApiAuthorization<TRemoteAuthenticationState>(this IServiceCollection services, string apiBaseUrl)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
        {
            return AddApiAuthorizationCore<TRemoteAuthenticationState, RemoteUserAccount>(services, configure: null, apiBaseUrl, Assembly.GetCallingAssembly().GetName().Name);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="ApiAuthorizationProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The account type.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="apiBaseUrl">The base URL for the api.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddApiAuthorization<TRemoteAuthenticationState, TAccount>(this IServiceCollection services, string apiBaseUrl)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
        {
            return AddApiAuthorizationCore<TRemoteAuthenticationState, TAccount>(services, configure: null, apiBaseUrl, Assembly.GetCallingAssembly().GetName().Name);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="ApiAuthorizationProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{ApiAuthorizationProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<OidcAuthenticationState, RemoteUserAccount> AddApiAuthorization(this IServiceCollection services, Action<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> configure)
        {
            return AddApiAuthorizationCore<OidcAuthenticationState, RemoteUserAccount>(services, configure, null, Assembly.GetCallingAssembly().GetName().Name);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="ApiAuthorizationProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{ApiAuthorizationProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddApiAuthorization<TRemoteAuthenticationState>(this IServiceCollection services, Action<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
        {
            return AddApiAuthorizationCore<TRemoteAuthenticationState, RemoteUserAccount>(services, configure, null, Assembly.GetCallingAssembly().GetName().Name);
        }

        /// <summary>
        /// Adds support for authentication for SPA applications using <see cref="ApiAuthorizationProviderOptions"/> and the <see cref="OidcAuthenticationState"/>.
        /// </summary>
        /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
        /// <typeparam name="TAccount">The account type.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">An action that will configure the <see cref="RemoteAuthenticationOptions{ApiAuthorizationProviderOptions}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> where the services were registered.</returns>
        public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddApiAuthorization<TRemoteAuthenticationState, TAccount>(this IServiceCollection services, Action<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> configure)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
        {
            return AddApiAuthorizationCore<TRemoteAuthenticationState, TAccount>(services, configure, null, Assembly.GetCallingAssembly().GetName().Name);
        }

        private static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddApiAuthorizationCore<TRemoteAuthenticationState, TAccount>(
            IServiceCollection services,
            Action<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> configure,
            string apiBaseUrl,
            string inferredClientId)
            where TRemoteAuthenticationState : OidcAuthenticationState, new()
            where TAccount : RemoteUserAccount
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Scoped<IPostConfigureOptions<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>>, DefaultApiAuthorizationOptionsConfiguration>(_ =>
                new DefaultApiAuthorizationOptionsConfiguration(apiBaseUrl, inferredClientId)));

            services.AddRemoteAuthentication<TRemoteAuthenticationState, TAccount, ApiAuthorizationProviderOptions>(configure);

            return new RemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount>(services);
        }

        /// <summary>
        /// Base URI for authentication
        /// </summary>
        private static string AuthenticationBaseUri => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "http://localhost:0/" : "app://0.0.0.0/";
    }
}
