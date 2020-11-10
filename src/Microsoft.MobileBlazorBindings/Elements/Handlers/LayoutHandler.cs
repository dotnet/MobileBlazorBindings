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

        public virtual void RemoveChild(XF.Element child)
        {
            var childAsView = child as XF.View;

            // TODO: Had to add a switch statement to handle ContentView case.
            // as I'm not very familiar with native controls, this probably needs
            // a better fix.
            switch (LayoutControl)
            {
                case XF.Layout<XF.View> layoutControlOfView:
                    layoutControlOfView.Children.Remove(childAsView);
                    break;
            }
        }
    }
}
