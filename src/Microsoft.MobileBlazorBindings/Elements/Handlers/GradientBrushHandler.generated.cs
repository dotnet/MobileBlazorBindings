// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class GradientBrushHandler : BrushHandler
    {

        public GradientBrushHandler(NativeComponentRenderer renderer, XF.GradientBrush gradientBrushControl) : base(renderer, gradientBrushControl)
        {
            GradientBrushControl = gradientBrushControl ?? throw new ArgumentNullException(nameof(gradientBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.GradientBrush GradientBrushControl { get; }
    }
}
