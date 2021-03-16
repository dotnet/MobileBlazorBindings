// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class SelectableItemsView<T> : StructuredItemsView<T>
    {
        //Changing source at run time is valid behavior so do not make read only
#pragma warning disable CA2227 // Collection properties should be read only.
        [Parameter] public IList<object> SelectedItems { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
        [Parameter] public object SelectedItem { get; set; }
        [Parameter] public XF.SelectionMode? SelectionMode { get; set; }

        [Parameter] public EventCallback<XF.SelectionChangedEventArgs> OnSelectionChanged { get; set; }
        [Parameter] public EventCallback<IList<object>> SelectedItemsChanged { get; set; }
        [Parameter] public EventCallback<object> SelectedItemChanged { get; set; }

        public new XF.SelectableItemsView NativeControl => ((SelectableItemsViewHandler)ElementHandler).SelectableItemsViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (SelectedItems != null)
            {
                builder.AddAttribute(nameof(SelectedItems), AttributeHelper.ObjectToDelegate(SelectedItems));
            }

            if (SelectedItem != null)
            {
                builder.AddAttribute(nameof(SelectedItem), AttributeHelper.ObjectToDelegate(SelectedItem));
            }
            if (SelectionMode != null)
            {
                builder.AddAttribute(nameof(SelectionMode), (int)SelectionMode.Value);
            }

            builder.AddAttribute("onselectionchanged", OnSelectionChanged);
            builder.AddAttribute("onselecteditemchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, e => SelectedItemChanged.InvokeAsync(e.Value)));
            builder.AddAttribute("onselecteditemschanged", EventCallback.Factory.Create<ChangeEventArgs>(this, e => SelectedItemsChanged.InvokeAsync((IList<object>)e.Value)));
        }
    }
}
