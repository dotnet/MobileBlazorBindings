// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    // TODO: This used to be internal. Is it OK to be public now?
    public class BlazorHybridNavigationManager : NavigationManager
    {
        private const string InteropPrefix = "Blazor._internal.navigationManager.";
        private const string InteropNavigateTo = InteropPrefix + "navigateTo";

        private IJSRuntime _jsRuntime;
        private bool _isInitialized;

#pragma warning disable CA1054 // URI-like parameters should not be strings
        public void Initialize(IJSRuntime jsRuntime, string baseUri, string initialUri)
#pragma warning restore CA1054 // URI-like parameters should not be strings
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
#pragma warning disable CA2012 // Use ValueTasks correctly
            _jsRuntime.InvokeAsync<object>(InteropNavigateTo, uri, forceLoad);
#pragma warning restore CA2012 // Use ValueTasks correctly
        }

#pragma warning disable CA1054 // URI-like parameters should not be strings
        public void SetLocation(string uri, bool isInterceptedLink)
#pragma warning restore CA1054 // URI-like parameters should not be strings
        {
            Uri = uri;
            NotifyLocationChanged(isInterceptedLink);
        }
    }
}
