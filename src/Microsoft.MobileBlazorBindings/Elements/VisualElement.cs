// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class VisualElement : NavigableElement
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<VisualElement>(nameof(Background),
                _ => new ContentPropertyHandler<XF.VisualElement>((visualElement, contentElement) => visualElement.Background = (XF.Brush)contentElement));
        }

        [Parameter] public RenderFragment Background { get; set; }
        [Parameter] public EventCallback<XF.FocusEventArgs> OnFocused { get; set; }
        [Parameter] public EventCallback OnSizeChanged { get; set; }
        [Parameter] public EventCallback<XF.FocusEventArgs> OnUnfocused { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onfocused", OnFocused);
            builder.AddAttribute("onsizechanged", OnSizeChanged);
            builder.AddAttribute("onunfocused", OnUnfocused);
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(VisualElement), nameof(Background), Background);
        }
    }
}
