// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TabBarHandler : ShellItemHandler
    {

        public TabBarHandler(NativeComponentRenderer renderer, MC.TabBar tabBarControl) : base(renderer, tabBarControl)
        {
            TabBarControl = tabBarControl ?? throw new ArgumentNullException(nameof(tabBarControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.TabBar TabBarControl { get; }
    }
}
