// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.GridInternals;
using System;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Grid : Layout
    {
        public Grid()
        {
            GridMetadata = new GridMetadata();
            GridMetadata.StateHasChanged += OnGridMetadataStateHasChanged;
        }

        private void OnGridMetadataStateHasChanged(object sender, EventArgs e)
        {
            StateHasChanged();
        }

        private GridMetadata GridMetadata { get; } // Used as a CascadingValue for certain child components, such as Row/Column definitions

        // TODO: Maybe replace all this with ChildContents. All handling will probably go directly to the grid anyway
        [Parameter] public RenderFragment Contents { get; set; }
        [Parameter] public RenderFragment Layout { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute(nameof(GridMetadata), System.Text.Json.JsonSerializer.Serialize(GridMetadata));
        }

        protected override RenderFragment GetChildContent() => builder =>
        {
            builder.OpenComponent<CascadingValue<GridMetadata>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<GridMetadata>.Value), GridMetadata);

            builder.AddAttribute(2, nameof(CascadingValue<GridMetadata>.ChildContent), (RenderFragment)(builder =>
            {
                builder.AddContent(0, Layout);
            }));

            builder.CloseComponent(); // CascadingValue<GridMetadata>

            builder.AddContent(1, Contents);
        };
    }
}
