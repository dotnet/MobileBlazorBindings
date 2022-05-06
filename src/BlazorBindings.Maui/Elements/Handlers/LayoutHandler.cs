// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using System.Diagnostics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract partial class LayoutHandler : ViewHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as IView;

            if (physicalSiblingIndex <= LayoutControl.Children.Count)
            {
                LayoutControl.Children.Insert(physicalSiblingIndex, childAsView);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but layoutControlOfView.Children.Count={LayoutControl.Children.Count}");
                LayoutControl.Children.Add(childAsView);
            }
        }

        public virtual int GetChildIndex(MC.Element child)
        {
            var childAsView = child as IView;
            return LayoutControl.Children.IndexOf(childAsView);
        }

        public virtual void RemoveChild(MC.Element child)
        {
            var childAsView = child as IView;

            LayoutControl.Children.Remove(childAsView);
        }
    }
}
