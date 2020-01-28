// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class VisualElement : NavigableElement
    {
        [Parameter] public EventCallback<XF.FocusEventArgs> OnFocused { get; set; }
        [Parameter] public EventCallback OnSizeChanged { get; set; }
        [Parameter] public EventCallback<XF.FocusEventArgs> OnUnfocused { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onfocused", OnFocused);
            builder.AddAttribute("onsizechanged", OnSizeChanged);
            builder.AddAttribute("onunfocused", OnUnfocused);
        }
    }
}
