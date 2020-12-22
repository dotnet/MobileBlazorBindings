// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using IdentityModel.OidcClient;
using Microsoft.Extensions.Options;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
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
    public class WindowsAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions> :
        OidcAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>
        where TRemoteAuthenticationState : OidcAuthenticationState, new()
        where TProviderOptions : new()
        where TAccount : RemoteUserAccount
    {
        /// <summary>
        /// Authentication operation timeout (in seconds) before we cancel it.
        /// </summary>
        private const int OperationTimeout = 300;

        /// <summary>
        /// The Http Listener that we will use to listen to requests.
        /// </summary>
        private HttpListener _httpListener;

        /// <summary>
        /// The prefix for the listener.
        /// </summary>
        private string _prefix;

        /// <summary>
        /// The token source that can cancel a timed-out or previous authentication operation.
        /// </summary>
        private CancellationTokenSource _cancelationTokenSource;

        /// <summary>
        /// The previous task.
        /// </summary>
        private Task _previousTask;

        /// <summary>
        /// Whether this Authentication service is disposing.
        /// </summary>
        private bool _disposing;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options">The options to be passed down to the underlying Authentication library handling the authentication operations.</param>
        /// <param name="tokenCache">The token cache to use to store tokens.</param>
        /// <param name="protectedStorage">The protect storage where refresh tokens will be stored.</param>
        /// <param name="accountClaimsPrincipalFactory">The <see cref="AccountClaimsPrincipalFactory{TAccount}"/> used to generate the <see cref="ClaimsPrincipal"/> for the user.</param>
        public WindowsAuthenticationService(
            IOptionsSnapshot<RemoteAuthenticationOptions<TProviderOptions>> options,
            ITokenCache tokenCache,
            IProtectedStorage protectedStorage,
            AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory) : base(options, tokenCache, protectedStorage, accountClaimsPrincipalFactory)
        {
        }

        /// <inheritdoc/>
        protected override async Task<OidcClient> CreateOidcClientFromOptions()
        {
            var result = await base.CreateOidcClientFromOptions();

            var redirectUri = new UriBuilder(result.Options.RedirectUri);
            var postLogoutRedirectUri = new UriBuilder(result.Options.PostLogoutRedirectUri);

            if (redirectUri.Port != postLogoutRedirectUri.Port)
            {
                throw new OptionsValidationException(nameof(OidcProviderOptions.RedirectUri), typeof(OidcProviderOptions), new string[]
                    {
                        $"The port of the {nameof(OidcProviderOptions.RedirectUri)} must be equal to the port of the {nameof(OidcProviderOptions.PostLogoutRedirectUri)}.",
                    });
            }

            if (
                (redirectUri.Port == 0 || redirectUri.Port == 80) && (redirectUri.Host == "localhost" || redirectUri.Host == "127.0.0.1") &&
                (postLogoutRedirectUri.Port == 0 || postLogoutRedirectUri.Port == 80) && (postLogoutRedirectUri.Host == "localhost" || postLogoutRedirectUri.Host == "127.0.0.1"))
            {
                var port = GetRandomUnusedPort();

                // set ports to the random port value.
                redirectUri.Port = port;
                postLogoutRedirectUri.Port = port;
                result.Options.RedirectUri = redirectUri.ToString();
                result.Options.PostLogoutRedirectUri = postLogoutRedirectUri.ToString();
            }

            // create a listener and add the prefix to reserve the port.
            _prefix = $"{redirectUri.Scheme}://{redirectUri.Host}:{redirectUri.Port}/";
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(_prefix);

            return result;
        }

        /// <inheritdoc />
        protected override async Task<string> StartSecureNavigation(Uri startUrl, Uri redirectUrl)
        {
            if (startUrl is null)
            {
                throw new ArgumentNullException(nameof(startUrl));
            }

            if (redirectUrl is null)
            {
                throw new ArgumentNullException(nameof(redirectUrl));
            }

            // cancel any previous authentication attempt.
            if (_cancelationTokenSource != null && !_cancelationTokenSource.IsCancellationRequested)
            {
                _cancelationTokenSource.Cancel();
                _cancelationTokenSource.Dispose();
                if (_previousTask != null)
                {
                    await _previousTask;
                }
            }

            // have the authentication time out after a certain period.
            _cancelationTokenSource = new CancellationTokenSource(OperationTimeout * 1000);
            var cancellationToken = _cancelationTokenSource.Token;

            _httpListener.Start();

            // Opens request in the browser.
            var ps = new ProcessStartInfo(startUrl.ToString())
            {
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(ps);

            var resultUri = string.Empty;

            // a method to restart the listener as start stop has a bug in .NET core 3.1 LTS.
            // It's fixed in net5, but we aren't there yet.
            void RestartListener()
            {
                if (_disposing)
                {
                    return;
                }

                // Listener cannot be properly stopped. see https://github.com/dotnet/runtime/issues/30284
                _httpListener.Close();
                _httpListener = new HttpListener();
                _httpListener.Prefixes.Add(_prefix);
            }

            // start a new listening task that is cancelable, as GetContextAsync() is not.
            var task = Task.Factory.StartNew(async () =>
            {
                try
                {
                    // Waits for the OAuth authorization response.
                    while (string.IsNullOrEmpty(resultUri) && !cancellationToken.IsCancellationRequested)
                    {
                        var contextTask = _httpListener.GetContextAsync();
                        // use WhenAny and task.delay to return from the listening task as GetContextAsync is
                        // not cancelable.
                        await Task.WhenAny(contextTask, Task.Delay(-1, cancellationToken));

                        if (!cancellationToken.IsCancellationRequested)
                        {
                            try
                            {
                                var context = await contextTask;

                                var response = context.Response;
                                var requestedUri = context.Request.Url;

                                if (redirectUrl.IsBaseOf(requestedUri))
                                {
                                    resultUri = requestedUri.ToString();

                                    // Sends an HTTP response to the browser.
                                    var responseString = "<html><head></head><body>Please return to the app.</body></html>";
                                    var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                                    response.ContentLength64 = buffer.Length;
                                    var responseOutput = response.OutputStream;
                                    _ = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
                                    {
                                        responseOutput.Close();
                                        RestartListener();
                                        _cancelationTokenSource.Dispose();
                                        _cancelationTokenSource = null;
                                    }, TaskScheduler.Current);
                                }
                                else
                                {
                                    response.StatusCode = 404;
                                    response.OutputStream.Close();
                                }
                            }
                            catch (ObjectDisposedException)
                            {
                                // The http listener might be tearing down as a result of disposal / closure
                                // which will end the GetContextAsync() method. Just return here. There is nothing
                                // to listen for anymore.
                                return;
                            }
                        }
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        RestartListener();
                    }
                }
                catch (TaskCanceledException)
                {
                    RestartListener();
                }
                catch (OperationCanceledException)
                {
                    RestartListener();
                }
            }, cancellationToken, TaskCreationOptions.None, TaskScheduler.Current).Unwrap();

            _previousTask = task;
            await task;
            _previousTask = null;

            if (string.IsNullOrEmpty(resultUri))
            {
                return string.Empty;
            }

            return new Uri(resultUri).Query;
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

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposing = true;

                if (_httpListener != null)
                {
                    if (_httpListener.IsListening)
                    {
                        _httpListener.Close();
                    }
                    ((IDisposable)_httpListener).Dispose();
                    _httpListener = null;
                }
                if (_cancelationTokenSource != null)
                {
                    _cancelationTokenSource.Dispose();
                    _cancelationTokenSource = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
