// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class GroupableItemsView<T> : SelectableItemsView<T>
    {
        // Grouping is not supported at this moment
        // [Parameter] public bool? IsGrouped { get; set; }

        public new MC.GroupableItemsView NativeControl => ((GroupableItemsViewHandler)ElementHandler).GroupableItemsViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            //if (IsGrouped != null)
            //{
            //    builder.AddAttribute(nameof(IsGrouped), IsGrouped.Value);
            //}
        }
    }
}
