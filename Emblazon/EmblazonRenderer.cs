using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emblazon
{
    public abstract class EmblazonRenderer<TComponentHandler> : Renderer where TComponentHandler : class, INativeControlHandler
    {
        private readonly Dictionary<int, EmblazonAdapter<TComponentHandler>> _componentIdToAdapter = new Dictionary<int, EmblazonAdapter<TComponentHandler>>();
        private NativeControlManager<TComponentHandler> _nativeControlManager;
        private readonly Dictionary<ulong, Action> _eventRegistrations = new Dictionary<ulong, Action>();


        public EmblazonRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        protected abstract NativeControlManager<TComponentHandler> CreateNativeControlManager();

        internal NativeControlManager<TComponentHandler> NativeControlManager => _nativeControlManager ?? (_nativeControlManager = CreateNativeControlManager());

        public override Dispatcher Dispatcher { get; }
             = Dispatcher.CreateDefault();

        /// <summary>
        /// Creates a component of type <typeparamref name="TComponent"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public async Task AddComponent<TComponent>(TComponentHandler parent) where TComponent : IComponent
        {
            await AddComponent(typeof(TComponent), parent);
        }

        /// <summary>
        /// Creates a component of type <paramref name="componentType"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public async Task AddComponent(Type componentType, TComponentHandler parent)
        {
            var component = InstantiateComponent(componentType);
            var componentId = AssignRootComponentId(component);

            var rootAdapter = new EmblazonAdapter<TComponentHandler>(this, closestPhysicalParent: parent, knownTargetControl: parent)
            {
                Name = "RootAdapter"
            };

            _componentIdToAdapter[componentId] = rootAdapter;
            await RenderRootComponentAsync(componentId);
        }

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

            var numDisposeEventHandlers = renderBatch.DisposedEventHandlerIDs.Count;
            if (numDisposeEventHandlers != 0)
            {
                for (int i = 0; i < numDisposeEventHandlers; i++)
                {
                    DisposeEvent(renderBatch.DisposedEventHandlerIDs.Array[i]);
                }
            }

            return Task.CompletedTask;
        }

        public void RegisterEvent(ulong eventHandlerId, Action unregisterCallback)
        {
            if (eventHandlerId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(eventHandlerId), "Event handler ID must not be 0.");
            }
            if (unregisterCallback == null)
            {
                throw new ArgumentNullException(nameof(unregisterCallback));
            }
            _eventRegistrations.Add(eventHandlerId, unregisterCallback);
        }

        private void DisposeEvent(ulong eventHandlerId)
        {
            if (!_eventRegistrations.TryGetValue(eventHandlerId, out var unregisterCallback))
            {
                throw new InvalidOperationException($"Attempting to dispose unknown event handler id '{eventHandlerId}'.");
            }
            unregisterCallback();
        }

        internal EmblazonAdapter<TComponentHandler> CreateAdapterForChildComponent(TComponentHandler physicalParent, int componentId)
        {
            var result = new EmblazonAdapter<TComponentHandler>(this, physicalParent);
            _componentIdToAdapter[componentId] = result;
            return result;
        }
    }
}
