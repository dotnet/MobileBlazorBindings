// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <summary>
    /// Assists in configuring and registering events for elements. Events must first be configured by a handler
    /// to specify the action to take when attaching and detaching events. Then events can be registered to
    /// indicate that an event has been raised.
    /// </summary>
    public class EventManager
    {
        private readonly Dictionary<string, EventRegistration> _configuredEvents = new Dictionary<string, EventRegistration>();

        /// <summary>
        /// Configures an event for use in a handler.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="setId"></param>
        /// <param name="clearId"></param>
        public void ConfigureEvent(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            _configuredEvents[eventName] = new EventRegistration(eventName, setId, clearId);
        }

        /// <summary>
        /// Tries to register an event handler that will be attached to the native control.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="eventName"></param>
        /// <param name="eventHandlerId"></param>
        /// <returns></returns>
        public bool TryRegisterEvent(NativeComponentRenderer renderer, string eventName, ulong eventHandlerId)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            if (_configuredEvents.TryGetValue(eventName, out var eventRegistration))
            {
                renderer.RegisterEvent(eventHandlerId, eventRegistration.ClearId);
                eventRegistration.SetId(eventHandlerId);

                return true;
            }
            return false;
        }
    }
}
