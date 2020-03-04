// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FormattedStringHandler : ElementHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsSpan = child as XF.Span;

            if (physicalSiblingIndex <= FormattedStringControl.Spans.Count)
            {
                FormattedStringControl.Spans.Insert(physicalSiblingIndex, childAsSpan);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but FormattedStringControl.Spans.Count={FormattedStringControl.Spans.Count}");
                FormattedStringControl.Spans.Add(childAsSpan);
            }
        }
    }
}
