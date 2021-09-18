// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ToolbarItem : MenuItem
    {
        static ToolbarItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ToolbarItem>(
                renderer => new ToolbarItemHandler(renderer, new MC.ToolbarItem()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.ToolbarItemOrder? Order { get; set; }
        [Parameter] public int? Priority { get; set; }

        public new MC.ToolbarItem NativeControl => ((ToolbarItemHandler)ElementHandler).ToolbarItemControl;

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
