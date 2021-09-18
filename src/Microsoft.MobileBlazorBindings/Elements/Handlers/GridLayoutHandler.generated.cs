// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GridLayoutHandler : LayoutHandler
    {
        private static readonly double ColumnSpacingDefaultValue = MC.GridLayout.ColumnSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly double RowSpacingDefaultValue = MC.GridLayout.RowSpacingProperty.DefaultValue is double value ? value : default;

        public GridLayoutHandler(NativeComponentRenderer renderer, MC.GridLayout gridLayoutControl) : base(renderer, gridLayoutControl)
        {
            GridLayoutControl = gridLayoutControl ?? throw new ArgumentNullException(nameof(gridLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.GridLayout GridLayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.GridLayout.ColumnSpacing):
                    GridLayoutControl.ColumnSpacing = AttributeHelper.StringToDouble((string)attributeValue, ColumnSpacingDefaultValue);
                    break;
                case nameof(MC.GridLayout.RowSpacing):
                    GridLayoutControl.RowSpacing = AttributeHelper.StringToDouble((string)attributeValue, RowSpacingDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
