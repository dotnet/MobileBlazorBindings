// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class BaseShellItem : NavigableElement
    {
        [Parameter] public EventCallback OnAppearing { get; set; }
        [Parameter] public EventCallback OnDisappearing { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onappearing", OnAppearing);
            builder.AddAttribute("ondisappearing", OnDisappearing);
        }
    }
}
