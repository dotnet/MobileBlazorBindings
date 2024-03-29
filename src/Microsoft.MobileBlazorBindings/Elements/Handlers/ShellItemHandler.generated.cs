// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellItemHandler : ShellGroupItemHandler
    {

        public ShellItemHandler(NativeComponentRenderer renderer, MC.ShellItem shellItemControl) : base(renderer, shellItemControl)
        {
            ShellItemControl = shellItemControl ?? throw new ArgumentNullException(nameof(shellItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ShellItem ShellItemControl { get; }
    }
}
