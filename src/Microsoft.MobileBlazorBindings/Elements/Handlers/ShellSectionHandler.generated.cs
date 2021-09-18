// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellSectionHandler : ShellGroupItemHandler
    {

        public ShellSectionHandler(NativeComponentRenderer renderer, MC.ShellSection shellSectionControl) : base(renderer, shellSectionControl)
        {
            ShellSectionControl = shellSectionControl ?? throw new ArgumentNullException(nameof(shellSectionControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ShellSection ShellSectionControl { get; }
    }
}
