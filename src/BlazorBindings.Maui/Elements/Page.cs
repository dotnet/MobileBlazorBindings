// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Page : VisualElement
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<Page>(nameof(ToolbarItems),
                _ => new ListContentPropertyHandler<MC.Page, MC.ToolbarItem>(page => page.ToolbarItems));
        }

        /// <summary>
        /// Indicates that the <see cref="MC.Page" /> is about to appear.
        /// </summary>
        [Parameter] public EventCallback OnAppearing { get; set; }

        /// <summary>
        /// Indicates that the <see cref="MC.Page" /> is about to cease displaying.
        /// </summary>
        [Parameter] public EventCallback OnDisappearing { get; set; }

        [Parameter] public RenderFragment ToolbarItems { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Page), nameof(ToolbarItems), ToolbarItems);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onappearing", OnAppearing);
            builder.AddAttribute("ondisappearing", OnDisappearing);
        }
    }
}
