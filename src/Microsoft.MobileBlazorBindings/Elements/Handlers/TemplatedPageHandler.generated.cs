// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TemplatedPageHandler : PageHandler
    {

        public TemplatedPageHandler(NativeComponentRenderer renderer, XF.TemplatedPage templatedPageControl) : base(renderer, templatedPageControl)
        {
            TemplatedPageControl = templatedPageControl ?? throw new ArgumentNullException(nameof(templatedPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.TemplatedPage TemplatedPageControl { get; }
    }
}
