// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Slider : View
    {
        [Parameter] public EventCallback OnDragCompleted { get; set; }
        [Parameter] public EventCallback OnDragStarted { get; set; }
        [Parameter] public EventCallback<double> ValueChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("ondragcompleted", OnDragCompleted);
            builder.AddAttribute("ondragstarted", OnDragStarted);
            builder.AddAttribute("onvaluechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleValueChanged));
        }

        private Task HandleValueChanged(ChangeEventArgs evt)
        {
            return ValueChanged.InvokeAsync((double)evt.Value);
        }
    }
}
