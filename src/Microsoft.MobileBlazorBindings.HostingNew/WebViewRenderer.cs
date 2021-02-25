using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    internal class WebViewRenderer : Renderer
    {
        private readonly Action<Exception> _onException;
        private readonly Dispatcher _dispatcher;
        private readonly BlazorWebViewIPC _ipc;
        private readonly ConcurrentQueue<UnacknowledgedRenderBatch> _unacknowledgedRenderBatches = new();
        private long _nextRenderId = 1;

        public WebViewRenderer(
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            Action<Exception> onException,
            Dispatcher dispatcher,
            BlazorWebViewIPC ipc)
            : base(serviceProvider, loggerFactory)
        {
            _onException = onException ?? throw new ArgumentNullException(nameof(onException));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _ipc = ipc ?? throw new ArgumentNullException(nameof(ipc));
        }

        public override Dispatcher Dispatcher => _dispatcher;

        public Task AddRootComponentAsync(Type componentType, string domElementSelector, ParameterView parameters)
        {
            var component = InstantiateComponent(componentType);
            var componentId = AssignRootComponentId(component);

            // TODO: At this point, notify the JS runtime that it should associate
            // componentId with selector so the following renderbatch will work

            return RenderRootComponentAsync(componentId, parameters);
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
