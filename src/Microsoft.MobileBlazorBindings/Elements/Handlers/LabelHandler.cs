// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsSpan = child as XF.Span;

            var formattedString = GetFormattedString();
            if (physicalSiblingIndex <= formattedString.Spans.Count)
            {
                formattedString.Spans.Insert(physicalSiblingIndex, childAsSpan);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but Label.FormattedText.Spans.Count={LabelControl.FormattedText.Spans.Count}");
                formattedString.Spans.Add(childAsSpan);
            }
        }

        public int GetChildIndex(XF.Element child)
        {
            // There are two cases to consider:
            // 1. A Xamarin.Forms Label can have only 1 child (a FormattedString), so the child's index is always 0.
            // 2. But to simplify things, in MobileBlazorBindings a Label can contain a Span directly, so if the child
            //    is a Span, we have to compute its sibling index.

            return child switch
            {
                XF.Span span => LabelControl.FormattedText?.Spans.IndexOf(span) ?? -1,
                XF.FormattedString formattedString when LabelControl.FormattedText == formattedString => 0,
                _ => -1
            };
        }

        public virtual void RemoveChild(XF.Element child)
        {
            var childAsSpan = child as XF.Span;

            var formattedString = GetFormattedString();
            formattedString.Spans.Remove(childAsSpan);
        }

        private XF.FormattedString GetFormattedString()
        {
            if (LabelControl.FormattedText == null)
            {
                LabelControl.FormattedText = new XF.FormattedString();
            }
            return LabelControl.FormattedText;
        }
    }
}
