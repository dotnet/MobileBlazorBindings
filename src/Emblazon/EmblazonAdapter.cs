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
    internal sealed class EmblazonAdapter<TElementHandler> : IDisposable where TElementHandler : class, IElementHandler
    {
        private static volatile int DebugInstanceCounter;

        public EmblazonAdapter(EmblazonRenderer<TElementHandler> renderer, TElementHandler closestPhysicalParent, TElementHandler knownTargetControl = null)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _closestPhysicalParent = closestPhysicalParent;
            _possibleTargetControl = knownTargetControl;

            // Assign unique counter value. This *should* all be done on one thread, but just in case, make it thread-safe.
            _debugInstanceCounterValue = Interlocked.Increment(ref DebugInstanceCounter);
        }

        private readonly int _debugInstanceCounterValue;

        private string DebugName => $"[#{_debugInstanceCounterValue}] {Name}";

        public EmblazonAdapter<TElementHandler> Parent { get; private set; }
        public List<EmblazonAdapter<TElementHandler>> Children { get; } = new List<EmblazonAdapter<TElementHandler>>();

        private readonly TElementHandler _closestPhysicalParent;
        private TElementHandler _possibleTargetControl;

        public EmblazonRenderer<TElementHandler> Renderer { get; }

        /// <summary>
        /// Used for debugging purposes.
        /// </summary>
        public string Name { get; internal set; }

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
                        // TODO: See whether siblingIndex is needed here
                        ApplyRemoveAttribute(edit.RemovedAttributeName);
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
                Renderer.NativeControlManager.RemoveElement(_possibleTargetControl);
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

            _possibleTargetControl.ApplyAttribute(
                attributeFrame.AttributeEventHandlerId,
                attributeFrame.AttributeName,
                attributeFrame.AttributeValue,
                attributeFrame.AttributeEventUpdatesAttributeName);
        }

        private void ApplyRemoveAttribute(string removedAttributeName)
        {
            if (_possibleTargetControl == null)
            {
                throw new InvalidOperationException($"Trying to remove attribute {removedAttributeName} to an adapter that isn't for an element");
            }

            _possibleTargetControl.ApplyAttribute(
                attributeEventHandlerId: 0,
                attributeName: removedAttributeName,
                attributeValue: null,
                attributeEventUpdatesAttributeName: null);
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
#pragma warning disable CA2000 // Dispose objects before losing scope; adapters are disposed when they are removed from the adapter tree
                        var childAdapter = CreateAdapter(_possibleTargetControl ?? _closestPhysicalParent);
#pragma warning restore CA2000 // Dispose objects before losing scope
                        childAdapter.Name = $"Markup, sib#={siblingIndex}";
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
#pragma warning disable CA2000 // Dispose objects before losing scope; adapters are disposed when they are removed from the adapter tree
                        var childAdapter = CreateAdapter(_possibleTargetControl ?? _closestPhysicalParent);
#pragma warning restore CA2000 // Dispose objects before losing scope
                        childAdapter.Name = $"Text, sib#={siblingIndex}";
                        AddChildAdapter(siblingIndex, childAdapter);
                        return 1;
                    }
                default:
                    throw new NotImplementedException($"Not supported frame type: {frame.FrameType}");
            }
        }

        private EmblazonAdapter<TElementHandler> CreateAdapter(TElementHandler physicalParent)
        {
            return new EmblazonAdapter<TElementHandler>(Renderer, physicalParent);
        }

        private void InsertElement(int siblingIndex, RenderTreeFrame[] frames, int frameIndex, int componentId, RenderBatch batch)
        {
            // Elements represent native controls
            ref var frame = ref frames[frameIndex];
            var elementName = frame.ElementName;
            var controlFactory = ElementHandlerRegistry<TElementHandler>.ElementHandlers[elementName];
            var nativeControlHandler = controlFactory.CreateControl(new ElementHandlerFactoryContext<TElementHandler>(Renderer, _closestPhysicalParent));

            if (siblingIndex != 0)
            {
                // With the current design, we should be able to ignore sibling indices for elements,
                // so bail out if that's not the case
                throw new NotSupportedException($"Currently we assume all adapter controls render exactly zero or one elements. Found an element with sibling index {siblingIndex}");
            }

            // For the location in the physical control tree, find the last preceding-sibling adapter that has
            // a physical descendant (if any). If there is one, we physically insert after that one. If not,
            // we'll insert as the first child of the closest physical parent.
            if (!Renderer.NativeControlManager.IsParented(nativeControlHandler))
            {
                var elementIndex = GetIndexForElement();
                Renderer.NativeControlManager.AddChildElement(_closestPhysicalParent, nativeControlHandler, elementIndex);
            }
            _possibleTargetControl = nativeControlHandler;

            var endIndexExcl = frameIndex + frames[frameIndex].ElementSubtreeLength;
            for (var descendantIndex = frameIndex + 1; descendantIndex < endIndexExcl; descendantIndex++)
            {
                var candidateFrame = frames[descendantIndex];
                if (candidateFrame.FrameType == RenderTreeFrameType.Attribute)
                {
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

        /// <summary>
        /// Finds the sibling index to insert this adapter's element into. It walks up Parent adapters to find 
        /// an earlier sibling that has a native control, and uses that native control's physical index to determine
        /// the location of the new element.
        /// <code>
        /// * Adapter0
        /// * Adapter1
        /// * Adapter2
        /// * Adapter3 (native)
        ///     * Adapter3.0 (searchOrder=2)
        ///         * Adapter3.0.0 (searchOrder=3)
        ///         * Adapter3.0.1 (native)  (searchOrder=4) &lt;-- This is the nearest earlier sibling that has a physical control)
        ///         * Adapter3.0.2
        ///     * Adapter3.1 (searchOrder=1)
        ///         * Adapter3.1.0 (searchOrder=0)
        ///         * Adapter3.1.1 (native) &lt;-- Current adapter
        ///         * Adapter3.1.2
        ///     * Adapter3.2
        /// * Adapter4
        /// </code>
        /// </summary>
        /// <returns>The index at which the native control (element) should be inserted into within the parent. It returns -1 as a failure mode.</returns>
        private int GetIndexForElement()
        {
            var childAdapter = this;
            var parentAdapter = Parent;
            while (parentAdapter != null)
            {
                // Walk previous siblings of this level and deep-scan them for native controls
                var matchedEarlierSibling = GetEarlierSiblingMatch(parentAdapter, childAdapter);
                if (matchedEarlierSibling != null)
                {
                    if (!Renderer.NativeControlManager.IsParentOfChild(_closestPhysicalParent, matchedEarlierSibling._possibleTargetControl))
                    {
                        Debug.Fail($"Expected that the item found ({matchedEarlierSibling.DebugName}) with target control ({matchedEarlierSibling._possibleTargetControl.GetType().FullName}) should necessarily be an immediate child of the closest native parent ({_closestPhysicalParent.GetType().FullName}), but it wasn't...");
                    }

                    // If a native control was found somewhere within this sibling, the index for the new element
                    // will be 1 greater than its native index.
                    return Renderer.NativeControlManager.GetPhysicalSiblingIndex(matchedEarlierSibling._possibleTargetControl) + 1;
                }

                // If this level has a native control and all its relevant children have been scanned, then there's
                // no previous sibling, so the new element to be added will be its earliest child (index=0). (There
                // might be *later* siblings, but they are not relevant to this search.)
                if (parentAdapter._possibleTargetControl != null)
                {
                    Debug.Assert(parentAdapter._possibleTargetControl == _closestPhysicalParent, $"Expected that nearest parent ({parentAdapter.DebugName}) with native control ({parentAdapter._possibleTargetControl.GetType().FullName}) would have the closest physical parent ({_closestPhysicalParent.GetType().FullName}).");
                    return 0;
                }

                // If we haven't found a previous sibling with a native control or reached a native container, keep
                // walking up the parent tree...
                childAdapter = parentAdapter;
                parentAdapter = parentAdapter.Parent;
            }
            Debug.Fail($"Expected to find a parent with a native control but found none.");
            return -1;
        }

        private static EmblazonAdapter<TElementHandler> GetEarlierSiblingMatch(EmblazonAdapter<TElementHandler> parentAdapter, EmblazonAdapter<TElementHandler> childAdapter)
        {
            var indexOfParentsChildAdapter = parentAdapter.Children.IndexOf(childAdapter);

            for (var i = indexOfParentsChildAdapter - 1; i >= 0; i--)
            {
                var sibling = parentAdapter.Children[i];

                // Deep scan this sibling adapter to find its latest and highest native control
                var siblingWithNativeControl = sibling.GetLastDescendantWithPhysicalControl();
                if (siblingWithNativeControl != null)
                {
                    return siblingWithNativeControl;
                }
            }

            // No preceding sibling has any native controls
            return null;
        }

        private EmblazonAdapter<TElementHandler> GetLastDescendantWithPhysicalControl()
        {
            if (_possibleTargetControl != null)
            {
                // If this adapter has a target control, then this is the droid we're looking for. It can't be
                // any children of this target control because they can't be children of this control's parent.
                return this;
            }

            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                var physicalDescendant = child.GetLastDescendantWithPhysicalControl();
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
            ;
        }

        private void AddChildAdapter(int siblingIndex, EmblazonAdapter<TElementHandler> childAdapter)
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

        public void Dispose()
        {
            if (_possibleTargetControl is IDisposable disposableTargetControl)
            {
                disposableTargetControl.Dispose();
            }
        }
    }
}
