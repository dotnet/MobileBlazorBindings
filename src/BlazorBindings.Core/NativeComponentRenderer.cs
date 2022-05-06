// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorBindings.Core
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
                return _elementManager ??= CreateNativeControlManager();
            }
        }

        public override Dispatcher Dispatcher { get; }
             = Dispatcher.CreateDefault();

        /// <summary>
        /// Creates a component of type <typeparamref name="TComponent"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<TComponent> AddComponent<TComponent>(IElementHandler parent, Dictionary<string, object> parameters = null) where TComponent : IComponent
        {
            return (TComponent)await AddComponent(typeof(TComponent), parent, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a component of type <paramref name="componentType"/> and adds it as a child of <paramref name="parent"/>. If parameters are provided they will be set on the component.
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="parent"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IComponent> AddComponent(Type componentType, IElementHandler parent, Dictionary<string, object> parameters = null)
        {
            try
            {
                return await Dispatcher.InvokeAsync(async () =>
                {
                    var component = InstantiateComponent(componentType);
                    var componentId = AssignRootComponentId(component);

                    var rootAdapter = new NativeComponentAdapter(this, closestPhysicalParent: parent, knownTargetElement: parent)
                    {
                        Name = $"RootAdapter attached to {parent.GetType().FullName}",
                    };

                    _componentIdToAdapter[componentId] = rootAdapter;

                    SetParameterArguments(component, parameters);

                    await RenderRootComponentAsync(componentId).ConfigureAwait(false);
                    return component;
                }).ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                HandleException(ex);
                return null;
            }
        }

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            HashSet<int> processedComponentIds = new HashSet<int>();

            var numUpdatedComponents = renderBatch.UpdatedComponents.Count;
            for (var componentIndex = 0; componentIndex < numUpdatedComponents; componentIndex++)
            {
                var updatedComponent = renderBatch.UpdatedComponents.Array[componentIndex];

                // If UpdatedComponent is already processed (due to recursive ApplyEdits) - skip it.
                if (updatedComponent.Edits.Count > 0 && !processedComponentIds.Contains(updatedComponent.ComponentId))
                {
                    var adapter = _componentIdToAdapter[updatedComponent.ComponentId];
                    adapter.ApplyEdits(updatedComponent.ComponentId, updatedComponent.Edits, renderBatch.ReferenceFrames, renderBatch, processedComponentIds);
                }
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

        internal static void SetParameterArguments(IComponent component, Dictionary<string, object> arguments)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            if (arguments == null || arguments.Count == 0)
            {
                //parameters will often be null. e.g. if you navigate with no parameters or when creating a root component.
                return;
            }

            foreach (var parameter in arguments)
            {
                var prop = component.GetType().GetProperty(parameter.Key);

                if (prop == null)
                {
                    throw new InvalidOperationException($"Object of type '{component.GetType()}' does not have a property matching the name '{parameter.Key}'.");
                }

                var parameterAttribute = prop.GetCustomAttribute(typeof(ParameterAttribute));
                if (parameterAttribute == null)
                {
                    throw new InvalidOperationException($"Object of type '{component.GetType()}' has a property matching the name '{parameter.Key}', but it does not have [ParameterAttribute] or [CascadingParameterAttribute] applied.");
                }

                prop.SetValue(component, parameter.Value);
            }
        }
    }
}
