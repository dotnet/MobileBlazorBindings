// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class FormattedString : Element
    {
        [Parameter] public RenderFragment Spans { get; set; }

        protected override RenderFragment GetChildContent() => Spans;
    }
}
