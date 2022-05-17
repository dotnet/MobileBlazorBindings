// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Grid
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.Column",
                (element, value) => MC.Grid.SetColumn(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.ColumnSpan",
                (element, value) => MC.Grid.SetColumnSpan(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.Row",
                (element, value) => MC.Grid.SetRow(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.RowSpan",
                (element, value) => MC.Grid.SetRowSpan(element, AttributeHelper.GetInt(value)));
        }

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
