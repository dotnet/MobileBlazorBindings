using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
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
            var indexOfAdapterInParentContainer = Parent.Children.IndexOf(this);

            // Calculate the actual desired sibling index based on the current adapter tree and mapping it
            // to the native control tree.
            int actualSiblingIndex;

            if (indexOfAdapterInParentContainer == 0)
            {
                // If this adapter is the first in its container, the child control should be the first control in its container
                actualSiblingIndex = 0;
            }
            else
            {
                // If this adapter has previous siblings, find out the nearest previous sibling that has a target control
                // and find the index of that target control in its container. The new control being added here needs to come
                // immediately after that target control.

                var previousSiblingAdapterWithTargetControlInParentContainer =
                    Parent.Children
                    .Take(indexOfAdapterInParentContainer)
                    .Reverse()
                    .FirstOrDefault(adapter => adapter.TargetControl != null);

                if (previousSiblingAdapterWithTargetControlInParentContainer != null)
                {
                    var indexOfPreviousSiblingTargetControlInParentContainer =
                    parentControl.Controls.IndexOf(previousSiblingAdapterWithTargetControlInParentContainer.TargetControl);

                    actualSiblingIndex = indexOfPreviousSiblingTargetControlInParentContainer + 1;
                }
                else
                {
                    // TODO: Need to determine what gets into this state, and what the sibling index should be
                    actualSiblingIndex = 0;
                }
            }

            if (actualSiblingIndex <= parentControl.Controls.Count)
            {
                // WinForms ControlCollection doesn't support Insert(), so add the new child at the end,
                // and then re-order the collection to move the control to the correct index.
                parentControl.Controls.Add(childControl);
                parentControl.Controls.SetChildIndex(childControl, actualSiblingIndex);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChildControl)} called with {nameof(actualSiblingIndex)}={actualSiblingIndex}, but parentControl.Controls.Count={parentControl.Controls.Count}");
                parentControl.Controls.Add(childControl);
            }
        }

        void IDisposable.Dispose()
        {
            TargetControl.Dispose();
        }
    }
}
