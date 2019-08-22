using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Emblazon
{
    /// <summary>
    /// Represents a "shadow" item that Blazor uses to map changes into the live WinForms control tree.
    /// </summary>
    public abstract class EmblazonAdapter<TNativeComponent> where TNativeComponent : class
    {
        private static Dictionary<string, ComponentControlFactory<TNativeComponent>> KnownElements { get; }
            = new Dictionary<string, ComponentControlFactory<TNativeComponent>>();

        public static void RegisterNativeControlComponent<TComponent>(Func<EmblazonRenderer<TNativeComponent>, TNativeComponent, TNativeComponent> factory) where TComponent: NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentControlFactory<TNativeComponent>(factory));
        }

        public static void RegisterNativeControlComponent<TComponent>(Func<EmblazonRenderer<TNativeComponent>, TNativeComponent> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentControlFactory<TNativeComponent>((renderer, _) => factory(renderer)));
        }

        public static void RegisterNativeControlComponent<TComponent, TControl>() where TComponent : NativeControlComponentBase where TControl : TNativeComponent, new()
        {
            RegisterNativeControlComponent<TComponent>((_, __) => new TControl());
        }

        public EmblazonAdapter()
        {
        }

        public EmblazonAdapter<TNativeComponent> Parent { get; set; }
        public List<EmblazonAdapter<TNativeComponent>> Children { get; } = new List<EmblazonAdapter<TNativeComponent>>();

        // TODO: Is this the right concept? Can a component have multiple WinForms controls created?
        public TNativeComponent TargetControl { get; set; }

        public EmblazonRenderer<TNativeComponent> Renderer { get; private set; }

        internal void SetRenderer(EmblazonRenderer<TNativeComponent> renderer)
        { 
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        /// <summary>
        /// Used for debugging purposes.
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"EmblazonAdapter: Name={Name ?? "<?>"}, Target={TargetControl?.GetType().Name ?? "<?>"}, #Children={Children.Count}";
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
                RemoveChildControl(childToRemove);
            }
            Children.RemoveAt(siblingIndex);
        }

        protected abstract void RemoveChildControl(EmblazonAdapter<TNativeComponent> child);

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
                        var childAdapter = CreateAdapter();
                        childAdapter.Name = $"Dummy markup, sib#={siblingIndex}";
                        childAdapter.SetRenderer(Renderer);
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
                        var childAdapter = CreateAdapter();
                        childAdapter.Name = $"Dummy text, sib#={siblingIndex}";
                        childAdapter.SetRenderer(Renderer);
                        AddChildAdapter(siblingIndex, childAdapter);
                        return 1;
                    }
                default:
                    throw new NotImplementedException($"Not supported frame type: {frame.FrameType}");
            }
        }

        protected abstract EmblazonAdapter<TNativeComponent> CreateAdapter();

        protected abstract bool IsChildControlParented(TNativeComponent nativeChild);

        private void InsertElement(int siblingIndex, RenderTreeFrame[] frames, int frameIndex, int componentId, RenderBatch batch)
        {
            // Elements represent Winforms native controls
            ref var frame = ref frames[frameIndex];
            var elementName = frame.ElementName;
            var controlFactory = KnownElements[elementName];
            var nativeControl = controlFactory.CreateControl(new ComponentControlFactoryContext<TNativeComponent>(Renderer, Parent?.TargetControl));

            TargetControl = nativeControl;

            // TODO: Need a more reliable way to know whether the target control is already created, e.g. a return value
            // from ControlFactory.CreateControl(). Right now the check assumes that if the target control is already parented,
            // there is no need to parent it. Not an awful assumption, but looks odd.
            if (!IsChildControlParented(TargetControl))
            {
                // Add the new native control to the parent's child controls (the parent adapter is our
                // container, so the parent adapter's control is our control's container.
                AddChildControl(Parent.TargetControl, siblingIndex, TargetControl);
            }

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

        protected abstract void AddChildControl(TNativeComponent parentControl, int siblingIndex, TNativeComponent childControl);

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
    }
}
