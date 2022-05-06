// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class TabHandler : ShellSectionHandler
    {

        public TabHandler(NativeComponentRenderer renderer, MC.Tab tabControl) : base(renderer, tabControl)
        {
            TabControl = tabControl ?? throw new ArgumentNullException(nameof(tabControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Tab TabControl { get; }
    }
}
