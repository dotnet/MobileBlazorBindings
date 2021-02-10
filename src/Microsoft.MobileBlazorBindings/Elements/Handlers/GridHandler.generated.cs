// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GridHandler : LayoutHandler
    {
        private static readonly double ColumnSpacingDefaultValue = XF.Grid.ColumnSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly double RowSpacingDefaultValue = XF.Grid.RowSpacingProperty.DefaultValue is double value ? value : default;

        public GridHandler(NativeComponentRenderer renderer, XF.Grid gridControl) : base(renderer, gridControl)
        {
            GridControl = gridControl ?? throw new ArgumentNullException(nameof(gridControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Grid GridControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Grid.ColumnSpacing):
                    GridControl.ColumnSpacing = AttributeHelper.StringToDouble((string)attributeValue, ColumnSpacingDefaultValue);
                    break;
                case nameof(XF.Grid.RowSpacing):
                    GridControl.RowSpacing = AttributeHelper.StringToDouble((string)attributeValue, RowSpacingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
