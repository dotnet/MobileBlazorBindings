// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Switch : View
    {
        [Parameter] public EventCallback<bool> IsToggledChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onistoggledchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleIsToggledChanged));
        }

        private Task HandleIsToggledChanged(ChangeEventArgs evt)
        {
            return IsToggledChanged.InvokeAsync((bool)evt.Value);
        }
    }
}
