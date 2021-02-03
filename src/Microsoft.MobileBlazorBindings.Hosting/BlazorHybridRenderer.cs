// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public class BlazorHybridRenderer : Renderer
    {
        private static readonly Type _writer;
        private static readonly MethodInfo _writeMethod;
        private static readonly Task _canceledTask = Task.FromCanceled(new CancellationToken(canceled: true));

        private const int RendererId = 0; // No need for more than one renderer per webview
        private readonly WebViewIPC _ipc;
        private readonly IJSRuntime _jsRuntime;
        private readonly Dispatcher _dispatcher;
        private readonly IBlazorErrorHandler _blazorErrorHandler;
        private readonly ConcurrentQueue<UnacknowledgedRenderBatch> _unacknowledgedRenderBatches = new();
        private bool _disposing;
        private long _nextRenderId = 1;


#pragma warning disable CA1810 // Initialize reference type static fields inline
        static BlazorHybridRenderer()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            _writer = typeof(RenderBatchWriter);
            _writeMethod = _writer.GetMethod("Write", new[] { typeof(RenderBatch).MakeByRefType() });
        }

        public BlazorHybridRenderer(WebViewIPC ipc, IServiceProvider serviceProvider, ILoggerFactory loggerFactory, JSRuntime jsRuntime, Dispatcher dispatcher, IBlazorErrorHandler blazorErrorHandler, string rootComponentElementSelector)
            : base(serviceProvider, loggerFactory)
        {
            _ipc = ipc ?? throw new ArgumentNullException(nameof(ipc));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            _blazorErrorHandler = blazorErrorHandler;
            rootComponentElementSelector ??= "app";

            var rootComponent = new RenderFragmentComponent();
            var rootComponentId = AssignRootComponentId(rootComponent);
            RootRenderHandle = rootComponent.RenderHandle;

            var initTask = _jsRuntime.InvokeAsync<object>(
                "Blazor._internal.attachRootComponentToElement",
                rootComponentElementSelector,
                rootComponentId,
                RendererId);
            CaptureAsyncExceptions(initTask);
        }

        public async Task DispatchEventAsync(WebEventDescriptor eventDescriptor, string eventArgsJson) {
            if (eventDescriptor is null)
            {
                throw new ArgumentNullException(nameof(eventDescriptor));
            }

            var webEvent = WebEventData.Parse(eventDescriptor, eventArgsJson);
            await Dispatcher.InvokeAsync(async () =>
                await DispatchEventAsync(
                    webEvent.EventHandlerId,
                    webEvent.EventFieldInfo,
                    webEvent.EventArgs).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Signals that a render batch has completed.
        /// </summary>
        /// <param name="incomingBatchId">The render batch id.</param>
        /// <param name="errorMessageOrNull">The error message or null.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task OnRenderCompletedAsync(long incomingBatchId, string errorMessageOrNull)
        {
            if (_disposing)
            {
                // Disposing so don't do work.
                return Task.CompletedTask;
            }

            if (!_unacknowledgedRenderBatches.TryPeek(out var nextUnacknowledgedBatch) || incomingBatchId < nextUnacknowledgedBatch.BatchId)
            {
                // TODO: Log duplicated batch ack.
                return Task.CompletedTask;
            }
            else
            {
                var lastBatchId = nextUnacknowledgedBatch.BatchId;

                // Order is important here so that we don't prematurely dequeue the last nextUnacknowledgedBatch
                while (_unacknowledgedRenderBatches.TryPeek(out nextUnacknowledgedBatch) && nextUnacknowledgedBatch.BatchId <= incomingBatchId)
                {
                    lastBatchId = nextUnacknowledgedBatch.BatchId;

                    // At this point the queue is definitely not full, we have at least emptied one slot, so we allow a further
                    // full queue log entry the next time it fills up.
                    _unacknowledgedRenderBatches.TryDequeue(out _);
                    ProcessPendingBatch(errorMessageOrNull, nextUnacknowledgedBatch);
                }

                if (lastBatchId < incomingBatchId)
                {
                    // This exception is due to a bad client input, so we mark it as such to prevent logging it as a warning and
                    // flooding the logs with warnings.
                    throw new InvalidOperationException($"Received an acknowledgement for batch with id '{incomingBatchId}' when the last batch produced was '{lastBatchId}'.");
                }

                // Normally we will not have pending renders, but it might happen that we reached the limit of
                // available buffered renders and new renders got queued.
                // Invoke ProcessBufferedRenderRequests so that we might produce any additional batch that is
                // missing.

                // We return the task in here, but the caller doesn't await it.
                return Dispatcher.InvokeAsync(() =>
                {
                    // Now we're on the sync context, check again whether we got disposed since this
                    // work item was queued. If so there's nothing to do.
                    if (!_disposing)
                    {
                        ProcessPendingRender();
                    }
                });
            }
        }

        public RenderHandle RootRenderHandle { get; }

        public override Dispatcher Dispatcher => _dispatcher;

        protected override void HandleException(Exception exception)
        {
            if (_blazorErrorHandler != null)
            {
                _blazorErrorHandler.HandleException(exception);
            }
            else
            {
                if (Dispatcher.CheckAccess())
                {
                    Dispatcher.InvokeAsync(() => throw exception);
                }
                else
                {
                    throw exception;
                }
            }
        }

        private async void CaptureAsyncExceptions(ValueTask<object> task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                HandleException(ex);
            }
        }


        /// <summary>
        /// Processes a pending batch.
        /// </summary>
        /// <param name="errorMessageOrNull">The error message or null.</param>
        /// <param name="entry">The entry to process.</param>
        private static void ProcessPendingBatch(string errorMessageOrNull, UnacknowledgedRenderBatch entry)
        {
            CompleteRender(entry.CompletionSource, errorMessageOrNull);
        }

        /// <summary>
        /// Completes a render pass.
        /// </summary>
        /// <param name="pendingRenderInfo">The pending render info.</param>
        /// <param name="errorMessageOrNull">The error message.</param>
        private static void CompleteRender(TaskCompletionSource<object> pendingRenderInfo, string errorMessageOrNull)
        {
            if (errorMessageOrNull == null)
            {
                pendingRenderInfo.TrySetResult(null);
            }
            else
            {
                pendingRenderInfo.TrySetException(new InvalidOperationException(errorMessageOrNull));
            }
        }

        /// <summary>
        /// Releases all resources currently used by this <see cref="BlazorHybridRenderer"/> instance.
        /// </summary>
        /// <param name="disposing">true if this method is being invoked by System.IDisposable.Dispose, otherwise false.</param>
        protected override void Dispose(bool disposing)
        {
            _disposing = true;

            while (_unacknowledgedRenderBatches.TryDequeue(out var entry))
            {
                try
                {
                    entry.CompletionSource.TrySetCanceled();
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch
#pragma warning restore CA1031 // Do not catch general exception types
                {
                }
            }

            base.Dispose(true);
        }

        /// <summary>
        /// Updates the visible part of the UI.
        /// </summary>
        /// <param name="batch">The batch to render.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override Task UpdateDisplayAsync(in RenderBatch batch)
        {
            if (_disposing)
            {
                // We are being disposed, so do no work.
                return _canceledTask;
            }

            string base64;
            using (var memoryStream = new MemoryStream())
            {
                var renderBatchWriter = Activator.CreateInstance(_writer, new object[] { memoryStream, false });
                using (renderBatchWriter as IDisposable)
                {
                    // TODO: use delegate instead of reflection for more performance.
                    _writeMethod.Invoke(renderBatchWriter, new object[] { batch });
                }

                var batchBytes = memoryStream.ToArray();
                base64 = Convert.ToBase64String(batchBytes);
            }

            var renderId = Interlocked.Increment(ref _nextRenderId);

            var pendingRender = new UnacknowledgedRenderBatch(
                renderId,
                new TaskCompletionSource<object>());

            // Buffer the rendered batches no matter what. We'll send it down immediately when the client
            // is connected or right after the client reconnects.
            _unacknowledgedRenderBatches.Enqueue(pendingRender);

            _ipc.Send("JS.RenderBatch", renderId, base64);

            return pendingRender.CompletionSource.Task;
        }

        private class RenderFragmentComponent : IComponent
        {
            public RenderHandle RenderHandle { get; private set; }

            public void Attach(RenderHandle renderHandle)
                => RenderHandle = renderHandle;

            public Task SetParametersAsync(ParameterView parameters)
                => Task.CompletedTask;
        }

        /// <summary>
        /// A struct representing an unacknowledged render batch.
        /// </summary>
        internal readonly struct UnacknowledgedRenderBatch
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UnacknowledgedRenderBatch"/> struct.
            /// </summary>
            /// <param name="batchId">The batch id.</param>
            /// <param name="completionSource">The completion source.</param>
            public UnacknowledgedRenderBatch(long batchId, TaskCompletionSource<object> completionSource)
            {
                BatchId = batchId;
                CompletionSource = completionSource;
            }

            /// <summary>
            /// Gets the batch id.
            /// </summary>
            public long BatchId { get; }

            /// <summary>
            /// Gets the completion source.
            /// </summary>
            public TaskCompletionSource<object> CompletionSource { get; }
        }
    }
}