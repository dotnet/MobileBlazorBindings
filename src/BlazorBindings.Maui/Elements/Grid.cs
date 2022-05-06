// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements
{
    public partial class Grid
    {
        /// <summary>
        /// A comma-separated list of column definitions. A column definition can be:
        /// Auto-sized with the <c>Auto</c> keyword; A numeric size, such as <c>80.5</c>; Or a relative size, such as <c>*</c>, <c>2*</c>, or <c>3.5*</c>.
        /// </summary>
        [Parameter] public string ColumnDefinitions { get; set; }
        /// <summary>
        /// A comma-separated list of row definitions. A row definition can be:
        /// Auto-sized with the <c>Auto</c> keyword; A numeric size, such as <c>80.5</c>; Or a relative size, such as <c>*</c>, <c>2*</c>, or <c>3.5*</c>.
        /// </summary>
        [Parameter] public string RowDefinitions { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (ColumnDefinitions != null)
            {
                builder.AddAttribute(nameof(ColumnDefinitions), ColumnDefinitions);
            }
            if (RowDefinitions != null)
            {
                builder.AddAttribute(nameof(RowDefinitions), RowDefinitions);
            }
        }
    }
}
