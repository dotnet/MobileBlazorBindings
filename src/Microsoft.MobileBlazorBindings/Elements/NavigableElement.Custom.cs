﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class NavigableElement : Element
    {
        public async Task PopModalAsync(bool animated = true)
        {
            await NativeControl.Navigation.PopModalAsync(animated).ConfigureAwait(true);
        }
    }
}
