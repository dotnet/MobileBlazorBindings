using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    internal class BlazorWebViewJSRuntime : JSRuntime
    {
        private BlazorWebViewIPC _ipc;

        private static readonly Type VoidTaskResultType = typeof(Task).Assembly
            .GetType("System.Threading.Tasks.VoidTaskResult", true);

        public BlazorWebViewJSRuntime()
        {
            JsonSerializerOptions.Converters.Add(new ElementReferenceJsonConverter());
        }

        public void AttachToIpcChannel(BlazorWebViewIPC ipc)
        {
            _ipc = ipc ?? throw new ArgumentNullException(nameof(ipc));
        }

        protected override void BeginInvokeJS(long taskId, string identifier, string argsJson, JSCallResultType resultType, long targetInstanceId)
        {
            ThrowIfIpcNotSet();

            _ = _ipc.SendBeginInvokeJS(taskId, identifier, argsJson, resultType, targetInstanceId);
        }

        protected override void EndInvokeDotNet(DotNetInvocationInfo invocationInfo, in DotNetInvocationResult invocationResult)
        {
            ThrowIfIpcNotSet();

            // The other params aren't strictly required and are only used for logging
            var resultOrError = invocationResult.Success ? HandlePossibleVoidTaskResult(invocationResult.Result) : invocationResult.Exception.ToString();
            _ = _ipc.SendEndInvokeDotNet(invocationInfo.CallId, invocationResult.Success, resultOrError);
        }

        private static object HandlePossibleVoidTaskResult(object result)
        {
            // Looks like the TaskGenericsUtil logic in Microsoft.JSInterop doesn't know how to
            // understand System.Threading.Tasks.VoidTaskResult
            return result?.GetType() == VoidTaskResultType ? null : result;
        }

        private void ThrowIfIpcNotSet()
        {
            if (_ipc == null)
            {
                throw new InvalidOperationException($"{nameof(AttachToIpcChannel)} must be called before using {nameof(IJSRuntime)}.");
            }
        }
    }
}
