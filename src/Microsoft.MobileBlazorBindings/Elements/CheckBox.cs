// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class CheckBox : View
    {
        [Parameter] public EventCallback<bool> IsCheckedChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onischeckedchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleIsCheckedChanged));
        }

        private Task HandleIsCheckedChanged(ChangeEventArgs evt)
        {
            return IsCheckedChanged.InvokeAsync((bool)evt.Value);
        }
    }
}
