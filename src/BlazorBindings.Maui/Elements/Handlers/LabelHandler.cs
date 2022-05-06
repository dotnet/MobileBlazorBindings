// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler, IMauiContainerElementHandler
    {
        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsSpan = child as MC.Span;

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

        public int GetChildIndex(MC.Element child)
        {
            // There are two cases to consider:
            // 1. A Xamarin.Forms Label can have only 1 child (a FormattedString), so the child's index is always 0.
            // 2. But to simplify things, in MobileBlazorBindings a Label can contain a Span directly, so if the child
            //    is a Span, we have to compute its sibling index.

            return child switch
            {
                MC.Span span => LabelControl.FormattedText?.Spans.IndexOf(span) ?? -1,
                MC.FormattedString formattedString when LabelControl.FormattedText == formattedString => 0,
                _ => -1
            };
        }

        public virtual void RemoveChild(MC.Element child)
        {
            var childAsSpan = child as MC.Span;

            var formattedString = GetFormattedString();
            formattedString.Spans.Remove(childAsSpan);
        }

        private MC.FormattedString GetFormattedString()
        {
            if (LabelControl.FormattedText == null)
            {
                LabelControl.FormattedText = new MC.FormattedString();
            }
            return LabelControl.FormattedText;
        }
    }
}
