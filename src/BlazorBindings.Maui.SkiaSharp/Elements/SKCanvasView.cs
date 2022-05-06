// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.SkiaSharp.Elements.Handlers;
using SkiaSharp.Views.Maui;
using SK = SkiaSharp.Views.Maui.Controls;

namespace BlazorBindings.Maui.SkiaSharp
{
    public class SKCanvasView : View
    {
        static SKCanvasView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<SKCanvasView>(renderer => new SKCanvasViewHandler(renderer, new SK.SKCanvasView()));
        }

        [Parameter] public EventCallback<SKPaintSurfaceEventArgs> OnPaintSurface { get; set; }

        public new SK.SKCanvasView NativeControl => ((SKCanvasViewHandler)ElementHandler).SKCanvasViewControl;
        public void InvalidateSurface()
        {
            NativeControl.InvalidateSurface();
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);
            builder.AddAttribute("onpaintsurface", OnPaintSurface);
        }
    }
}
