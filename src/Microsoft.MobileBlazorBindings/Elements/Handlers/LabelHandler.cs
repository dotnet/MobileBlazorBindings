// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsFormattedString = child as XF.FormattedString;

            if (physicalSiblingIndex == 0)
            {
                // Label can have exactly one child, which is a FormattedString
                LabelControl.FormattedText = childAsFormattedString;
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentAsLabel can have only 1 child");
            }
        }
    }
}
