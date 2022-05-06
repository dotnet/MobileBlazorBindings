// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class GridHandler
    {
        private void ApplyColumnDefinitions(object attributeValue)
        {
            GridControl.ColumnDefinitions.Clear();

            var columnDefinitionConverter = new MC.ColumnDefinitionCollectionTypeConverter();
            var columnDefinitions = (MC.ColumnDefinitionCollection)columnDefinitionConverter.ConvertFromInvariantString((string)attributeValue);
            foreach (var column in columnDefinitions)
            {
                GridControl.ColumnDefinitions.Add(column);
            }
        }

        private void ApplyRowDefinitions(object attributeValue)
        {
            GridControl.RowDefinitions.Clear();

            var rowDefinitionConverter = new MC.RowDefinitionCollectionTypeConverter();
            var rowDefinitions = (MC.RowDefinitionCollection)rowDefinitionConverter.ConvertFromInvariantString((string)attributeValue);
            foreach (var row in rowDefinitions)
            {
                GridControl.RowDefinitions.Add(row);
            }
        }

        public override bool ApplyAdditionalAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(Grid.ColumnDefinitions):
                    ApplyColumnDefinitions(attributeValue);
                    return true;
                case nameof(Grid.RowDefinitions):
                    ApplyRowDefinitions(attributeValue);
                    return true;
                default:
                    return base.ApplyAdditionalAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
