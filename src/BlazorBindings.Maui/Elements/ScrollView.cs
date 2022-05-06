// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements
{
    public partial class ScrollView
    {
        /// <summary>
        /// Event that is raised after a scroll completes.
        /// </summary>
        [Parameter] public EventCallback<ScrolledEventArgs> OnScrolled { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onscrolled", OnScrolled);
        }
    }
}
