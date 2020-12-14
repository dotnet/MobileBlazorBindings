// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ScrollView : Layout
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
