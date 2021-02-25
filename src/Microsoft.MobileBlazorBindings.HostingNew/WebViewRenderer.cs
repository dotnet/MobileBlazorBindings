using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    internal class WebViewRenderer : Renderer
    {
        private const int RendererId = 0; // Only one per WebView
        private readonly Action<Exception> _onException;
        private readonly Dispatcher _dispatcher;
        private readonly IJSRuntime _js;
        private readonly BlazorWebViewIPC _ipc;
        private readonly ConcurrentQueue<UnacknowledgedRenderBatch> _unacknowledgedRenderBatches = new();
        private long _nextRenderId = 1;

        public WebViewRenderer(
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            Action<Exception> onException,
            Dispatcher dispatcher,
            IJSRuntime jsRuntime,
            BlazorWebViewIPC ipc)
            : base(serviceProvider, loggerFactory)
        {
            _onException = onException ?? throw new ArgumentNullException(nameof(onException));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _ipc = ipc ?? throw new ArgumentNullException(nameof(ipc));
            _js = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public override Dispatcher Dispatcher => _dispatcher;

        public async Task AddRootComponentAsync(Type componentType, string domElementSelector, ParameterView parameters)
        {
            var component = InstantiateComponent(componentType);
            var componentId = AssignRootComponentId(component);

            await _js.InvokeAsync<object>(
                "Blazor._internal.attachRootComponentToElement",
                domElementSelector,
                componentId,
                RendererId).ConfigureAwait(true);

            await RenderRootComponentAsync(componentId, parameters).ConfigureAwait(true);
        }

        protected override void HandleException(Exception exception)
            => _onException(exception);

        protected override Task UpdateDisplayAsync(in RenderBatch batch)
        {
            var arrayBuilder = new ArrayBuilder<byte>(2048);
            using var memoryStream = new ArrayBuilderMemoryStream(arrayBuilder);
            using var renderBatchWriter = new RenderBatchWriter(memoryStream, false);
            renderBatchWriter.Write(in batch);

            var renderId = Interlocked.Increment(ref _nextRenderId);
            var completionSource = new TaskCompletionSource();
            _unacknowledgedRenderBatches.Enqueue(new UnacknowledgedRenderBatch(
                renderId, arrayBuilder, completionSource));

            return SendRenderBatchAsync(renderId, arrayBuilder, completionSource);
        }

        private async Task SendRenderBatchAsync(long batchId, ArrayBuilder<byte> arrayBuilder, TaskCompletionSource completionSource)
        {
            await _ipc.SendRenderBatchAsync(batchId, new Span<byte>(arrayBuilder.Buffer, 0, arrayBuilder.Count)).ConfigureAwait(true);
            await completionSource.Task.ConfigureAwait(true);
        }

        public void RenderBatchAcknowledged(long batchId)
        {
            if (_unacknowledgedRenderBatches.TryDequeue(out var info))
            {
                if (info.BatchId != batchId)
                {
                    throw new ArgumentException($"Received acknowledgement of batch {batchId}, but the next batch to acknowledge is {info.BatchId}");
                }

                info.Data.Dispose();
                info.CompletionSource.SetResult();
            }
            else
            {
                throw new InvalidOperationException("There are no pending batches to acknowledge");
            }
        }

        private readonly struct UnacknowledgedRenderBatch
        {
            public UnacknowledgedRenderBatch(long batchId, ArrayBuilder<byte> data, TaskCompletionSource completionSource)
            {
                BatchId = batchId;
                Data = data;
                CompletionSource = completionSource;
            }

            public long BatchId { get; }
            public ArrayBuilder<byte> Data { get; }
            public TaskCompletionSource CompletionSource { get; }
        }
    }
}
