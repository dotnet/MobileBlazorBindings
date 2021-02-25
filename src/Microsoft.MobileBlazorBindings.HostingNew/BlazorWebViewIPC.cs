using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    internal class BlazorWebViewIPC
    {
        private readonly Dispatcher _dispatcher;
        private readonly Func<string, ValueTask> _sendAsync;
        private readonly StringBuilder _ipcStringBuilder = new();

        public BlazorWebViewIPC(Dispatcher dispatcher, Func<string, ValueTask> sendAsync)
        {
            _dispatcher = dispatcher;
            _sendAsync = sendAsync;
        }

        public ValueTask SendRenderBatchAsync(long batchId, Span<byte> data)
        {
            _dispatcher.CheckAccess();

            try
            {
                _ipcStringBuilder.Append("JS.RenderBatch:[");
                _ipcStringBuilder.Append(batchId);
                _ipcStringBuilder.Append(",\"");
                _ipcStringBuilder.Append(Convert.ToBase64String(data));
                _ipcStringBuilder.Append("\"]");

                return _sendAsync(_ipcStringBuilder.ToString());
            }
            finally
            {
                _ipcStringBuilder.Clear();
            }
        }

        public ValueTask SendBeginInvokeJS(long taskId, string identifier, string argsJson, JSCallResultType resultType, long targetInstanceId)
        {
            _dispatcher.CheckAccess();

            try
            {
                _ipcStringBuilder.Append("JS.BeginInvokeJS:[");
                _ipcStringBuilder.Append(taskId);
                _ipcStringBuilder.Append(",\"");
                _ipcStringBuilder.Append(identifier); // TODO: JSON-encode
                _ipcStringBuilder.Append("\",");

                // TODO: Stop having so many nested layers of encoding. If this is already JSON,
                // we should be able to append it as-is.
                _ipcStringBuilder.Append(JsonSerializer.Serialize(argsJson));

                _ipcStringBuilder.Append(',');
                _ipcStringBuilder.Append((int)resultType);
                _ipcStringBuilder.Append(',');
                _ipcStringBuilder.Append(targetInstanceId);
                _ipcStringBuilder.Append(']');

                return _sendAsync(_ipcStringBuilder.ToString());
            }
            finally
            {
                _ipcStringBuilder.Clear();
            }
        }

        public ValueTask SendEndInvokeDotNet(string callId, bool success, object resultOrError)
        {
            _dispatcher.CheckAccess();

            try
            {
                _ipcStringBuilder.Append("JS.EndInvokeDotNet:[\"");
                _ipcStringBuilder.Append(callId); // TODO: JSON-encode
                _ipcStringBuilder.Append("\",");
                _ipcStringBuilder.Append(success);
                if (resultOrError != null)
                {
                    // TODO: Should this be JSON-serialized entirely?
                    _ipcStringBuilder.Append(",\"");
                    _ipcStringBuilder.Append(resultOrError.ToString());
                    _ipcStringBuilder.Append('"');
                }
                _ipcStringBuilder.Append("\"]");

                return _sendAsync(_ipcStringBuilder.ToString());
            }
            finally
            {
                _ipcStringBuilder.Clear();
            }
        }
    }
}
