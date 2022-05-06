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
    public partial class LinearGradientBrush : GradientBrush
    {
        static LinearGradientBrush()
        {
            ElementHandlerRegistry.RegisterElementHandler<LinearGradientBrush>(
                renderer => new LinearGradientBrushHandler(renderer, new MC.LinearGradientBrush()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Point? EndPoint { get; set; }
        [Parameter] public Point? StartPoint { get; set; }

        public new MC.LinearGradientBrush NativeControl => ((LinearGradientBrushHandler)ElementHandler).LinearGradientBrushControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (EndPoint != null)
            {
                builder.AddAttribute(nameof(EndPoint), AttributeHelper.PointToString(EndPoint.Value));
            }
            if (StartPoint != null)
            {
                builder.AddAttribute(nameof(StartPoint), AttributeHelper.PointToString(StartPoint.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
