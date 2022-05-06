// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class GradientBrush : Brush
    {
        static GradientBrush()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.GradientBrush NativeControl => ((GradientBrushHandler)ElementHandler).GradientBrushControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
