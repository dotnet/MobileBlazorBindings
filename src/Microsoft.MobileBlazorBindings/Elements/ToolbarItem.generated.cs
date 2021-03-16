// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ToolbarItem : MenuItem
    {
        static ToolbarItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ToolbarItem>(
                renderer => new ToolbarItemHandler(renderer, new XF.ToolbarItem()));

            RegisterAdditionalHandlers();
        }

        /// <summary>
        /// Gets or sets a value that indicates on which of the primary, secondary, or default toolbar surfaces to display this <see cref="T:Xamarin.Forms.ToolbarItem" /> element.
        /// </summary>
        [Parameter] public XF.ToolbarItemOrder? Order { get; set; }
        /// <summary>
        /// Gets or sets the priority of this <see cref="T:Xamarin.Forms.ToolbarItem" /> element.
        /// </summary>
        [Parameter] public int? Priority { get; set; }

        public new XF.ToolbarItem NativeControl => ((ToolbarItemHandler)ElementHandler).ToolbarItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Order != null)
            {
                builder.AddAttribute(nameof(Order), (int)Order.Value);
            }
            if (Priority != null)
            {
                builder.AddAttribute(nameof(Priority), Priority.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
