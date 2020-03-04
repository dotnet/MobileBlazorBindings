// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Core
{
    /// <summary>
    /// Interface to indicate that this element is a container of elements that are not
    /// true children of their parent. For example, a host for elements that go in a modal dialog
    /// are not true children of their parent.
    /// </summary>
    public interface INonChildContainerElement
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
    }
}
