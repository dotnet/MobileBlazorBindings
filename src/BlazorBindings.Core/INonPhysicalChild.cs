// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core
{
    /// <summary>
    /// Represents that the given handler does not represent a physical child element.
    /// This is often used on handlers for elements that contain metadata that is applied
    /// to a parent rather than a child control.
    /// </summary>
    public interface INonPhysicalChild
    {
        /// <summary>
        /// This is called when this component would otherwise be added to a parent container. Instead
        /// of adding this to a parent container, this method is called, so that this component can track
        /// which element would have been its parent. This is useful so that this component can use the
        /// parent component for any children it might have (that is, delegate parenting of its children to
        /// its parent).
        /// </summary>
        /// <param name="parentElement"></param>
        void SetParent(object parentElement);

        /// <summary>
        /// This is called when this component would otherwise be removed from a parent container.
        /// This is useful so that this component can unapply its effects from parent element.
        /// </summary>
        void Remove();
    }
}
