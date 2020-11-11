// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ContentPageHandler : TemplatedPageHandler, IXamarinFormsContainerElementHandler
    {
        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as XF.View;
            ContentPageControl.Content = childAsView;
        }

        public int GetChildIndex(XF.Element child)
        {
            return ContentPageControl.Content == child ? 0 : -1;
        }

        public virtual void RemoveChild(XF.Element child)
        {
            if (ContentPageControl.Content == child)
            {
                ContentPageControl.Content = null;
            }
        }
    }
}
