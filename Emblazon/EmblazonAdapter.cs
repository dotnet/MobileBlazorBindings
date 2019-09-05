using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Emblazon
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live native control tree.
    /// </summary>
    [DebuggerDisplay("{DebugName}")]
    internal sealed class EmblazonAdapter<TNativeComponent> : IDisposable where TNativeComponent : class
    {
        private static volatile int DebugInstanceCounter;

        public EmblazonAdapter(EmblazonRenderer<TNativeComponent> renderer, TNativeComponent closestPhysicalParent, TNativeComponent knownTargetControl = null)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _closestPhysicalParent = closestPhysicalParent;
            _possibleTargetControl = knownTargetControl;

            // Assign unique counter value. This *should* all be done on one thread, but just in case, make it thread-safe.
            _debugInstanceCounterValue = Interlocked.Increment(ref DebugInstanceCounter);
        }

        private readonly int _debugInstanceCounterValue;

        public string DebugName
        {
            get
            {
                return $"[#{_debugInstanceCounterValue}] {Name}";
            }
        }

        public EmblazonAdapter<TNativeComponent> Parent { get; set; }
        public List<EmblazonAdapter<TNativeComponent>> Children { get; } = new List<EmblazonAdapter<TNativeComponent>>();

        private readonly TNativeComponent _closestPhysicalParent;
        private TNativeComponent _possibleTargetControl;

        public EmblazonRenderer<TNativeComponent> Renderer { get; private set; }

        /// <summary>
        /// Used for debugging purposes.
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"EmblazonAdapter: Name={Name ?? "<?>"}, Target={_possibleTargetControl?.GetType().Name ?? "<None>"}, #Children={Children.Count}";
        }

        internal void ApplyEdits(int componentId, ArrayBuilderSegment<RenderTreeEdit> edits, ArrayRange<RenderTreeFrame> referenceFrames, RenderBatch batch)
        {
            if (edits.Count == 0)
            {
                // TODO: Without this check there's a NullRef in ArrayBuilderSegment? Possibly a Blazor bug?
                return;
            }

            foreach (var edit in edits)
            {
                switch (edit.Type)
                {
                    case RenderTreeEditType.PrependFrame:
                        ApplyPrependFrame(batch, componentId, edit.SiblingIndex, referenceFrames.Array, edit.ReferenceFrameIndex);
                        break;
                    case RenderTreeEditType.RemoveFrame:
                        ApplyRemoveFrame(edit.SiblingIndex);
                        break;
                    case RenderTreeEditType.SetAttribute:
                        ApplySetAttribute(ref referenceFrames.Array[edit.ReferenceFrameIndex]);
                        break;
                    case RenderTreeEditType.RemoveAttribute:
                        ApplyRemoveAttribute(edit.SiblingIndex, edit.RemovedAttributeName);
                        break;
                    case RenderTreeEditType.UpdateText:
                        throw new NotImplementedException($"Not supported edit type: {edit.Type}");
                    case RenderTreeEditType.StepIn:
                        {
                            // TODO: Need to implement this. For now it seems safe to ignore.
                            break;
                        }
                    case RenderTreeEditType.StepOut:
                        {
                            // TODO: Need to implement this. For now it seems safe to ignore.
                            break;
                        }
                    case RenderTreeEditType.UpdateMarkup:
                        throw new NotImplementedException($"Not supported edit type: {edit.Type}");
                    case RenderTreeEditType.PermutationListEntry:
                        throw new NotImplementedException($"Not supported edit type: {edit.Type}");
                    case RenderTreeEditType.PermutationListEnd:
                        throw new NotImplementedException($"Not supported edit type: {edit.Type}");
                    default:
                        throw new NotImplementedException($"Invalid edit type: {edit.Type}");
                }
            }
        }

        private void ApplyRemoveFrame(int siblingIndex)
        {
            var childToRemove = Children[siblingIndex];
            Children.RemoveAt(siblingIndex);
            childToRemove.RemoveSelfAndDescendants();
        }

        private void RemoveSelfAndDescendants()
        {
            if (_possibleTargetControl != null)
            {
                // This adapter represents a physical control, so by removing it, we implicitly
                // remove all descendants.
                Renderer.NativeControlManager.RemovePhysicalControl(_possibleTargetControl);
            }
            else
            {
                // This adapter is just a container for other adapters
                foreach (var child in Children)
                {
                    child.RemoveSelfAndDescendants();
                }
            }
        }

        private void ApplySetAttribute(ref RenderTreeFrame attributeFrame)
        {
            if (_possibleTargetControl == null)
            {
                throw new InvalidOperationException($"Trying to apply attribute {attributeFrame.AttributeName} to an adapter that isn't for an element");
            }

            var mapper = GetControlPropertyMapper(_possibleTargetControl);
            mapper.SetControlProperty(attributeFrame.AttributeEventHandlerId, attributeFrame.AttributeName, attributeFrame.AttributeValue, attributeFrame.AttributeEventUpdatesAttributeName);
        }

        private void ApplyRemoveAttribute(int siblingIndex, string removedAttributeName)// ref RenderTreeFrame attributeFrame)
        {
            if (_possibleTargetControl == null)
            {
                throw new InvalidOperationException($"Trying to remove attribute {removedAttributeName} to an adapter that isn't for an element");
            }

            var mapper = GetControlPropertyMapper(_possibleTargetControl);
            mapper.SetControlProperty(0, removedAttributeName, null, null);
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
                        var childAdapter = Renderer.CreateAdapterForChildComponent(_possibleTargetControl ?? _closestPhysicalParent, frame.ComponentId);
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
                        var childAdapter = CreateAdapter(_possibleTargetControl ?? _closestPhysicalParent);
                        childAdapter.Name = $"Dummy markup, sib#={siblingIndex}";
                        AddChildAdapter(siblingIndex, childAdapter);
                        return 1;
                    }
                case RenderTreeFrameType.Text:
                    {
                        // TODO: Maybe support this for Labels for Text property, etc. ("DefaultProperty"?)
                        if (!string.IsNullOrWhiteSpace(frame.TextContent))
                        {
                            throw new NotImplementedException("Nonempty text: " + frame.TextContent);
                        }
                        var childAdapter = CreateAdapter(_possibleTargetControl ?? _closestPhysicalParent);
                        childAdapter.Name = $"Dummy text, sib#={siblingIndex}";
                        AddChildAdapter(siblingIndex, childAdapter);
                        return 1;
                    }
                default:
                    throw new NotImplementedException($"Not supported frame type: {frame.FrameType}");
            }
        }

        private EmblazonAdapter<TNativeComponent> CreateAdapter(TNativeComponent physicalParent)
        {
            return new EmblazonAdapter<TNativeComponent>(Renderer, physicalParent);
        }

        private void InsertElement(int siblingIndex, RenderTreeFrame[] frames, int frameIndex, int componentId, RenderBatch batch)
        {
            // Elements represent native controls
            ref var frame = ref frames[frameIndex];
            var elementName = frame.ElementName;
            var controlFactory = NativeControlRegistry<TNativeComponent>.KnownElements[elementName];
            var nativeControl = controlFactory.CreateControl(new ComponentControlFactoryContext<TNativeComponent>(Renderer, _closestPhysicalParent));

            if (siblingIndex != 0)
            {
                // With the current design, we should be able to ignore sibling indices for elements,
                // so bail out if that's not the case
                throw new NotSupportedException($"Currently we assume all adapter controls render exactly zero or one elements. Found an element with sibling index {siblingIndex}");
            }

            // For the location in the physical control tree, find the last preceding-sibling adapter that has
            // a physical descendant (if any). If there is one, we physically insert after that one. If not,
            // we'll insert as the first child of the closest physical parent.
            if (!Renderer.NativeControlManager.IsParented(nativeControl))
            {
                if (Parent.TryFindPhysicalChildIndexBefore(_closestPhysicalParent, this, out var precedingSiblingPhysicalIndex))
                {
                    Renderer.NativeControlManager.AddPhysicalControl(_closestPhysicalParent, nativeControl, precedingSiblingPhysicalIndex + 1);
                }
                else
                {
                    Renderer.NativeControlManager.AddPhysicalControl(_closestPhysicalParent, nativeControl, 0);
                }
            }
            _possibleTargetControl = nativeControl;

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

        private bool TryFindPhysicalChildIndexBefore(TNativeComponent nativeParentOfInterest, EmblazonAdapter<TNativeComponent> child, out int resultIndex)
        {
            if (!TryGetPhysicalIndexOfLastDescendant(nativeParentOfInterest, out _))
            {
                if (Parent == null)
                {
                    // We're at the root
                    resultIndex = 0;
                    return false;
                }
                else
                {
                    // No physical controls exist in this subtree, so step upwards
                    return Parent.TryFindPhysicalChildIndexBefore(nativeParentOfInterest, this, out resultIndex);
                }
            }

            var suppliedChildIndex = Children.IndexOf(child);
            if (suppliedChildIndex < 0)
            {
                throw new ArgumentException("She says I am the one, but the kid is not my son");
            }

            for (var candidateAdapterIndex = suppliedChildIndex - 1; candidateAdapterIndex >= 0; candidateAdapterIndex--)
            {
                var candidateAdapter = Children[candidateAdapterIndex];
                if (candidateAdapter.TryGetPhysicalIndexOfLastDescendant(nativeParentOfInterest, out resultIndex))
                {
                    return true;
                }
            }

            resultIndex = 0;
            return false;
        }

        private bool TryGetPhysicalIndexOfLastDescendant(TNativeComponent nativeParentOfInterest, out int resultIndex)
        {
            var lastPhysicalDescendant = GetLastPhysicalDescendantWithParentOfInterest(nativeParentOfInterest);
            if (lastPhysicalDescendant == null)
            {
                resultIndex = 0;
                return false;
            }
            else
            {
                resultIndex = Renderer.NativeControlManager.GetPhysicalSiblingIndex(lastPhysicalDescendant);
                return true;
            }
        }

        private TNativeComponent GetLastPhysicalDescendantWithParentOfInterest(TNativeComponent nativeParentOfInterest)
        {
            if (_possibleTargetControl != null)
            {
                // TODO: Is it true that if the first condition above is true, that the second condition below must always be true, and so it is not necessary? (Probably? Let's see what the Debug.Fail() statement says.)
                if (Renderer.NativeControlManager.IsParentOfChild(nativeParentOfInterest, _possibleTargetControl))
                {
                    // If this adapter has a target control, then this is the droid we're looking for. It can't be
                    // any children of this target control because they can't be children of this control's parent.
                    return _possibleTargetControl;
                }
                else
                {
                    Debug.Fail($"Expected that the first item found ({DebugName}) with a target control ({_possibleTargetControl.GetType().FullName}) should necessarily be an immediate child of the native parent of interest ({nativeParentOfInterest.GetType().FullName}), but it wasn't, so the search continues...");
                }
            }

            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                var physicalDescendant = child.GetLastPhysicalDescendantWithParentOfInterest(nativeParentOfInterest);
                if (physicalDescendant != null)
                {
                    return physicalDescendant;
                }
            }

            return null;
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

        private static int CountDescendantFrames(RenderTreeFrame frame)
        {
            switch (frame.FrameType)
            {
                // The following frame types have a subtree length. Other frames may use that memory slot
                // to mean something else, so we must not read it. We should consider having nominal subtypes
                // of RenderTreeFramePointer that prevent access to non-applicable fields.
                case RenderTreeFrameType.Component:
                    return frame.ComponentSubtreeLength - 1;
                case RenderTreeFrameType.Element: return frame.ElementSubtreeLength - 1;
                case RenderTreeFrameType.Region: return frame.RegionSubtreeLength - 1;
                default:
                    return 0;
            };
        }

        private void AddChildAdapter(int siblingIndex, EmblazonAdapter<TNativeComponent> childAdapter)
        {
            childAdapter.Parent = this;

            if (siblingIndex <= Children.Count)
            {
                Children.Insert(siblingIndex, childAdapter);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChildAdapter)} called with {nameof(siblingIndex)}={siblingIndex}, but Children.Count={Children.Count}");
                Children.Add(childAdapter);
            }
        }

        private static IControlPropertyMapper GetControlPropertyMapper(TNativeComponent control)
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

        public void Dispose()
        {
            if (_possibleTargetControl is IDisposable disposableTargetControl)
            {
                disposableTargetControl.Dispose();
            }
        }
    }
}
