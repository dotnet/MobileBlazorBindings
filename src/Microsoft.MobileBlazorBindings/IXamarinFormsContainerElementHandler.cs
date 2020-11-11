// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public interface IXamarinFormsContainerElementHandler : IXamarinFormsElementHandler
    {
        void AddChild(XF.Element child, int physicalSiblingIndex);
        void RemoveChild(XF.Element child);
        int GetChildIndex(XF.Element child);
    }
}
