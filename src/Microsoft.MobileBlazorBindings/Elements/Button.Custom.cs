// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Button : View
    {
        [Parameter] public EventCallback OnClick { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onclick", OnClick);
        }
    }
}
