using Emblazon;
using System.Diagnostics;

namespace BlinForms.Framework
{
    internal class BlinFormsElementManager : ElementManager<IWindowsFormsControlHandler>
    {
        public override void RemoveElement(IWindowsFormsControlHandler control)
        {
            control.Control.Parent.Controls.Remove(control.Control);
        }

        public override void AddChildElement(IWindowsFormsControlHandler parentControl, IWindowsFormsControlHandler childControl, int physicalSiblingIndex)
        {
            if (physicalSiblingIndex <= parentControl.Control.Controls.Count)
            {
                // WinForms ControlCollection doesn't support Insert(), so add the new child at the end,
                // and then re-order the collection to move the control to the correct index.
                parentControl.Control.Controls.Add(childControl.Control);
                parentControl.Control.Controls.SetChildIndex(childControl.Control, physicalSiblingIndex);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentControl.Controls.Count={parentControl.Control.Controls.Count}");
                parentControl.Control.Controls.Add(childControl.Control);
            }
        }

        public override int GetPhysicalSiblingIndex(IWindowsFormsControlHandler control)
        {
            return control.Control.Parent.Controls.GetChildIndex(control.Control);
        }

        public override bool IsParented(IWindowsFormsControlHandler nativeControl)
        {
            return nativeControl.Control.Parent != null;
        }

        public override bool IsParentOfChild(IWindowsFormsControlHandler parentControl, IWindowsFormsControlHandler childControl)
        {
            return parentControl.Control.Contains(childControl.Control);
        }
    }
}
