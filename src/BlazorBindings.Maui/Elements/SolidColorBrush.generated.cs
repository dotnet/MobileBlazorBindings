// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class SolidColorBrush : Brush
    {
        static SolidColorBrush()
        {
            ElementHandlerRegistry.RegisterElementHandler<SolidColorBrush>(
                renderer => new SolidColorBrushHandler(renderer, new MC.SolidColorBrush()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }

        public new MC.SolidColorBrush NativeControl => ((SolidColorBrushHandler)ElementHandler).SolidColorBrushControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
