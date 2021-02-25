using Microsoft.AspNetCore.Components;
using System;
using System.Runtime.ExceptionServices;
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
            if (CurrentDispatcher.CheckAccess())
            {
                try
                {
                    workItem();
                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    // If the intention is to rethrow an exception on the UI thread, the only way of making
                    // the UnhandledException events fire is to do it via BeginInvoke, not InvokeAsync.
                    CurrentDispatcher.BeginInvoke((Action<Exception>)((Exception exception) =>
                    {
                        ExceptionDispatchInfo.Capture(exception).Throw();
                    }), ex);
                    return Task.FromException(ex);
                }
            }
            else
            {
                return CurrentDispatcher.InvokeAsync(workItem).Task;
            }
        }

        public override async Task InvokeAsync(Func<Task> workItem)
        {
            if (CurrentDispatcher.CheckAccess())
            {
                try
                {
                    await workItem().ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    // If the intention is to rethrow an exception on the UI thread, the only way of making
                    // the UnhandledException events fire is to do it via BeginInvoke, not InvokeAsync.
                    CurrentDispatcher.BeginInvoke((Action<Exception>)((Exception exception) =>
                    {
                        ExceptionDispatchInfo.Capture(exception).Throw();
                    }), ex);

                    throw;
                }
            }
            else
            {
                await CurrentDispatcher.InvokeAsync(workItem).Task.ConfigureAwait(true);
            }
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return CurrentDispatcher.CheckAccess()
                ? Task.FromResult(workItem())
                : CurrentDispatcher.InvokeAsync(workItem).Task;
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return CurrentDispatcher.CheckAccess()
                ? workItem()
                : CurrentDispatcher.InvokeAsync(workItem).Task.Unwrap();
        }
    }
}
