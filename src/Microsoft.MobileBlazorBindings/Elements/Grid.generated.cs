// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Grid : Layout
    {
        static Grid()
        {
            ElementHandlerRegistry.RegisterElementHandler<Grid>(
                renderer => new GridHandler(renderer, new XF.Grid()));

            RegisterAdditionalHandlers();
        }

        /// <summary>
        /// Provides the interface for the bound property that gets or sets the distance between columns in the Grid.
        /// </summary>
        /// <value>
        /// The space between columns in this <see cref="T:Xamarin.Forms.Grid" /> layout. The default is 6.
        /// </value>
        [Parameter] public double? ColumnSpacing { get; set; }
        /// <summary>
        /// Gets or sets the amount of space left between rows in the Grid. This is a bindable property.
        /// </summary>
        /// <value>
        /// The space between rows
        /// </value>
        [Parameter] public double? RowSpacing { get; set; }

        public new XF.Grid NativeControl => ((GridHandler)ElementHandler).GridControl;

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
