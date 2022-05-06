// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract partial class BaseMenuItemHandler : ElementHandler
    {

        public BaseMenuItemHandler(NativeComponentRenderer renderer, MC.BaseMenuItem baseMenuItemControl) : base(renderer, baseMenuItemControl)
        {
            BaseMenuItemControl = baseMenuItemControl ?? throw new ArgumentNullException(nameof(baseMenuItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.BaseMenuItem BaseMenuItemControl { get; }
    }
}
