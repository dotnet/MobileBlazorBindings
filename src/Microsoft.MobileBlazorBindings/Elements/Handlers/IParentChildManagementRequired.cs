// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    /// <summary>
    /// Indicates that the <see cref="IXamarinFormsElementHandler"/> implementing this interface has
    /// custom behavior for managing parent/child elements and that behavior should be delegated to
    /// the <see cref="ParentChildManager"/> implementation.
    /// </summary>
    public interface IParentChildManagementRequired
    {
        IParentChildManager ParentChildManager { get; }
    }
}
