// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GridHandler : GridLayoutHandler
    {

        public GridHandler(NativeComponentRenderer renderer, MC.Grid gridControl) : base(renderer, gridControl)
        {
            GridControl = gridControl ?? throw new ArgumentNullException(nameof(gridControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Grid GridControl { get; }
    }
}
