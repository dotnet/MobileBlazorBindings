using BlinForms.Framework.Elements;
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

        public BlinFormsRenderer Renderer { get; private set; }

        /// <summary>
        /// Used for debugging purposes.
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Blontrol: Name={Name ?? "<?>"}, Target={TargetControl?.GetType().Name ?? "<?>"}, #Children={Children.Count}";
        }

        internal void ApplyEdits(ArrayBuilderSegment<RenderTreeEdit> edits, ArrayRange<RenderTreeFrame> referenceFrames)
        {
            foreach (var edit in edits)
            {
                switch (edit.Type)
                {
                    case RenderTreeEditType.PrependFrame:
                        ApplyPrependFrame(edit.SiblingIndex, referenceFrames.Array, edit.ReferenceFrameIndex);
                        break;
                    case RenderTreeEditType.SetAttribute:
                        ApplySetAttribute(edit.SiblingIndex, ref referenceFrames.Array[edit.ReferenceFrameIndex]);
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

        private void ApplySetAttribute(int siblingIndex, ref RenderTreeFrame attributeFrame)
        {
            // TODO: What to do with siblingIndex here? So far always seems to be 0
            var mapper = GetControlPropertyMapper(TargetControl);
            mapper.SetControlProperty(attributeFrame.AttributeEventHandlerId, attributeFrame.AttributeName, attributeFrame.AttributeValue, attributeFrame.AttributeEventUpdatesAttributeName);
        }

        private void ApplyPrependFrame(int siblingIndex, RenderTreeFrame[] frames, int frameIndex)
        {
            ref var frame = ref frames[frameIndex];
            switch (frame.FrameType)
            {
                case RenderTreeFrameType.Element:
                    {
                        // Elements represent Winforms native controls
                        var element = CreateNativeControlForElement(frames, frameIndex);
                        TargetControl = element;

                        // Add the new nataive control to the parent's child controls (the parent adapter is our
                        // container, so the parent adapter's control is our control's container.
                        // TODO: What about siblingIndex?
                        Parent.TargetControl.Controls.Add(TargetControl);

                        //// Ignoring non-controls, such as Timer Component

                        //if (element is Control elementControl)
                        //{
                        //    AddChildControl(siblingIndex, elementControl);
                        //}
                        //else
                        //{
                        //    Debug.WriteLine("Ignoring non-control child: " + element.GetType().FullName);
                        //}
                        break;
                    }
                case RenderTreeFrameType.Component:
                    {
                        // Components are represented by BlontrolAdapters
                        var childAdapter = Renderer.CreateAdapterForChildComponent(frame.ComponentId);
                        childAdapter.Name = $"For: '{frame.Component.GetType().FullName}'";
                        AddChildAdapter(siblingIndex, childAdapter);
                        break;
                    }
                case RenderTreeFrameType.Markup:
                    {
                        if (!string.IsNullOrWhiteSpace(frame.MarkupContent))
                        {
                            throw new NotImplementedException("Nonempty markup: " + frame.MarkupContent);
                        }
                        Children.Add(new BlontrolAdapter(Renderer) { Name = $"Dummy markup, sib#={siblingIndex}" });
                        break;
                    }
                case RenderTreeFrameType.Text:
                    {
                        // TODO: Maybe support this for Labels for Text property, etc. ("DefaultProperty"?)
                        if (!string.IsNullOrWhiteSpace(frame.TextContent))
                        {
                            throw new NotImplementedException("Nonempty text: " + frame.TextContent);
                        }
                        Children.Add(new BlontrolAdapter(Renderer) { Name = $"Dummy text, sib#={siblingIndex}" });
                        break;
                    }
                default:
                    throw new NotImplementedException($"Not supported frame type: {frame.FrameType}");
            }
        }

        private Control CreateNativeControlForElement(RenderTreeFrame[] frames, int frameIndex)
        {
            ref var frame = ref frames[frameIndex];
            var elementName = frame.ElementName;
            var nativeControl = KnownElements[elementName](Renderer);

            foreach (var attribute in AttributeUtil.ElementAttributeFrames(frames, frameIndex))
            {
                // TODO: Do smarter property setting...? Not calling <NativeControl>.ApplyAttribute(...) right now. Should it?
                var mapper = GetControlPropertyMapper(nativeControl);
                mapper.SetControlProperty(attribute.AttributeEventHandlerId, attribute.AttributeName, attribute.AttributeValue, attribute.AttributeEventUpdatesAttributeName);
            }

            return nativeControl;
        }

        private void AddChildAdapter(int siblingIndex, BlontrolAdapter childAdapter)
        {
            childAdapter.Parent = this;

            // TODO: Maybe tag each adapter with the "expected" siblingIndex so that later we can use that to
            // map which sibling is where during inserts, etc. Right now this is handled by having dummy adapters
            // in the tree so that the index locations match between Blazor and the adapter tree.

            if (siblingIndex < Children.Count)
            {
                Children.Insert(siblingIndex, childAdapter);
            }
            else
            {
                Children.Add(childAdapter);
            }
        }

        private static IControlPropertyMapper GetControlPropertyMapper(Control control)
        {
            // TODO: Have control-specific ones, but also need a general one for custom controls? Or maybe not needed?
            return new ReflectionControlPropertyMapper(control);
        }
    }
}
