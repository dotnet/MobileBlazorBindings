// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Button : View
    {
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public EventCallback OnPress { get; set; }
        [Parameter] public EventCallback OnRelease { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onclick", OnClick);
            builder.AddAttribute("onpress", OnPress);
            builder.AddAttribute("onrelease", OnRelease);
        }
    }
}
