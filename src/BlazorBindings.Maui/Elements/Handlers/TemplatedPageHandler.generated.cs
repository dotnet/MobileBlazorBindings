// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class TemplatedPageHandler : PageHandler
    {

        public TemplatedPageHandler(NativeComponentRenderer renderer, MC.TemplatedPage templatedPageControl) : base(renderer, templatedPageControl)
        {
            TemplatedPageControl = templatedPageControl ?? throw new ArgumentNullException(nameof(templatedPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.TemplatedPage TemplatedPageControl { get; }
    }
}
