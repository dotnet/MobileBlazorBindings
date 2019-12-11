// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class TabbedPageHandler : PageHandler
    {
        public TabbedPageHandler(NativeComponentRenderer renderer, XF.TabbedPage tabbedPageControl) : base(renderer, tabbedPageControl)
        {
            TabbedPageControl = tabbedPageControl ?? throw new System.ArgumentNullException(nameof(tabbedPageControl));
        }

        public XF.TabbedPage TabbedPageControl { get; }
    }
}
