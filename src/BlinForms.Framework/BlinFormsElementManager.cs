// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System.Diagnostics;

namespace BlinForms.Framework
{
    internal class BlinFormsElementManager : ElementManager<IWindowsFormsControlHandler>
    {
        protected override void RemoveChildElement(IWindowsFormsControlHandler parentHandler, IWindowsFormsControlHandler childHandler)
        {
            parentHandler.Control.Controls.Remove(childHandler.Control);
        }

        protected override void AddChildElement(IWindowsFormsControlHandler parentHandler, IWindowsFormsControlHandler childHandler, int physicalSiblingIndex)
        {
            if (physicalSiblingIndex <= parentHandler.Control.Controls.Count)
            {
                // WinForms ControlCollection doesn't support Insert(), so add the new child at the end,
                // and then re-order the collection to move the control to the correct index.
                parentHandler.Control.Controls.Add(childHandler.Control);
                parentHandler.Control.Controls.SetChildIndex(childHandler.Control, physicalSiblingIndex);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentControl.Controls.Count={parentHandler.Control.Controls.Count}");
                parentHandler.Control.Controls.Add(childHandler.Control);
            }
        }

        protected override int GetChildElementIndex(IWindowsFormsControlHandler parentHandler, IWindowsFormsControlHandler childHandler)
        {
            return parentHandler.Control.Controls.GetChildIndex(childHandler.Control);
        }

        protected override bool IsParented(IWindowsFormsControlHandler handler)
        {
            return handler.Control.Parent != null;
        }

        protected override bool IsParentOfChild(IWindowsFormsControlHandler parentHandler, IWindowsFormsControlHandler childHandler)
        {
            return parentHandler.Control.Contains(childHandler.Control);
        }
    }
}
