// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ContentViewHandler : TemplatedViewHandler, IMauiContainerElementHandler
    {
        public void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as MC.View;
            ContentViewControl.Content = childAsView;
        }

        public int GetChildIndex(MC.Element child)
        {
            return ContentViewControl.Content == child ? 0 : -1;
        }

        public void RemoveChild(MC.Element child)
        {
            if (ContentViewControl.Content == child)
            {
                ContentViewControl.Content = null;
            }
        }
    }
}
