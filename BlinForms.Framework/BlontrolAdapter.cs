using BlinForms.Framework.Controls;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live WinForms control tree.
    /// </summary>
    public class BlontrolAdapter
    {
        internal static Dictionary<string, Func<BlinFormsRenderer, Control>> KnownElements { get; }
            = new Dictionary<string, Func<BlinFormsRenderer, Control>>();

        public BlontrolAdapter(BlinFormsRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public BlontrolAdapter Parent { get; set; }
        public List<BlontrolAdapter> Children { get; } = new List<BlontrolAdapter>();

        // TODO: Is this the right concept? Can a component have multiple WinForms controls created?
        public Control TargetControl { get; set; }

        public BlinFormsRenderer Renderer { get; }

        /// <summary>
        /// Used for debugging purposes.
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Blontrol: Name={Name ?? "<?>"}, Target={TargetControl?.GetType().Name ?? "<?>"}, #Children={Children.Count}";
        }

        internal void ApplyEdits(int componentId, ArrayBuilderSegment<RenderTreeEdit> edits, ArrayRange<RenderTreeFrame> referenceFrames, RenderBatch batch)
        {
            foreach (var edit in edits)
            {
                switch (edit.Type)
                {
                    case RenderTreeEditType.PrependFrame:
                        ApplyPrependFrame(batch, componentId, edit.SiblingIndex, referenceFrames.Array, edit.ReferenceFrameIndex);
                        break;
                    case RenderTreeEditType.SetAttribute:
                        ApplySetAttribute(ref referenceFrames.Array[edit.ReferenceFrameIndex]);
                        break;
                    case RenderTreeEditType.RemoveFrame:
                        ApplyRemoveFrame(edit.SiblingIndex);
                        break;
                    default:
                        throw new NotImplementedException($"Not supported edit type: {edit.Type}");
                }
            }
        }

        private void ApplyRemoveFrame(int siblingIndex)
        {
            var childToRemove = Children[siblingIndex];

            // If there's a target control for the child adapter, remove it from the live control tree.
            // Not all adapters have target controls; Adapters for markup/text have no associated native control.
            if (childToRemove.TargetControl != null)
            {
                TargetControl.Controls.Remove(childToRemove.TargetControl);
            }
            Children.RemoveAt(siblingIndex);
        }

        private void ApplySetAttribute(ref RenderTreeFrame attributeFrame)
        {
            var mapper = GetControlPropertyMapper(TargetControl);
            mapper.SetControlProperty(attributeFrame.AttributeEventHandlerId, attributeFrame.AttributeName, attributeFrame.AttributeValue, attributeFrame.AttributeEventUpdatesAttributeName);
        }

        private int ApplyPrependFrame(RenderBatch batch, int componentId, int siblingIndex, RenderTreeFrame[] frames, int frameIndex)
        {
            ref var frame = ref frames[frameIndex];
            switch (frame.FrameType)
            {
                case RenderTreeFrameType.Element:
                    {
                        InsertElement(siblingIndex, frames, frameIndex, componentId, batch);
                        return 1;
                    }
                case RenderTreeFrameType.Component:
                    {
                        // Components are represented by BlontrolAdapters
                        var childAdapter = Renderer.CreateAdapterForChildComponent(frame.ComponentId);
                        childAdapter.Name = $"For: '{frame.Component.GetType().FullName}'";
                        AddChildAdapter(siblingIndex, childAdapter);
                        return 1;
                    }
                case RenderTreeFrameType.Region:
                    {
                        return InsertFrameRange(batch, componentId, siblingIndex, frames, frameIndex + 1, frameIndex + frame.RegionSubtreeLength);
                    }
                case RenderTreeFrameType.Markup:
                    {
                        if (!string.IsNullOrWhiteSpace(frame.MarkupContent))
                        {
                            throw new NotImplementedException("Nonempty markup: " + frame.MarkupContent);
                        }
                        Children.Add(new BlontrolAdapter(Renderer) { Name = $"Dummy markup, sib#={siblingIndex}" });
                        return 1;
                    }
                case RenderTreeFrameType.Text:
                    {
                        // TODO: Maybe support this for Labels for Text property, etc. ("DefaultProperty"?)
                        if (!string.IsNullOrWhiteSpace(frame.TextContent))
                        {
                            throw new NotImplementedException("Nonempty text: " + frame.TextContent);
                        }
                        Children.Add(new BlontrolAdapter(Renderer) { Name = $"Dummy text, sib#={siblingIndex}" });
                        return 1;
                    }
                default:
                    throw new NotImplementedException($"Not supported frame type: {frame.FrameType}");
            }
        }

        private void InsertElement(int siblingIndex, RenderTreeFrame[] frames, int frameIndex, int componentId, RenderBatch batch)
        {
            // Elements represent Winforms native controls
            ref var frame = ref frames[frameIndex];
            var elementName = frame.ElementName;
            var nativeControl = KnownElements[elementName](Renderer);

            TargetControl = nativeControl;

            // Add the new native control to the parent's child controls (the parent adapter is our
            // container, so the parent adapter's control is our control's container.
            AddChildControl(siblingIndex, TargetControl);


            var endIndexExcl = frameIndex + frames[frameIndex].ElementSubtreeLength;
            for (var descendantIndex = frameIndex + 1; descendantIndex < endIndexExcl; descendantIndex++)
            {
                var candidateFrame = frames[descendantIndex];
                if (candidateFrame.FrameType == RenderTreeFrameType.Attribute)
                {
                    // TODO: Do smarter property setting...? Not calling <NativeControl>.ApplyAttribute(...) right now. Should it?
                    ApplySetAttribute(ref candidateFrame);
                }
                else
                {
                    // As soon as we see a non-attribute child, all the subsequent child frames are
                    // not attributes, so bail out and insert the remnants recursively
                    InsertFrameRange(batch, componentId, childIndex: 0, frames, descendantIndex, endIndexExcl);
                    break;
                }
            }


            //// Ignoring non-controls, such as Timer Component

            //if (element is Control elementControl)
            //{
            //    AddChildControl(siblingIndex, elementControl);
            //}
            //else
            //{
            //    Debug.WriteLine("Ignoring non-control child: " + element.GetType().FullName);
            //}
        }

        private int InsertFrameRange(RenderBatch batch, int componentId, int childIndex, RenderTreeFrame[] frames, int startIndex, int endIndexExcl)
        {
            var origChildIndex = childIndex;
            for (var index = startIndex; index < endIndexExcl; index++)
            {
                ref var frame = ref batch.ReferenceFrames.Array[index];
                var numChildrenInserted = ApplyPrependFrame(batch, componentId, childIndex, frames, index);
                childIndex += numChildrenInserted;

                // Skip over any descendants, since they are already dealt with recursively
                index += CountDescendantFrames(frame);
            }

            return (childIndex - origChildIndex); // Total number of children inserted     
        }

        private int CountDescendantFrames(RenderTreeFrame frame)
        {
            return frame.FrameType switch
            {
                // The following frame types have a subtree length. Other frames may use that memory slot
                // to mean something else, so we must not read it. We should consider having nominal subtypes
                // of RenderTreeFramePointer that prevent access to non-applicable fields.
                RenderTreeFrameType.Component => frame.ComponentSubtreeLength - 1,
                RenderTreeFrameType.Element => frame.ElementSubtreeLength - 1,
                RenderTreeFrameType.Region => frame.RegionSubtreeLength - 1,
                _ => 0,
            };
        }

        private void AddChildAdapter(int siblingIndex, BlontrolAdapter childAdapter)
        {
            childAdapter.Parent = this;

            if (siblingIndex < Children.Count)
            {
                Children.Insert(siblingIndex, childAdapter);
            }
            else
            {
                Children.Add(childAdapter);
            }
        }

        private void AddChildControl(int siblingIndex, Control childControl)
        {
            if (siblingIndex < Parent.TargetControl.Controls.Count)
            {
                // WinForms ControlCollection doesn't support Insert(), so add the new child at the end,
                // and then re-order the collection to move the control to the correct index.
                Parent.TargetControl.Controls.Add(childControl);
                Parent.TargetControl.Controls.SetChildIndex(childControl, siblingIndex);
            }
            else
            {
                Parent.TargetControl.Controls.Add(childControl);
            }
        }

        private static IControlPropertyMapper GetControlPropertyMapper(Control control)
        {
            // TODO: Have control-specific ones, but also need a general one for custom controls? Or maybe not needed?
            if (control is IBlazorNativeControl nativeControl)
            {
                return new NativeControlPropertyMapper(nativeControl);
            }
            else
            {
                return new ReflectionControlPropertyMapper(control);
            }
        }
    }
}
