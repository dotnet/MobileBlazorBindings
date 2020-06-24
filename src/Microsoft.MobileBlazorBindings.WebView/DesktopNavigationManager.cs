using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class DesktopNavigationManager : NavigationManager
    {
        public static readonly DesktopNavigationManager Instance = new DesktopNavigationManager();

        private static readonly string InteropPrefix = "Blazor._internal.navigationManager.";
        private static readonly string InteropNavigateTo = InteropPrefix + "navigateTo";

        private IJSRuntime _jsRuntime;
        private bool isInitialized;

        public void Initialize(IJSRuntime jsRuntime, string baseUri, string initialUri)
        {
            Initialize(baseUri, initialUri);
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            isInitialized = true;
        }

        protected override void EnsureInitialized()
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException($"Didn't receive any call to {nameof(DesktopNavigationManager)}.{nameof(Initialize)} in time.");
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
