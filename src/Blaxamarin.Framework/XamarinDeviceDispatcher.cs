using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    /// <summary>
    /// Custom dispatcher for Xamarin.Forms apps to ensure all UI work is done on the UI (main) thread.
    /// </summary>
    internal class XamarinDeviceDispatcher : Dispatcher
    {
        public override bool CheckAccess()
        {
            return !Device.IsInvokeRequired;
        }

        public override Task InvokeAsync(Action workItem)
        {
            return Device.InvokeOnMainThreadAsync(workItem);
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return Device.InvokeOnMainThreadAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return Device.InvokeOnMainThreadAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return Device.InvokeOnMainThreadAsync(workItem);
        }
    }
}
