// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ContentPageHandler : TemplatedPageHandler, IXamarinFormsContainerElementHandler
    {
        public void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as XF.View;
            ContentPageControl.Content = childAsView;
        }

        public void RemoveChild(XF.Element child)
        {
            if (ContentPageControl.Content == child)
            {
                ContentPageControl.Content = null;
            }
        }
    }
}
