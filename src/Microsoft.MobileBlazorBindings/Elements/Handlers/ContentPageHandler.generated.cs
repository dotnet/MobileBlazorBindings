// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ContentPageHandler : TemplatedPageHandler
    {
        public ContentPageHandler(NativeComponentRenderer renderer, XF.ContentPage contentPageControl) : base(renderer, contentPageControl)
        {
            ContentPageControl = contentPageControl ?? throw new ArgumentNullException(nameof(contentPageControl));
        }

        public XF.ContentPage ContentPageControl { get; }
    }
}
