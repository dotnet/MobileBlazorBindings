using System;
using System.Diagnostics;
using System.Windows.Forms;
using Emblazon;

namespace BlinForms.Framework
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live WinForms control tree.
    /// </summary>
    public class BlontrolAdapter : EmblazonAdapter<Control>
    {
        // Notice how all the methods here could be static (none of them operate on any members
        // of BlontrolAdapter). This suggests that EmblazonAdapter should instead be a private
        // implementation detail of Emblazon, not something subclassed for each UI tech. Instead,
        // each UI tech should provide a singleton-like implementation of some interface that
        // contains these methods. That service only needs to know how to update the physical tree
        // given parent/child/index params and doesn't need any concept of adapters.

        public BlontrolAdapter(Control physicalParent) : base(physicalParent)
        {
        }

        public override void RemovePhysicalControl(Control control)
        {
            control.Parent.Controls.Remove(control);
        }

        protected override EmblazonAdapter<Control> CreateAdapter(Control physicalParent)
        {
            return new BlontrolAdapter(physicalParent);
        }

        public override void AddPhysicalControl(Control parentControl, Control childControl, int siblingIndex)
        {
            if (siblingIndex <= parentControl.Controls.Count)
            {
                // WinForms ControlCollection doesn't support Insert(), so add the new child at the end,
                // and then re-order the collection to move the control to the correct index.
                parentControl.Controls.Add(childControl);
                parentControl.Controls.SetChildIndex(childControl, siblingIndex);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddPhysicalControl)} called with {nameof(siblingIndex)}={siblingIndex}, but parentControl.Controls.Count={parentControl.Controls.Count}");
                parentControl.Controls.Add(childControl);
            }
        }

        public override int GetPhysicalSiblingIndex(Control control)
        {
            return control.Parent.Controls.GetChildIndex(control);
        }

        protected override bool IsParented(Control nativeControl)
        {
            return nativeControl.Parent != null;
        }
    }
}
