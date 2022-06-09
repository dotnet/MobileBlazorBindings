// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FlyoutItemHandler : ShellItemHandler
    {

        public FlyoutItemHandler(NativeComponentRenderer renderer, MC.FlyoutItem flyoutItemControl) : base(renderer, flyoutItemControl)
        {
            FlyoutItemControl = flyoutItemControl ?? throw new ArgumentNullException(nameof(flyoutItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.FlyoutItem FlyoutItemControl { get; }
    }
}
