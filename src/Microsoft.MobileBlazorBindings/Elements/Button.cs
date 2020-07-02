// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Button : View
    {
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public EventCallback OnPress { get; set; }
        [Parameter] public EventCallback OnRelease { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onclick", OnClick);
            builder.AddAttribute("onpress", OnPress);
            builder.AddAttribute("onrelease", OnRelease);
        }
    }
}
