using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    internal class WebViewRenderer : Renderer
    {
        public WebViewRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher => throw new NotImplementedException();

        public Task AddRootComponentAsync(Type componentType, string domElementSelector, ParameterView parameters)
        {
            var component = InstantiateComponent(componentType);
            var componentId = AssignRootComponentId(component);

            // TODO: At this point, notify the JS runtime that it should associate
            // componentId with selector so the following renderbatch will work

            return RenderRootComponentAsync(componentId, parameters);
        }

        protected override void HandleException(Exception exception)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            throw new NotImplementedException();
        }
    }
}
