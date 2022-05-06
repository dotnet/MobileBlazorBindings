// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    internal class EventRegistration
    {
        public EventRegistration(string eventName, Action<ulong> setId, Action<ulong> clearId)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentException("message", nameof(eventName));
            }

            EventName = eventName;
            SetId = setId ?? throw new ArgumentNullException(nameof(setId));
            ClearId = clearId ?? throw new ArgumentNullException(nameof(clearId));
        }

        public string EventName { get; }
        public Action<ulong> SetId { get; }
        public Action<ulong> ClearId { get; }
    }
}
