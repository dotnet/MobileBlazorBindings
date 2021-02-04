// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TemplatedViewHandler : LayoutHandler
    {

        public TemplatedViewHandler(NativeComponentRenderer renderer, XF.TemplatedView templatedViewControl) : base(renderer, templatedViewControl)
        {
            TemplatedViewControl = templatedViewControl ?? throw new ArgumentNullException(nameof(templatedViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.TemplatedView TemplatedViewControl { get; }
    }
}
