// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class HorizontalStackLayoutHandler : StackBaseHandler
    {

        public HorizontalStackLayoutHandler(NativeComponentRenderer renderer, MC.HorizontalStackLayout horizontalStackLayoutControl) : base(renderer, horizontalStackLayoutControl)
        {
            HorizontalStackLayoutControl = horizontalStackLayoutControl ?? throw new ArgumentNullException(nameof(horizontalStackLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.HorizontalStackLayout HorizontalStackLayoutControl { get; }
    }
}
