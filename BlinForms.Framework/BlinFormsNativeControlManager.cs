using Emblazon;
using System.Diagnostics;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    internal class BlinFormsNativeControlManager : NativeControlManager<Control>
    {
        public override void RemovePhysicalControl(Control control)
        {
            control.Parent.Controls.Remove(control);
        }

        public override void AddPhysicalControl(Control parentControl, Control childControl, int physicalSiblingIndex)
        {
            if (physicalSiblingIndex <= parentControl.Controls.Count)
            {
                // WinForms ControlCollection doesn't support Insert(), so add the new child at the end,
                // and then re-order the collection to move the control to the correct index.
                parentControl.Controls.Add(childControl);
                parentControl.Controls.SetChildIndex(childControl, physicalSiblingIndex);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddPhysicalControl)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentControl.Controls.Count={parentControl.Controls.Count}");
                parentControl.Controls.Add(childControl);
            }
        }

        public override int GetPhysicalSiblingIndex(Control control)
        {
            return control.Parent.Controls.GetChildIndex(control);
        }

        public override bool IsParented(Control nativeControl)
        {
            return nativeControl.Parent != null;
        }

        public override bool IsParentOfChild(Control parentControl, Control childControl)
        {
            return parentControl.Contains(childControl);
        }
    }
}
