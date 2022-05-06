// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class InputView : View
    {
        [Parameter] public EventCallback<string> TextChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("ontextchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleTextChanged));
        }

        private Task HandleTextChanged(ChangeEventArgs evt)
        {
            return TextChanged.InvokeAsync((string)evt.Value);
        }
    }
}
