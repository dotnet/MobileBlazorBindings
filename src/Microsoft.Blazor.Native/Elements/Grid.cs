using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Blazor.Native.Elements.GridInternals;
using Microsoft.Blazor.Native.Elements.Handlers;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class Grid : Layout
    {
        static Grid()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<Grid>(renderer => new GridHandler(renderer, new XF.Grid()));
        }

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

        [Parameter] public double? ColumnSpacing { get; set; }
        [Parameter] public double? RowSpacing { get; set; }

        // TODO: Maybe replace all this with ChildContents. All handling will probably go directly to the grid anyway
        [Parameter] public RenderFragment Contents { get; set; }
        [Parameter] public RenderFragment Layout { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (ColumnSpacing != null)
            {
                builder.AddAttribute(nameof(ColumnSpacing), AttributeHelper.DoubleToString(ColumnSpacing.Value));
            }
            if (RowSpacing != null)
            {
                builder.AddAttribute(nameof(RowSpacing), AttributeHelper.DoubleToString(RowSpacing.Value));
            }
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
