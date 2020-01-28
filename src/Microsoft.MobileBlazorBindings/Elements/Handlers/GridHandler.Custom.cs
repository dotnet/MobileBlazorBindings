// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Elements.GridInternals;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GridHandler : LayoutHandler
    {
        partial void ApplyGridMetadata(object attributeValue)
        {
            GridControl.RowDefinitions.Clear();
            GridControl.ColumnDefinitions.Clear();
            var gridMetadata = System.Text.Json.JsonSerializer.Deserialize<GridMetadataForDeserialization>((string)attributeValue);
            foreach (var row in gridMetadata.RowDefinitions)
            {
                GridControl.RowDefinitions.Add(new XF.RowDefinition { Height = GetGridLength(row.Height, row.GridUnitType) });
            }
            foreach (var column in gridMetadata.ColumnDefinitions)
            {
                GridControl.ColumnDefinitions.Add(new XF.ColumnDefinition { Width = GetGridLength(column.Width, column.GridUnitType) });
            }
        }

        private static XF.GridLength GetGridLength(double? length, XF.GridUnitType? gridUnitType)
        {
            if (!gridUnitType.HasValue)
            {
                gridUnitType = XF.GridUnitType.Absolute;
            }
            return gridUnitType.Value switch
            {
                XF.GridUnitType.Absolute => new XF.GridLength(length.Value),
                XF.GridUnitType.Star => XF.GridLength.Star,
                XF.GridUnitType.Auto => XF.GridLength.Auto,
                _ => throw new ArgumentException("Arguments represent an invalid grid length."),
            };
        }
    }
}
