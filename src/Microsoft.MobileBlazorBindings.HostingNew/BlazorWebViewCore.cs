using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    /// <summary>
    /// Platform-agnostic parts of BlazorWebView
    /// </summary>
    public abstract class BlazorWebViewCore : IDisposable
    {
        private static readonly FileExtensionContentTypeProvider FileExtensionContentTypeProvider = new();

        private readonly IServiceProvider _serviceProvider;
        private readonly Dispatcher _dispatcher;
        private readonly string _contentHost;
        private readonly string _contentRootPath;
        private readonly string _hostPageRelativeUrl;
        private readonly List<(Type type, string selector, ParameterView parameters)> _rootComponents = new();
        private readonly BlazorWebViewIPC _ipc;
        
        private WebViewPageContext _currentContext;

        public BlazorWebViewCore(IServiceProvider serviceProvider, Dispatcher dispatcher, string hostPageFilePath)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _ipc = new BlazorWebViewIPC(_dispatcher, SendIpcMessageAsync);

            var hostPageAbsolute = Path.GetFullPath(hostPageFilePath);
            _contentHost = "0.0.0.0";
            _contentRootPath = Path.GetDirectoryName(hostPageAbsolute);
            _hostPageRelativeUrl = Path.GetRelativePath(_contentRootPath, hostPageAbsolute).Replace(Path.DirectorySeparatorChar, '/');
        }

        public async Task AddRootComponentAsync(Type type, string selector, ParameterView parameters)
        {
            // Keep track of the root components so we can restore them each time the page is loaded
            _rootComponents.Add((type, selector, parameters));

            // If a page is already loaded, notify it
            if (_currentContext != null)
            {
                await _currentContext.Renderer.AddRootComponentAsync(type, selector, parameters).ConfigureAwait(true);
            }
        }

        protected abstract void Navigate(Uri uri);

        public virtual Task StartAsync()
        {
            var startUri = new Uri(new Uri($"https://{_contentHost}/"), _hostPageRelativeUrl);
            Navigate(startUri);
            return Task.CompletedTask;
        }

        private void NotifyUnhandledException(Exception exception)
        {
            _dispatcher.InvokeAsync(() => ExceptionDispatchInfo.Capture(exception).Throw());
        }

        protected abstract ValueTask SendIpcMessageAsync(string message);

        protected void ReceiveIpcMessage(string source, string message)
        {
            if (source == null || message == null || !source.StartsWith($"https://{_contentHost}/", StringComparison.Ordinal))
            {
                return;
            }

            _dispatcher.InvokeAsync(async () =>
            {
                if (message.StartsWith("\"ipc:components:init", StringComparison.Ordinal))
                {
                    _currentContext = new WebViewPageContext(_serviceProvider, _dispatcher,
                        _ipc, onUnhandledException: NotifyUnhandledException);
                    await _currentContext.AddRootComponents(_rootComponents).ConfigureAwait(true);
                }
            });
        }

        protected bool TryGetResponseContent(Uri requestUri, out int statusCode, out string statusMessage, out Stream content, out string headers)
        {
            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (string.Equals(requestUri.Host, _contentHost, StringComparison.Ordinal))
            {
                // Serve _framework files
                var requestUriPath = requestUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
                if (TryGetFrameworkFileContent(requestUriPath, out content))
                {
                    statusCode = 200;
                    statusMessage = "OK";
                    headers = GetResponseHeaders(GetResponseContentTypeOrDefault(requestUriPath));
                    return true;
                }

                // Serve files from disk
                // TODO: Make this pluggable, as not all platforms have disk access
                var filePath = Path.GetFullPath(Path.Combine(_contentRootPath, requestUriPath));
                if (filePath.StartsWith(_contentRootPath, StringComparison.Ordinal)
                    && File.Exists(filePath))
                {
                    statusCode = 200;
                    statusMessage = "OK";
                    headers = GetResponseHeaders(GetResponseContentTypeOrDefault(filePath));
                    content = File.OpenRead(filePath);
                    return true;
                }

                // Always provide a response to requests on the virtual domain, even if no file matches
                var message = $"There is no file at {filePath}";
                statusCode = 404;
                statusMessage = "Not found";
                headers = GetResponseHeaders("text/plain");
#pragma warning disable CA2000 // Dispose objects before losing scope
                content = new MemoryStream(Encoding.UTF8.GetBytes(message));
#pragma warning restore CA2000 // Dispose objects before losing scope

                return true;
            }

            statusCode = default;
            statusMessage = default;
            headers = default;
            content = default;
            return false;
        }

        private static bool TryGetFrameworkFileContent(string requestUriPath, out Stream content)
        {
            const string prefix = "_framework/";
            if (requestUriPath.StartsWith(prefix, StringComparison.Ordinal))
            {
                switch (requestUriPath.Substring(prefix.Length))
                {
                    case "blazor.webview.js":
                        content = typeof(BlazorWebViewCore).Assembly.GetManifestResourceStream("Microsoft.MobileBlazorBindings.HostingNew.Resources.blazor.webview.js");
                        return true;
                }
            }

            content = default;
            return false;
        }

        private static string GetResponseContentTypeOrDefault(string path)
        {
            return FileExtensionContentTypeProvider.TryGetContentType(path, out var matchedContentType)
                ? matchedContentType
                : "application/octet-stream";
        }

        private static string GetResponseHeaders(string contentType)
        {
            return $"Content-Type: {contentType}{Environment.NewLine}Cache-Control: no-cache, max-age=0, must-revalidate, no-store";
        }

        public void Dispose()
        {
            _currentContext?.Dispose();
        }
    }
}
