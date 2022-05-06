// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;

namespace BlazorBindings.Maui
{
    public interface IMauiElementHandler : IElementHandler
    {
        Microsoft.Maui.Controls.Element ElementControl { get; }

        bool IsParented();
        bool IsParentedTo(Microsoft.Maui.Controls.Element parent);
        void SetParent(Microsoft.Maui.Controls.Element parent);
    }
}
