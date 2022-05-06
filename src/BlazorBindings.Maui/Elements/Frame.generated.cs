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
    public partial class Frame : ContentView
    {
        static Frame()
        {
            ElementHandlerRegistry.RegisterElementHandler<Frame>(
                renderer => new FrameHandler(renderer, new MC.Frame()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BorderColor { get; set; }
        [Parameter] public float? CornerRadius { get; set; }
        [Parameter] public bool? HasShadow { get; set; }

        public new MC.Frame NativeControl => ((FrameHandler)ElementHandler).FrameControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.SingleToString(CornerRadius.Value));
            }
            if (HasShadow != null)
            {
                builder.AddAttribute(nameof(HasShadow), HasShadow.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
