// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements
{
    public partial class Entry : InputView
    {
        [Parameter] public EventCallback OnCompleted { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("oncompleted", OnCompleted);
        }
    }
}
