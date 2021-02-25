using Microsoft.AspNetCore.Components;
using System;
using System.Text;
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
    }
}
