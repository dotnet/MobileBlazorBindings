// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GridHandler : LayoutHandler
    {
        partial void ApplyColumnDefinitions(object attributeValue)
        {
            GridControl.ColumnDefinitions.Clear();

            var columnDefinitionConverter = new XF.ColumnDefinitionCollectionTypeConverter();
            var columnDefinitions = (XF.ColumnDefinitionCollection)columnDefinitionConverter.ConvertFromInvariantString((string)attributeValue);
            foreach (var column in columnDefinitions)
            {
                GridControl.ColumnDefinitions.Add(column);
            }
        }

        partial void ApplyRowDefinitions(object attributeValue)
        {
            GridControl.RowDefinitions.Clear();

            var rowDefinitionConverter = new XF.RowDefinitionCollectionTypeConverter();
            var rowDefinitions = (XF.RowDefinitionCollection)rowDefinitionConverter.ConvertFromInvariantString((string)attributeValue);
            foreach (var row in rowDefinitions)
            {
                GridControl.RowDefinitions.Add(row);
            }
        }
    }
}
