// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.Routing;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    internal class BlazorHybridNavigationInterception : INavigationInterception
    {
        public Task EnableNavigationInterceptionAsync()
        {
            // We don't actually need to set anything up in this environment
            return Task.CompletedTask;
        }
    }
}
