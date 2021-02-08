// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TabBarHandler : ShellItemHandler
    {

        public TabBarHandler(NativeComponentRenderer renderer, XF.TabBar tabBarControl) : base(renderer, tabBarControl)
        {
            TabBarControl = tabBarControl ?? throw new ArgumentNullException(nameof(tabBarControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.TabBar TabBarControl { get; }
    }
}
