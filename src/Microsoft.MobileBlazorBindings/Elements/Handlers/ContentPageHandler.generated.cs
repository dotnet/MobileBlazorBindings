// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ContentPageHandler : TemplatedPageHandler
    {

        public ContentPageHandler(NativeComponentRenderer renderer, MC.ContentPage contentPageControl) : base(renderer, contentPageControl)
        {
            ContentPageControl = contentPageControl ?? throw new ArgumentNullException(nameof(contentPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ContentPage ContentPageControl { get; }
    }
}
