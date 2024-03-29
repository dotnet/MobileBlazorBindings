// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TemplatedViewHandler : Microsoft.MobileBlazorBindings.Elements.Compatibility.Handlers.LayoutHandler
    {

        public TemplatedViewHandler(NativeComponentRenderer renderer, MC.TemplatedView templatedViewControl) : base(renderer, templatedViewControl)
        {
            TemplatedViewControl = templatedViewControl ?? throw new ArgumentNullException(nameof(templatedViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.TemplatedView TemplatedViewControl { get; }
    }
}
