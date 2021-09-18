// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
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
