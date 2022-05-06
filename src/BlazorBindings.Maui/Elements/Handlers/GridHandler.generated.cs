// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class GridHandler : LayoutHandler
    {
        private static readonly double ColumnSpacingDefaultValue = MC.Grid.ColumnSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly double RowSpacingDefaultValue = MC.Grid.RowSpacingProperty.DefaultValue is double value ? value : default;

        public GridHandler(NativeComponentRenderer renderer, MC.Grid gridControl) : base(renderer, gridControl)
        {
            GridControl = gridControl ?? throw new ArgumentNullException(nameof(gridControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Grid GridControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Grid.ColumnSpacing):
                    GridControl.ColumnSpacing = AttributeHelper.StringToDouble((string)attributeValue, ColumnSpacingDefaultValue);
                    break;
                case nameof(MC.Grid.RowSpacing):
                    GridControl.RowSpacing = AttributeHelper.StringToDouble((string)attributeValue, RowSpacingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
