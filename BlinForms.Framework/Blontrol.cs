using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BlinForms.Framework.Controls;
using BlinForms.Framework.Elements;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework
{
    public class Blontrol : Control
    {
        private readonly BlinFormsRenderer _renderer;
        private readonly List<Control> _childControls = new List<Control>();

        internal static Dictionary<string, Func<BlinFormsRenderer, IBlazorNativeControl>> KnownElements { get; }
            = new Dictionary<string, Func<BlinFormsRenderer, IBlazorNativeControl>>();

        public Blontrol(BlinFormsRenderer renderer)
        {
            _renderer = renderer;
            Width = 500;
            Height = 500;
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
                    default:
                        throw new NotImplementedException($"Not supported edit type: {edit.Type}");
                }
            }
        }

        private void ApplySetAttribute(int siblingIndex, ref RenderTreeFrame attributeFrame)
        {
            var target = (IBlazorNativeControl)Controls[siblingIndex];
            target.ApplyAttribute(ref attributeFrame);
        }

        private void ApplyPrependFrame(int siblingIndex, RenderTreeFrame[] frames, int frameIndex)
        {
            ref var frame = ref frames[frameIndex];
            switch (frame.FrameType)
            {
                case RenderTreeFrameType.Element:
                    var elementControl = (Control)CreateNativeControlForElement(frames, frameIndex);
                    AddChildControl(siblingIndex, elementControl);
                    break;
                case RenderTreeFrameType.Component:
                    {
                        var childControl = _renderer.CreateControlForChildComponent(frame.ComponentId);
                        AddChildControl(siblingIndex, childControl);
                        break;
                    }
                case RenderTreeFrameType.Markup:
                    if (!string.IsNullOrWhiteSpace(frame.MarkupContent))
                    {
                        throw new NotImplementedException("Nonempty markup: " + frame.MarkupContent);
                    }
                    break;
                default:
                    throw new NotImplementedException($"Not supported frame type: {frame.FrameType}");
            }
        }

        private void AddChildControl(int siblingIndex, Control childControl)
        {
            Controls.Add(childControl);
            if (siblingIndex < _childControls.Count)
            {
                _childControls.Insert(siblingIndex, childControl);
            }
            else
            {
                _childControls.Add(childControl);
            }
        }

        private IBlazorNativeControl CreateNativeControlForElement(RenderTreeFrame[] frames, int frameIndex)
        {
            ref var frame = ref frames[frameIndex];
            var elementName = frame.ElementName;
            var nativeControl = KnownElements[elementName](_renderer);

            foreach (var attribute in AttributeUtil.ElementAttributeFrames(frames, frameIndex))
            {
                var attributeCopy = attribute;
                nativeControl.ApplyAttribute(ref attributeCopy);
            }

            return nativeControl;
        }
    }
}
