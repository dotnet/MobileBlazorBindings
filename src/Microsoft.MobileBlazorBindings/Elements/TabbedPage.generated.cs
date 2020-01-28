// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TabbedPage : Page
    {
        static TabbedPage()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<TabbedPage>(renderer => new TabbedPageHandler(renderer, new XF.TabbedPage()));
        }

        public new XF.TabbedPage NativeControl => ((TabbedPageHandler)ElementHandler).TabbedPageControl;
    }
}
