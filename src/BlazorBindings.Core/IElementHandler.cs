// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core
{
    /// <summary>
    /// Represents a container for native element.
    /// </summary>
    public interface IElementHandler
    {
        /// <summary>
        /// Sets an attribute named <paramref name="attributeName"/> on the <see cref="TargetElement"/> represented by
        /// this handler to value <paramref name="attributeValue"/>.
        /// </summary>
        /// <param name="attributeEventHandlerId"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="attributeEventUpdatesAttributeName"></param>
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);

        /// <summary>
        /// The native element represented by this handler. This is often a native UI component, but can be any type
        /// of component used by the native system.
        /// </summary>
        object TargetElement { get; }
    }
}
