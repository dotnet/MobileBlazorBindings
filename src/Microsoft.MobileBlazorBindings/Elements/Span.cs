// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Span : GestureElement
    {

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<GestureElement>>(1);
            builder.AddAttribute(2, "Name", "GestureElement");
            builder.AddAttribute(3, "Value", this);

            RenderFragment child = (__builder1) => __builder1.AddContent(5, (__builder2) => base.BuildRenderTree(__builder2));
            builder.AddAttribute(4, "ChildContent", child);
            builder.CloseComponent();
        }

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}