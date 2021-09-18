// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ContentViewHandler : TemplatedViewHandler
    {

        public ContentViewHandler(NativeComponentRenderer renderer, MC.ContentView contentViewControl) : base(renderer, contentViewControl)
        {
            ContentViewControl = contentViewControl ?? throw new ArgumentNullException(nameof(contentViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ContentView ContentViewControl { get; }
    }
}
