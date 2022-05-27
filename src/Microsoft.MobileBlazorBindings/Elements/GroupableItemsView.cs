// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class GroupableItemsView<T> : SelectableItemsView<T>
    {
        // Grouping is not supported at this moment
        // [Parameter] public bool? IsGrouped { get; set; }

        public new XF.GroupableItemsView NativeControl => ((GroupableItemsViewHandler)ElementHandler).GroupableItemsViewControl;

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
