// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class TabbedPageHandler : PageHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsPage = child as MC.Page;

            if (physicalSiblingIndex <= TabbedPageControl.Children.Count)
            {
                TabbedPageControl.Children.Insert(physicalSiblingIndex, childAsPage);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but TabbedPageControl.Children.Count={TabbedPageControl.Children.Count}");
                TabbedPageControl.Children.Add(childAsPage);
            }
        }

        public int GetChildIndex(MC.Element child)
        {
            return TabbedPageControl.Children.IndexOf(child as MC.Page);
        }

        public virtual void RemoveChild(MC.Element child)
        {
            TabbedPageControl.Children.Remove(child as MC.Page);
        }
    }
}
