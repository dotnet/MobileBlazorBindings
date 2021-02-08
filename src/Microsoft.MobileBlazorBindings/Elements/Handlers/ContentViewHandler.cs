// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ContentViewHandler : TemplatedViewHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as XF.View;
            ContentViewControl.Content = childAsView;
        }

        public override int GetChildIndex(XF.Element child)
        {
            return ContentViewControl.Content == child ? 0 : -1;
        }

        public override void RemoveChild(XF.Element child)
        {
            if (ContentViewControl.Content == child)
            {
                ContentViewControl.Content = null;
            }
        }
    }
}
