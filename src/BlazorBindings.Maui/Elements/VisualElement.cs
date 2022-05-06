// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class VisualElement : NavigableElement
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<VisualElement>(nameof(Background),
                _ => new ContentPropertyHandler<MC.VisualElement>((visualElement, contentElement) => visualElement.Background = (MC.Brush)contentElement));
        }

        [Parameter] public RenderFragment Background { get; set; }
        [Parameter] public EventCallback<MC.FocusEventArgs> OnFocused { get; set; }
        [Parameter] public EventCallback OnSizeChanged { get; set; }
        [Parameter] public EventCallback<MC.FocusEventArgs> OnUnfocused { get; set; }

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
