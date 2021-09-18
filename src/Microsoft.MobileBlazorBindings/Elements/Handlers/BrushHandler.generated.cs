// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class BrushHandler : ElementHandler
    {

        public BrushHandler(NativeComponentRenderer renderer, XF.Brush brushControl) : base(renderer, brushControl)
        {
            BrushControl = brushControl ?? throw new ArgumentNullException(nameof(brushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Brush BrushControl { get; }
    }
}
