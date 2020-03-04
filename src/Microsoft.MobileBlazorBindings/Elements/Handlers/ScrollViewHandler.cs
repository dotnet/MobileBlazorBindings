// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ScrollViewHandler : LayoutHandler
    {
        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            var childAsView = child as XF.View;
            ScrollViewControl.Content = childAsView;
        }
    }
}
