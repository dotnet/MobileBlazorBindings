// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public interface IMauiContainerElementHandler : IMauiElementHandler
    {
        void AddChild(MC.Element child, int physicalSiblingIndex);
        void RemoveChild(MC.Element child);
        int GetChildIndex(MC.Element child);
    }
}
