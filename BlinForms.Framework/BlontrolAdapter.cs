using System;
using System.Diagnostics;
using System.Windows.Forms;
using Emblazon;

namespace BlinForms.Framework
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live WinForms control tree.
    /// </summary>
    public class BlontrolAdapter : EmblazonAdapter<Control>, IDisposable
    {
        public BlontrolAdapter()
        {
        }

        protected override void RemoveChildControl(EmblazonAdapter<Control> child)
        {
            TargetControl.Controls.Remove(child.TargetControl);
        }

        protected override EmblazonAdapter<Control> CreateAdapter()
        {
            return new BlontrolAdapter();
        }

        protected override bool IsChildControlParented(Control nativeChild)
        {
            return nativeChild.Parent != null;
        }

        protected override void AddChildControl(Control parentControl, int siblingIndex, Control childControl)
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
                Debug.WriteLine($"WARNING: {nameof(AddChildControl)} called with {nameof(siblingIndex)}={siblingIndex}, but parentControl.Controls.Count={parentControl.Controls.Count}");
                parentControl.Controls.Add(childControl);
            }
        }

        void IDisposable.Dispose()
        {
            TargetControl.Dispose();
        }
    }
}
