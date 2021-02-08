// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using IdentityModel.OidcClient;
using Microsoft.Extensions.Options;
using Microsoft.MobileBlazorBindings.Authentication.Services.Tizen;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// An implementation for <see cref="IAuthenticationService"/> that uses IdentityModel.OidcClient to authenticate the user.
    /// </summary>
    /// <typeparam name="TRemoteAuthenticationState">The state to preserve across authentication operations.</typeparam>
    /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount" />.</typeparam>
    /// <typeparam name="TProviderOptions">The options to be passed down to the underlying Authentication library handling the authentication operations.</typeparam>
    public class TizenAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions> :
        OidcAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>
        where TRemoteAuthenticationState : OidcAuthenticationState, new()
        where TProviderOptions : new()
        where TAccount : RemoteUserAccount
    {

        private const string LoopbackCallbackPath = "/authorize/";
        private const string DefaultClosePageResponse =
@"<html>
  <head><title>OAuth 2.0 Authentication Token Received</title></head>
  <body>
    <p style='font-size:30pt'>Received verification code. You may now close this window.</p>
  </body>
</html>";

        private string _redirectUri;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options">The options to be passed down to the underlying Authentication library handling the authentication operations.</param>
        /// <param name="tokenCache">The token cache to use to store tokens.</param>
        /// <param name="protectedStorage">The protect storage where refresh tokens will be stored.</param>
        /// <param name="accountClaimsPrincipalFactory">The <see cref="AccountClaimsPrincipalFactory{TAccount}"/> used to generate the <see cref="ClaimsPrincipal"/> for the user.</param>
        public TizenAuthenticationService(
            IOptionsSnapshot<RemoteAuthenticationOptions<TProviderOptions>> options,
            ITokenCache tokenCache,
            IProtectedStorage protectedStorage,
            AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory) : base(options, tokenCache, protectedStorage, accountClaimsPrincipalFactory)
        {
        }

        /// <inheritdoc />
        protected override async Task<OidcClient> CreateOidcClientFromOptions()
        {
            var result = await base.CreateOidcClientFromOptions();

            _redirectUri = new UriBuilder("http", "127.0.0.1", GetRandomUnusedPort(), LoopbackCallbackPath).ToString();
            result.Options.RedirectUri = _redirectUri;
            result.Options.PostLogoutRedirectUri = _redirectUri;
            return result;
        }

        /// <inheritdoc />
        protected override async Task<string> StartSecureNavigation(Uri startUrl, Uri redirectUrl)
        {
            if (startUrl is null)
            {
                throw new ArgumentNullException(nameof(startUrl));
            }

            using (var listener = StartListener(_redirectUri))
            using (var cts = new CancellationTokenSource(TimeSpan.FromMinutes(10)))
            using (var embeddingBrowser = new TizenEmbeddingBrowser(cts))
            {
                try
                {
                    embeddingBrowser.StartNavigate(startUrl);
                    return await GetResponseFromListener(listener, cts.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return string.Empty;
                }
                catch (Exception e)
                {
                    throw new NotSupportedException(
                        $"Failed to launch browser with \"{startUrl}\" for authorization. See inner exception for details.", e);
                }
            }
        }

        private static HttpListener StartListener(string prefix)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();
            return listener;
        }

        private static async Task<string> GetResponseFromListener(HttpListener listener, CancellationToken ct)
        {
            HttpListenerContext context;
            // Set up cancellation. HttpListener.GetContextAsync() doesn't accept a cancellation token,
            // the HttpListener needs to be stopped which immediately aborts the GetContextAsync() call.
            using (ct.Register(listener.Stop))
            {
                // Wait to get the authorization code response.
                try
                {
                    context = await listener.GetContextAsync().ConfigureAwait(false);
                }
                catch (Exception) when (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    // Next line will never be reached because cancellation will always have been requested in this catch block.
                    // But it's required to satisfy compiler.
                    throw new InvalidOperationException();
                }
            }
            NameValueCollection coll = context.Request.QueryString;

            // Write a "close" response.
            var bytes = Encoding.UTF8.GetBytes(DefaultClosePageResponse);
            context.Response.ContentLength64 = bytes.Length;
            context.Response.SendChunked = false;
            context.Response.KeepAlive = false;
            var output = context.Response.OutputStream;
            await output.WriteAsync(bytes, 0, bytes.Length, ct).ConfigureAwait(false);
            await output.FlushAsync(ct).ConfigureAwait(false);
            output.Close();
            context.Response.Close();

            return $"?{string.Join("&", coll.AllKeys.Select(x => $"{x}={coll[x]}"))}";
        }

        private static int GetRandomUnusedPort()
        {
            // Get a random unused port by starting a Tcp listener on port 0.
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
