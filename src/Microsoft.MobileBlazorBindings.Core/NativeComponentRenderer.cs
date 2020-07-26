// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Core
{
    public abstract class NativeComponentRenderer : Renderer
    {
        private readonly Dictionary<int, NativeComponentAdapter> _componentIdToAdapter = new Dictionary<int, NativeComponentAdapter>();
        private ElementManager _elementManager;
        private readonly Dictionary<ulong, Action<ulong>> _eventRegistrations = new Dictionary<ulong, Action<ulong>>();


        public NativeComponentRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        protected abstract ElementManager CreateNativeControlManager();

        internal ElementManager ElementManager
        {
            get
            {
                return _elementManager ?? (_elementManager = CreateNativeControlManager());
            }
        }

        public override Dispatcher Dispatcher { get; }
             = Dispatcher.CreateDefault();

        /// <summary>
        /// Creates a component of type <typeparamref name="TComponent"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public async Task AddComponent<TComponent>(IElementHandler parent) where TComponent : IComponent
        {
            await AddComponent(typeof(TComponent), parent).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a component of type <paramref name="componentType"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public async Task<IComponent> AddComponent(Type componentType, IElementHandler parent, Dictionary<string, string> parameters = null)
        {
            IComponent component = null;

            //I think only a few lines of this need to happen inside the dispatcher
            //should some bits be brought out?
            //TODO: Try setting configurea awail false, this may open up options for calling this sync/async on startup
            await Dispatcher.InvokeAsync(async () =>
            {
                //This is where we create the component
                component = InstantiateComponent(componentType);
                var componentId = AssignRootComponentId(component);

                var rootAdapter = new NativeComponentAdapter(this, closestPhysicalParent: parent, knownTargetElement: parent)
                {
                    Name = $"RootAdapter attached to {parent.GetType().FullName}",
                };

                _componentIdToAdapter[componentId] = rootAdapter;
                
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        //Think about the parameter attribute, this will set any property even if not marked
                        var prop = component.GetType().GetProperty(parameter.Key);

                        if (prop != null)
                        {
                            try
                            {
                                prop.SetValue(component, parameter.Value);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"failed to set parameter {ex} \n{ex.StackTrace}");
                            }
                        }
                    }
                }
                
                


                //This is where we render the component, parameters need to be set befiore this
                await RenderRootComponentAsync(componentId).ConfigureAwait(false);
            }).ConfigureAwait(false);
            return component;
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
                for (var i = 0; i < numDisposeEventHandlers; i++)
                {
                    DisposeEvent(renderBatch.DisposedEventHandlerIDs.Array[i]);
                }
            }

            return Task.CompletedTask;
        }

        public void RegisterEvent(ulong eventHandlerId, Action<ulong> unregisterCallback)
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
            unregisterCallback(eventHandlerId);
        }

        internal NativeComponentAdapter CreateAdapterForChildComponent(IElementHandler physicalParent, int componentId)
        {
            var result = new NativeComponentAdapter(this, physicalParent);
            _componentIdToAdapter[componentId] = result;
            return result;
        }
    }
}
