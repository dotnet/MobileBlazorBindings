// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract partial class BrushHandler : ElementHandler
    {

        public BrushHandler(NativeComponentRenderer renderer, MC.Brush brushControl) : base(renderer, brushControl)
        {
            BrushControl = brushControl ?? throw new ArgumentNullException(nameof(brushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Brush BrushControl { get; }
    }
}
