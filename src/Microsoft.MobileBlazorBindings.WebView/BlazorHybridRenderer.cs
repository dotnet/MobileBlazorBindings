// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.WebView
{
#pragma warning disable BL0006 // Do not use RenderTree types
    internal class BlazorHybridRenderer : Renderer
    {
        private static readonly Type _writer;
        private static readonly MethodInfo _writeMethod;

        private readonly int _rendererId = 0; // No need for more than one renderer per webview
        private readonly IPC _ipc;
        private readonly IJSRuntime _jsRuntime;
        private readonly Dispatcher _dispatcher;

#pragma warning disable CA1810 // Initialize reference type static fields inline
        static BlazorHybridRenderer()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            _writer = typeof(RenderBatchWriter);
            _writeMethod = _writer.GetMethod("Write", new[] { typeof(RenderBatch).MakeByRefType() });
        }

        public BlazorHybridRenderer(IPC ipc, IServiceProvider serviceProvider, ILoggerFactory loggerFactory, JSRuntime jsRuntime, Dispatcher dispatcher)
            : base(serviceProvider, loggerFactory)
        {
            _ipc = ipc ?? throw new ArgumentNullException(nameof(ipc));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));

            var rootComponent = new RenderFragmentComponent();
            var rootComponentId = AssignRootComponentId(rootComponent);
            RootRenderHandle = rootComponent.RenderHandle;

            var initTask = _jsRuntime.InvokeAsync<object>(
                "Blazor._internal.attachRootComponentToElement",
                "app",
                rootComponentId,
                _rendererId);
            CaptureAsyncExceptions(initTask);
        }

        public RenderHandle RootRenderHandle { get; }

        public override Dispatcher Dispatcher => _dispatcher;

        protected override void HandleException(Exception exception)
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

        private async void CaptureAsyncExceptions(ValueTask<object> task)
        {
            try
            {
                await task;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                HandleException(ex);
            }
        }

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            string base64;
            using (var memoryStream = new MemoryStream())
            {
                var renderBatchWriter = Activator.CreateInstance(_writer, new object[] { memoryStream, false });
                using (renderBatchWriter as IDisposable)
                {
                    _writeMethod.Invoke(renderBatchWriter, new object[] { renderBatch });
                }

                var batchBytes = memoryStream.ToArray();
                base64 = Convert.ToBase64String(batchBytes);
            }

            _ipc.Send("JS.RenderBatch", _rendererId, base64);

            // TODO: Consider finding a way to get back a completion message from the Desktop side
            // in case there was an error. We don't really need to wait for anything to happen, since
            // this is not prerendering and we don't care how quickly the UI is updated, but it would
            // be desirable to flow back errors.
            return Task.CompletedTask;
        }

        private class RenderFragmentComponent : IComponent
        {
            public RenderHandle RenderHandle { get; private set; }

            public void Attach(RenderHandle renderHandle)
                => RenderHandle = renderHandle;

            public Task SetParametersAsync(ParameterView parameters)
                => Task.CompletedTask;
        }
    }
#pragma warning restore BL0006 // Do not use RenderTree types
}