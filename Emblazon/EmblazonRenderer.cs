using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emblazon
{
    public abstract class EmblazonRenderer<TNativeComponent> : Renderer where TNativeComponent : class
    {
        private readonly Dictionary<int, EmblazonAdapter<TNativeComponent>> _componentIdToAdapter = new Dictionary<int, EmblazonAdapter<TNativeComponent>>();

        public EmblazonRenderer(IServiceProvider serviceProvider)
            : base(serviceProvider, new LoggerFactory())
        {
        }

        public override Dispatcher Dispatcher { get; }
             = Dispatcher.CreateDefault();


        public Task AddComponent<T>() where T : IComponent
        {
            var component = InstantiateComponent(typeof(T));
            var componentId = AssignRootComponentId(component);
            var adapter = CreateAdapter();
            adapter.Name = "Root BlontrolAdapter";
            adapter.SetRenderer(this);

            InitializeRootAdapter(adapter);

            _componentIdToAdapter[componentId] = adapter;
            return RenderRootComponentAsync(componentId);
        }

        protected abstract void InitializeRootAdapter(EmblazonAdapter<TNativeComponent> adapter);

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            foreach (var updatedComponent in renderBatch.UpdatedComponents.Array.Take(renderBatch.UpdatedComponents.Count))
            {
                var adapter = _componentIdToAdapter[updatedComponent.ComponentId];
                adapter.ApplyEdits(updatedComponent.ComponentId, updatedComponent.Edits, renderBatch.ReferenceFrames, renderBatch);
            }

            var numDisposedComponents = renderBatch.DisposedComponentIDs.Count;
            for (var i = 0; i < numDisposedComponents; i++)
            {
                var disposedComponentId = renderBatch.DisposedComponentIDs.Array[i];
                if (_componentIdToAdapter.TryGetValue(disposedComponentId, out var adapter))
                {
                    _componentIdToAdapter.Remove(disposedComponentId);
                    (adapter as IDisposable)?.Dispose();
                }
            }

            if (renderBatch.DisposedEventHandlerIDs.Count != 0)
            {
                // TODO: Support this
                throw new NotSupportedException("Disposing event handlers is not yet supported.");
            }

            return Task.CompletedTask;
        }

        internal EmblazonAdapter<TNativeComponent> CreateAdapterForChildComponent(int componentId)
        {
            var result = CreateAdapter();
            result.SetRenderer(this);
            _componentIdToAdapter[componentId] = result;
            return result;
        }

        protected abstract EmblazonAdapter<TNativeComponent> CreateAdapter();
    }
}
