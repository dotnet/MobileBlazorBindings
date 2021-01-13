// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class LayoutHandler : ViewHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as XF.View;

            var layoutControlOfView = LayoutControl as XF.Layout<XF.View>;

            if (physicalSiblingIndex <= layoutControlOfView.Children.Count)
            {
                layoutControlOfView.Children.Insert(physicalSiblingIndex, childAsView);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but layoutControlOfView.Children.Count={layoutControlOfView.Children.Count}");
                layoutControlOfView.Children.Add(childAsView);
            }
        }

        public virtual int GetChildIndex(XF.Element child)
        {
            var layoutControlOfView = (XF.Layout<XF.View>)LayoutControl;
            var childAsView = child as XF.View;

            return layoutControlOfView.Children.IndexOf(childAsView);
        }

        public virtual void RemoveChild(XF.Element child)
        {
            var layoutControlOfView = (XF.Layout<XF.View>)LayoutControl;
            var childAsView = child as XF.View;

            layoutControlOfView.Children.Remove(childAsView);
        }
    }
}
