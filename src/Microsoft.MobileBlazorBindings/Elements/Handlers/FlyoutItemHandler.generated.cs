// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FlyoutItemHandler : ShellItemHandler
    {

        public FlyoutItemHandler(NativeComponentRenderer renderer, XF.FlyoutItem flyoutItemControl) : base(renderer, flyoutItemControl)
        {
            FlyoutItemControl = flyoutItemControl ?? throw new ArgumentNullException(nameof(flyoutItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.FlyoutItem FlyoutItemControl { get; }
    }
}
