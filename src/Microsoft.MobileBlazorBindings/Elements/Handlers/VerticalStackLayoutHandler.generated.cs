// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class VerticalStackLayoutHandler : StackBaseHandler
    {

        public VerticalStackLayoutHandler(NativeComponentRenderer renderer, MC.VerticalStackLayout verticalStackLayoutControl) : base(renderer, verticalStackLayoutControl)
        {
            VerticalStackLayoutControl = verticalStackLayoutControl ?? throw new ArgumentNullException(nameof(verticalStackLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.VerticalStackLayout VerticalStackLayoutControl { get; }
    }
}
