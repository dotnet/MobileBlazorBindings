using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlazorDesktop
{
#pragma warning disable BL0006 // Do not use RenderTree types
    internal class DesktopRenderer : Renderer
    {
        private readonly IPC _ipc;
        private readonly Dispatcher _dispatcher;

        public DesktopRenderer(IPC ipc, IServiceProvider serviceProvider, ILoggerFactory loggerFactory, Dispatcher dispatcher)
            : base(serviceProvider, loggerFactory)
        {
            _ipc = ipc ?? throw new ArgumentNullException(nameof(ipc));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            
            var rootComponent = new RenderFragmentComponent();
            AssignRootComponentId(rootComponent);
            RootRenderHandle = rootComponent.RenderHandle;
        }

        public RenderHandle RootRenderHandle { get; }

        public override Dispatcher Dispatcher => _dispatcher;

        protected override void HandleException(Exception exception)
        {
            throw exception;
        }

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            throw new NotImplementedException();
        }

        class RenderFragmentComponent : IComponent
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