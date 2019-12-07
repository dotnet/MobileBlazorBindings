using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.GridInternals;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class GridHandler : LayoutHandler
    {
        public GridHandler(NativeComponentRenderer renderer, XF.Grid gridControl) : base(renderer, gridControl)
        {
            GridControl = gridControl ?? throw new ArgumentNullException(nameof(gridControl));
        }

        public XF.Grid GridControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Grid.ColumnSpacing):
                    GridControl.ColumnSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Grid.RowSpacing):
                    GridControl.RowSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(GridMetadata):
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
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
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
