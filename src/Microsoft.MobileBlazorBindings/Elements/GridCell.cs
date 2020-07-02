// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;

namespace Microsoft.MobileBlazorBindings.Elements
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
