// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core
{
    /// <summary>
    /// Interface to indicate that this element is a container of elements that are not
    /// true children of their parent. For example, a host for elements that go in a modal dialog
    /// are not true children of their parent.
    /// </summary>
    public interface INonChildContainerElement : INonPhysicalChild
    {
    }
}
