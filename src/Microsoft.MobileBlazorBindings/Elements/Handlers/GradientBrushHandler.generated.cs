// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class GradientBrushHandler : BrushHandler
    {

        public GradientBrushHandler(NativeComponentRenderer renderer, MC.GradientBrush gradientBrushControl) : base(renderer, gradientBrushControl)
        {
            GradientBrushControl = gradientBrushControl ?? throw new ArgumentNullException(nameof(gradientBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.GradientBrush GradientBrushControl { get; }
    }
}
