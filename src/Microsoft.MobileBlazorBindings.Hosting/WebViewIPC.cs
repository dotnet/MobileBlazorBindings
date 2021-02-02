// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public class WebViewIPC : IDisposable
    {
        private readonly IWebViewIPCAdapter _webViewIPCAdapter;

        private readonly Dictionary<string, List<Action<object>>> _registrations = new();
        private bool _isDisposed;

        public WebViewIPC(IWebViewIPCAdapter webViewIPCAdapter)
        {
            _webViewIPCAdapter = webViewIPCAdapter ?? throw new ArgumentNullException(nameof(webViewIPCAdapter));

            _webViewIPCAdapter.OnWebMessageReceived += HandleScriptNotify;
        }

        public void Send(string eventName, params object[] args)
        {
            try
            {
                _webViewIPCAdapter.BeginInvoke(new Action(() =>
                {
                    _webViewIPCAdapter.SendMessage($"{eventName}:{JsonSerializer.Serialize(args)}");
                }));
            }
#pragma warning disable CA1031 // Do not catch general exception types
            // TODO: Need to figure out if this is needed
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task<InteropHandshakeResult> AttachInteropAsync(JSRuntime jsRuntime)
        {
            if (jsRuntime is null)
            {
                throw new ArgumentNullException(nameof(jsRuntime));
            }

            var resultTcs = new TaskCompletionSource<InteropHandshakeResult>();

            // These hacks can go away once there's a proper IPC channel for event notifications etc.
            var selfAsDotNetObjectReference = typeof(DotNetObjectReference).GetMethod(nameof(DotNetObjectReference.Create))
                .MakeGenericMethod(GetType())
                .Invoke(null, new[] { this });
            var selfAsDotnetObjectReferenceId = (long)typeof(JSRuntime).GetMethod("TrackObjectReference", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(GetType())
                .Invoke(jsRuntime, new[] { selfAsDotNetObjectReference });

            Once("components:init", args =>
            {
                var argsArray = (object[])args;
                var initialUriAbsolute = ((JsonElement)argsArray[0]).GetString();
                var baseUriAbsolute = ((JsonElement)argsArray[1]).GetString();
                resultTcs.TrySetResult(new InteropHandshakeResult(baseUriAbsolute, initialUriAbsolute));
            });

            On("BeginInvokeDotNetFromJS", args =>
            {
                var argsArray = (object[])args;
                var assemblyName = argsArray[1] != null ? ((JsonElement)argsArray[1]).GetString() : null;
                var methodIdentifier = ((JsonElement)argsArray[2]).GetString();
                var dotNetObjectId = ((JsonElement)argsArray[3]).GetInt64();
                var callId = ((JsonElement)argsArray[0]).GetString();
                var argsJson = ((JsonElement)argsArray[4]).GetString();

                // As a temporary hack, intercept blazor.desktop.js's JS interop calls for event notifications,
                // and direct them to our own instance. This is to avoid needing a static BlazorHybridRenderer.Instance.
                // Similar temporary hack for navigation notifications
                // TODO: Change blazor.desktop.js to use a dedicated IPC call for these calls, not JS interop.

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // TODO TODO TODO: This is the wrong assembly name. Change to final name. Matches the code in Boot.Desktop.ts !!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                if (assemblyName == "Microsoft.MobileBlazorBindings.WebView")
                {
                    assemblyName = null;
                    dotNetObjectId = selfAsDotnetObjectReferenceId;
                }

                DotNetDispatcher.BeginInvokeDotNet(
                    jsRuntime,
                    new DotNetInvocationInfo(assemblyName, methodIdentifier, dotNetObjectId, callId),
                    argsJson);
            });

            On("EndInvokeJSFromDotNet", args =>
            {
                var argsArray = (object[])args;
                DotNetDispatcher.EndInvokeJS(
                    jsRuntime,
                    ((JsonElement)argsArray[2]).GetString());
            });

            return resultTcs.Task;
        }

        public void On(string eventName, Action<object> callback)
        {
            lock (_registrations)
            {
                if (!_registrations.TryGetValue(eventName, out var group))
                {
                    group = new List<Action<object>>();
                    _registrations.Add(eventName, group);
                }

                group.Add(callback);
            }
        }

        public void Once(string eventName, Action<object> callback)
        {
            void callbackOnce(object arg)
            {
                Off(eventName, callbackOnce);
                callback(arg);
            }

            On(eventName, callbackOnce);
        }

        public void Off(string eventName, Action<object> callback)
        {
            lock (_registrations)
            {
                if (_registrations.TryGetValue(eventName, out var group))
                {
                    group.Remove(callback);
                }
            }
        }

        private void HandleScriptNotify(object sender, string message)
        {
            var value = message;

            // Move off the browser UI thread
            Task.Factory.StartNew(
                () =>
                {
                    if (value.StartsWith("ipc:", StringComparison.Ordinal))
                    {
                        var spacePos = value.IndexOf(' ');
                        var eventName = value.Substring(4, spacePos - 4);
                        var argsJson = value.Substring(spacePos + 1);
                        var args = JsonSerializer.Deserialize<object[]>(argsJson);

                        Action<object>[] callbacksCopy;
                        lock (_registrations)
                        {
                            if (!_registrations.TryGetValue(eventName, out var callbacks))
                            {
                                return;
                            }

                            callbacksCopy = callbacks.ToArray();
                        }

                        foreach (var callback in callbacksCopy)
                        {
                            _webViewIPCAdapter.BeginInvoke(new Action(() =>
                            {
                                callback(args);
                            }));
                        }
                    }
                },
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default);
        }

        [JSInvokable(nameof(DispatchEvent))]
        public async Task DispatchEvent(WebEventDescriptor eventDescriptor, string eventArgsJson)
        {
            if (eventDescriptor is null)
            {
                throw new ArgumentNullException(nameof(eventDescriptor));
            }
            await _webViewIPCAdapter.DispatchEvent(eventDescriptor, eventArgsJson).ConfigureAwait(false);
        }

        [JSInvokable(nameof(NotifyLocationChanged))]
#pragma warning disable CA1054 // Uri parameters should not be strings
        public void NotifyLocationChanged(string uri, bool isInterceptedLink)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _webViewIPCAdapter.NotifyLocationChanged(uri, isInterceptedLink);
        }

        [JSInvokable(nameof(OnRenderCompleted))]
        public async Task OnRenderCompleted(long renderId, string errorMessageOrNull)
        {
            await _webViewIPCAdapter.OnRenderCompleted(renderId, errorMessageOrNull).ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                    _webViewIPCAdapter.OnWebMessageReceived -= HandleScriptNotify;
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
