// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.WebView.Elements;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.WebView
{
    internal class IPC
    {
        private readonly WebViewExtended _webView;
        private readonly Dictionary<string, List<Action<object>>> _registrations = new Dictionary<string, List<Action<object>>>();

        public IPC(WebViewExtended webView)
        {
            _webView = webView ?? throw new ArgumentNullException(nameof(webView));
            _webView.OnWebMessageReceived += HandleScriptNotify;
        }

        public void Send(string eventName, params object[] args)
        {
            try
            {
                _webView.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    _webView.SendMessage($"{eventName}:{JsonSerializer.Serialize(args)}");
                });
            }
#pragma warning disable CA1031 // Do not catch general exception types
            // TODO: Need to figure out if this is needed
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Console.WriteLine(ex.Message);
            }
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
                            _webView.Dispatcher.BeginInvokeOnMainThread(() =>
                            {
                                callback(args);
                            });
                        }
                    }
                },
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default);
        }
    }
}
