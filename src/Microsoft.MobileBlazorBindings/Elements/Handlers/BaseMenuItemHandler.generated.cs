// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public abstract partial class BaseMenuItemHandler : ElementHandler
    {

        public BaseMenuItemHandler(NativeComponentRenderer renderer, XF.BaseMenuItem baseMenuItemControl) : base(renderer, baseMenuItemControl)
        {
            BaseMenuItemControl = baseMenuItemControl ?? throw new ArgumentNullException(nameof(baseMenuItemControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.BaseMenuItem BaseMenuItemControl { get; }
    }
}
