// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.Maui.Layouts;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public class AbsoluteLayout : Layout
    {
        static AbsoluteLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<AbsoluteLayout>(
                renderer => new AbsoluteLayoutHandler(renderer, new MC.AbsoluteLayout()));

            RegisterAdditionalHandlers();
        }

        static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("AbsoluteLayout.LayoutBounds",
                (element, value) => MC.AbsoluteLayout.SetLayoutBounds(element, AttributeHelper.StringToBoundsRect(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("AbsoluteLayout.LayoutFlags",
                (element, value) => MC.AbsoluteLayout.SetLayoutFlags(element, AttributeHelper.GetEnum<AbsoluteLayoutFlags>(value)));
        }
    }
}
