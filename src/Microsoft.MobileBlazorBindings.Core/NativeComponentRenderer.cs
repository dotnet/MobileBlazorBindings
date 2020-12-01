// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// <returns></returns>
        public async Task AddComponent<TComponent>(IElementHandler parent) where TComponent : IComponent
        {
            await AddComponent(typeof(TComponent), parent).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a component of type <paramref name="componentType"/> and adds it as a child of <paramref name="parent"/>. If parameters are provided they will be set on the component.
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="parent"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IComponent> AddComponent(Type componentType, IElementHandler parent, Dictionary<string, string> parameters = null)
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

                    SetNavigationParameters(component, parameters);

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

        public static void SetNavigationParameters(IComponent component, Dictionary<string, string> parameters)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            if (parameters == null || parameters.Count == 0)
            {
                //parameters will often be null. e.g. if you navigate with no parameters or when creating a root component.
                return;
            }

            foreach (var parameter in parameters)
            {
                var prop = component.GetType().GetProperty(parameter.Key);

                if (prop != null)
                {
                    var parameterAttribute = prop.GetCustomAttribute(typeof(ParameterAttribute));
                    if (parameterAttribute == null)
                    {
                        throw new InvalidOperationException($"Object of type '{component.GetType()}' has a property matching the name '{parameter.Key}', but it does not have [ParameterAttribute] or [CascadingParameterAttribute] applied.");
                    }

                    if (TryParse(prop.PropertyType, parameter.Value, out var result))
                    {
                        prop.SetValue(component, result);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unable to set property {parameter.Key} on object of type '{component.GetType()}'.The value {parameter.Value}. can not be converted to a {prop.PropertyType.Name}");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Object of type '{component.GetType()}' does not have a property matching the name '{parameter.Key}'.");
                }
            }
        }

        /// <summary>
        /// Converts a string into the specified type. If conversion was successful, parsed property will be of the correct type and method will return true.
        /// If conversion fails it will return false and parsed property will be null.
        /// This method supports the 8 data types that are valid navigation parameters in Blazor. Passing a string is also safe but will be returned as is because no conversion is neccessary.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="s"></param>
        /// <param name="result">The parsed object of the type specified. This will be null if conversion failed.</param>
        /// <returns>True if s was converted successfully, otherwise false</returns>
        public static bool TryParse(Type type, string s, out object result)
        {
            bool success;

            if (type == typeof(string))
            {
                result = s;
                success = true;
            }
            else if (type == typeof(int))
            {
                success = int.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(Guid))
            {
                success = Guid.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(bool))
            {
                success = bool.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(DateTime))
            {
                success = DateTime.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(decimal))
            {
                success = decimal.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(double))
            {
                success = double.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(float))
            {
                success = float.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(long))
            {
                success = long.TryParse(s, out var parsed);
                result = parsed;
            }
            else
            {
                result = null;
                success = false;
            }
            return success;
        }
    }
}
