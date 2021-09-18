// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class GridLayout : Layout
    {
        static GridLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<GridLayout>(
                renderer => new GridLayoutHandler(renderer, new MC.GridLayout()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? ColumnSpacing { get; set; }
        [Parameter] public double? RowSpacing { get; set; }

        public new MC.GridLayout NativeControl => ((GridLayoutHandler)ElementHandler).GridLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (ColumnSpacing != null)
            {
                builder.AddAttribute(nameof(ColumnSpacing), AttributeHelper.DoubleToString(ColumnSpacing.Value));
            }
            if (RowSpacing != null)
            {
                builder.AddAttribute(nameof(RowSpacing), AttributeHelper.DoubleToString(RowSpacing.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
