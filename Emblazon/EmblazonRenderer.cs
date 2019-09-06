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


        public EmblazonRenderer(IServiceProvider serviceProvider)
            : base(serviceProvider, new LoggerFactory())
        {
        }

        protected abstract NativeControlManager<TComponentHandler> CreateNativeControlManager();

        internal NativeControlManager<TComponentHandler> NativeControlManager => _nativeControlManager ?? (_nativeControlManager = CreateNativeControlManager());

        public override Dispatcher Dispatcher { get; }
             = Dispatcher.CreateDefault();

        public Task AddComponent<T>() where T : IComponent
        {
            var component = InstantiateComponent(typeof(T));
            var componentId = AssignRootComponentId(component);
            var rootControl = CreateRootControl();
<<<<<<< HEAD
            var rootAdapter = new EmblazonAdapter<TComponentHandler>(rootControl);
            rootAdapter.Name = "RootAdapter";
            rootAdapter.SetRenderer(this);
=======
            var rootAdapter = new EmblazonAdapter<TNativeComponent>(this, closestPhysicalParent: rootControl, knownTargetControl: rootControl)
            {
                Name = "RootAdapter"
            };
>>>>>>> abd3d233d47af015f86ca491d516b9604c8d9953

            _componentIdToAdapter[componentId] = rootAdapter;
            return RenderRootComponentAsync(componentId);
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
<<<<<<< HEAD
            var result = new EmblazonAdapter<TComponentHandler>(physicalParent);
            result.SetRenderer(this);
=======
            var result = new EmblazonAdapter<TNativeComponent>(this, physicalParent);
>>>>>>> abd3d233d47af015f86ca491d516b9604c8d9953
            _componentIdToAdapter[componentId] = result;
            return result;
        }

        protected abstract TComponentHandler CreateRootControl();
    }
}
