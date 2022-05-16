// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class GradientStop : Element
    {
        static GradientStop()
        {
            ElementHandlerRegistry.RegisterElementHandler<GradientStop>(
                renderer => new GradientStopHandler(renderer, new MC.GradientStop()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public float? Offset { get; set; }

        public new MC.GradientStop NativeControl => (ElementHandler as GradientStopHandler)?.GradientStopControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color));
            }
            if (Offset != null)
            {
                builder.AddAttribute(nameof(Offset), AttributeHelper.SingleToString(Offset.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
