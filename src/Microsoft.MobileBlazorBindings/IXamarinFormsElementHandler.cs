// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public interface IXamarinFormsElementHandler : IElementHandler
    {
        XF.Element ElementControl { get; }

        bool IsParented();
        bool IsParentedTo(XF.Element parent);
        void SetParent(XF.Element parent);

        /// <summary>
        /// Returns the index of this handler's target element within its parent's set of children.
        /// </summary>
        /// <returns></returns>
        int GetPhysicalSiblingIndex();
    }
}
