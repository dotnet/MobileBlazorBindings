// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Grid : Layout
    {
        static Grid()
        {
            ElementHandlerRegistry.RegisterElementHandler<Grid>(
                renderer => new GridHandler(renderer, new MC.Grid()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? ColumnSpacing { get; set; }
        [Parameter] public double? RowSpacing { get; set; }

        public new MC.Grid NativeControl => ((GridHandler)ElementHandler).GridControl;

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
