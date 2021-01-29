// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.WPF
{
    /// <summary>
    /// Custom dispatcher for WPF apps to ensure all UI work is done on the UI (main) thread.
    /// </summary>
    internal class WPFDispatcher : Dispatcher
    {
        public static WPFDispatcher Instance { get; } = new();

        public override bool CheckAccess()
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.CheckAccess();
        }

        public override Task InvokeAsync(Action workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Result;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(workItem).Result;
        }
    }
}
