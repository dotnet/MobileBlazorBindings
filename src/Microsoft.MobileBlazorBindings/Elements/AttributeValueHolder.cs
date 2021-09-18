// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Elements
{
    /// <summary>
    /// Delegate used to transport data of non-primitive types from the markup to the handlers.
    /// Blazor supports serialization of only a limited set of types, so for more complex types,
    /// the data is wrapped in a delegate of this type, which can contain logic to return and type.
    /// Then the handler invokes that delegate to retrieve the original data.
    /// </summary>
    /// <returns>The data that this delegate represents.</returns>
    public delegate object AttributeValueHolder();
}
