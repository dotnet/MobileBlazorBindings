// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui
{
    /// <summary>
    /// Custom dispatcher for MAUI apps to ensure all UI work is done on the UI (main) thread.
    /// </summary>
    internal class XamarinDeviceDispatcher : Microsoft.AspNetCore.Components.Dispatcher
    {
        private static IDispatcher Dispatcher => Application.Current.Dispatcher;

        public override bool CheckAccess()
        {
            return !Dispatcher.IsDispatchRequired;
        }

        public override Task InvokeAsync(Action workItem)
        {
            return Dispatcher.DispatchAsync(workItem);
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return Dispatcher.DispatchAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return Dispatcher.DispatchAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return Dispatcher.DispatchAsync(workItem);
        }
    }
}
