// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class GridHandler : LayoutHandler
    {
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
                    GridControl.ColumnSpacing = AttributeHelper.StringToDouble((string)attributeValue, 6.00);
                    break;
                case nameof(XF.Grid.RowSpacing):
                    GridControl.RowSpacing = AttributeHelper.StringToDouble((string)attributeValue, 6.00);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
