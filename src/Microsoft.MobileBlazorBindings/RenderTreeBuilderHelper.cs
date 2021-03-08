// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Elements;
using System;

namespace Microsoft.MobileBlazorBindings
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

                // Content properties are handled by separate handlers, there rendered as separate child elements.
                // Renderer does not support elements without parent components as for now,
                // therefore adding empty parent component to workaround that.
                builder.OpenComponent<PropertyWrapperComponent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(2, $"p-{containingType.FullName}.{propertyName}");
                    builder.AddContent(3, content);
                    builder.CloseElement();
                }));
                builder.CloseComponent();

                builder.CloseRegion();
            }
        }
    }
}
