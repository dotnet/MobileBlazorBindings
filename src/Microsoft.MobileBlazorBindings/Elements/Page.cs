// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Page : VisualElement
    {
        /// <summary>
        /// Indicates that the <see cref="Xamarin.Forms.Page" /> is about to appear.
        /// </summary>
        [Parameter] public EventCallback OnAppearing { get; set; }

        /// <summary>
        /// Indicates that the <see cref="Xamarin.Forms.Page" /> is about to cease displaying.
        /// </summary>
        [Parameter] public EventCallback OnDisappearing { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onappearing", OnAppearing);
            builder.AddAttribute("ondisappearing", OnDisappearing);
        }
    }
}
