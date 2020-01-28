// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Label : View
    {
        [Parameter] public RenderFragment FormattedText { get; set; }

        protected override RenderFragment GetChildContent() => FormattedText;
    }
}
