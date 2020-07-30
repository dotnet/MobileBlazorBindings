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
            if (attributeEventHandlerId != 0)
            {
                ApplyEventHandlerId(attributeName, attributeEventHandlerId);
            }

            switch (attributeName)
            {
                case nameof(XF.Grid.ColumnSpacing):
                    GridControl.ColumnSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Grid.RowSpacing):
                    GridControl.RowSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;

                // TODO: Need a better long-term solution to this because this isn't compatible with generated code
                case nameof(Grid.ColumnDefinitions):
                    ApplyColumnDefinitions(attributeValue);
                    break;
                case nameof(Grid.RowDefinitions):
                    ApplyRowDefinitions(attributeValue);
                    break;

                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        partial void ApplyColumnDefinitions(object attributeValue);
        partial void ApplyRowDefinitions(object attributeValue);

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId);
    }
}
