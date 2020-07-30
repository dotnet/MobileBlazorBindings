// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class TimePicker : View
    {
        [Parameter] public EventCallback<TimeSpan> TimeChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("ontimechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleTimeChanged));
        }

        private Task HandleTimeChanged(ChangeEventArgs evt)
        {
            return TimeChanged.InvokeAsync((TimeSpan)evt.Value);
        }
    }
}
