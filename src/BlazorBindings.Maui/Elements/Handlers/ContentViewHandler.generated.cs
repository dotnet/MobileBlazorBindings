// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
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
