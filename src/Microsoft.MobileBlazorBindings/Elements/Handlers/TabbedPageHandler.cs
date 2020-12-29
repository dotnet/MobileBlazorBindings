// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TabbedPageHandler : PageHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsPage = child as XF.Page;

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

        public int GetChildIndex(XF.Element child)
        {
            return TabbedPageControl.Children.IndexOf(child as XF.Page);
        }

        public virtual void RemoveChild(XF.Element child)
        {
            TabbedPageControl.Children.Remove(child as XF.Page);
        }
    }
}
