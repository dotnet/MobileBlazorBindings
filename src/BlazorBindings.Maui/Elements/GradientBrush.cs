// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class GradientBrush : Brush
    {
#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
