// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.Routing;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Hosting
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class BlazorHybridNavigationInterception : INavigationInterception
#pragma warning restore CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    {
        public Task EnableNavigationInterceptionAsync()
        {
            // We don't actually need to set anything up in this environment
            return Task.CompletedTask;
        }
    }
}
