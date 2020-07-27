// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class DatePicker : View
    {
        [Parameter] public EventCallback<DateTime> DateChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("ondatechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleDateChanged));
        }

        private Task HandleDateChanged(ChangeEventArgs evt)
        {
            return DateChanged.InvokeAsync((DateTime)evt.Value);
        }
    }
}
