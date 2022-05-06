// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements
{
    public partial class MenuItem : BaseMenuItem
    {
        [Parameter] public EventCallback OnClick { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onclick", OnClick);
        }
    }
}
