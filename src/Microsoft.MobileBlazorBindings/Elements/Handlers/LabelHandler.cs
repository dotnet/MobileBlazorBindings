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
