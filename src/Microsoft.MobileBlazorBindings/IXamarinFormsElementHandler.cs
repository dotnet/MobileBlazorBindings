// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings
{
    public interface IMauiElementHandler : IElementHandler
    {
        Maui.Controls.Element ElementControl { get; }

        bool IsParented();
        bool IsParentedTo(Maui.Controls.Element parent);
        void SetParent(Maui.Controls.Element parent);
    }
}
