// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class BlazorHybridNavigationManager : NavigationManager
    {
        public static readonly BlazorHybridNavigationManager Instance = new BlazorHybridNavigationManager();

        private const string InteropPrefix = "Blazor._internal.navigationManager.";
        private const string InteropNavigateTo = InteropPrefix + "navigateTo";

        private IJSRuntime _jsRuntime;
        private bool _isInitialized;

        public void Initialize(IJSRuntime jsRuntime, string baseUri, string initialUri)
        {
            Initialize(baseUri, initialUri);
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            _isInitialized = true;
        }

        protected override void EnsureInitialized()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException($"Didn't receive any call to {nameof(BlazorHybridNavigationManager)}.{nameof(Initialize)} in time.");
            }
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            _jsRuntime.InvokeAsync<object>(InteropNavigateTo, uri, forceLoad);
        }

        public void SetLocation(string uri, bool isInterceptedLink)
        {
            Uri = uri;
            NotifyLocationChanged(isInterceptedLink);
        }
    }
}
