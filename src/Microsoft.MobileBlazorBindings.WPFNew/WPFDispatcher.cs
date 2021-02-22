using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static System.Windows.Threading.Dispatcher;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    /// <summary>
    /// Custom dispatcher for WPF apps to ensure all UI work is done on the UI (main) thread.
    /// </summary>
    internal class WPFDispatcher : Dispatcher
    {
        public static WPFDispatcher Instance { get; } = new();

        public override bool CheckAccess()
        {
            return CurrentDispatcher.CheckAccess();
        }

        public override Task InvokeAsync(Action workItem)
        {
            return CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return CurrentDispatcher.InvokeAsync(workItem).Result;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return CurrentDispatcher.InvokeAsync(workItem).Result;
        }
    }
}
