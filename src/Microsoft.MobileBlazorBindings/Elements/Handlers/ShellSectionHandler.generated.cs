// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellSectionHandler : ShellGroupItemHandler
    {

        public ShellSectionHandler(NativeComponentRenderer renderer, XF.ShellSection shellSectionControl) : base(renderer, shellSectionControl)
        {
            ShellSectionControl = shellSectionControl ?? throw new ArgumentNullException(nameof(shellSectionControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.ShellSection ShellSectionControl { get; }
    }
}
