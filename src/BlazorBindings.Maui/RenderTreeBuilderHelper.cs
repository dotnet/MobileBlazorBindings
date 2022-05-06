// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.DataTemplates;
using System;

namespace BlazorBindings.Maui
{
    public static class RenderTreeBuilderHelper
    {
        public static void AddContentProperty(RenderTreeBuilder builder, int sequence, Type containingType, string propertyName, RenderFragment content)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (content != null)
            {
                builder.OpenRegion(sequence);

                // Content properties are handled by separate handlers, therefore rendered as separate child elements.
                // Renderer does not support elements without parent components as for now,
                // therefore adding empty parent component to workaround that.
                builder.OpenComponent<PropertyWrapperComponent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(2, GetElementName(containingType, propertyName));
                    builder.AddContent(3, content);
                    builder.CloseElement();
                }));
                builder.CloseComponent();

                builder.CloseRegion();
            }
        }

        public static void AddDataTemplateProperty<T>(RenderTreeBuilder builder, int sequence, Type containingType, string propertyName, RenderFragment<T> template)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (containingType is null)
            {
                throw new ArgumentNullException(nameof(containingType));
            }

            builder.OpenRegion(sequence);

            builder.OpenComponent<DataTemplateItemsComponent<T>>(0);
            builder.AddAttribute(1, nameof(DataTemplateItemsComponent<T>.ElementName), GetElementName(containingType, propertyName));
            builder.AddAttribute(2, nameof(DataTemplateItemsComponent<T>.Template), template);
            builder.CloseComponent();

            builder.CloseRegion();
        }

        private static string GetElementName(Type containingType, string propertyName)
        {
            return $"p-{containingType.FullName}.{propertyName}";
        }
    }
}
