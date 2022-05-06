// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
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
