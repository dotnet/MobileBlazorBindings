// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ContentPageHandler : TemplatedPageHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as MC.View;
            ContentPageControl.Content = childAsView;
        }

        public int GetChildIndex(MC.Element child)
        {
            return ContentPageControl.Content == child ? 0 : -1;
        }

        public virtual void RemoveChild(MC.Element child)
        {
            if (ContentPageControl.Content == child)
            {
                ContentPageControl.Content = null;
            }
        }
    }
}
