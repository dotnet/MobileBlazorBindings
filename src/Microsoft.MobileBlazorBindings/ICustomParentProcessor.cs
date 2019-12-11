// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public interface ICustomParentProcessor
    {
        void SetParent(object parent);
        bool IsParented();
        bool IsParentedTo(Element elementControl);
    }
}
