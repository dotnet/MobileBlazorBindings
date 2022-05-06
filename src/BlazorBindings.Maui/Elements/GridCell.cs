// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;

namespace BlazorBindings.Maui.Elements
{
    public class GridCell : NativeControlComponentBase
    {
        static GridCell()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<GridCell>(renderer => new GridCellHandler(renderer, new GridCellPlaceholderElement()));
        }

        [Parameter] public int? Column { get; set; }
        [Parameter] public int? ColumnSpan { get; set; }
        [Parameter] public int? Row { get; set; }
        [Parameter] public int? RowSpan { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Column != null)
            {
                builder.AddAttribute(nameof(Column), Column.Value);
            }
            if (ColumnSpan != null)
            {
                builder.AddAttribute(nameof(ColumnSpan), ColumnSpan.Value);
            }
            if (Row != null)
            {
                builder.AddAttribute(nameof(Row), Row.Value);
            }
            if (RowSpan != null)
            {
                builder.AddAttribute(nameof(RowSpan), RowSpan.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
