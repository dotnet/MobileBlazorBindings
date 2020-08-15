using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using SK = SkiaSharp.Views.Forms;
using Microsoft.MobileBlazorBindings.SkiaSharp.Elements.Handlers;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.SkiaSharp
{
    public class SKCanvasView : View
    {
        static SKCanvasView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<SKCanvasView>(renderer => new SKCanvasViewHandler(renderer, new SK.SKCanvasView()));
        }

        [Parameter] public EventCallback<SK.SKPaintSurfaceEventArgs> OnPaintSurface { get; set; }

        public void InvalidateSurface()
        {
            (NativeControl as SK.SKCanvasView).InvalidateSurface();
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);
            builder.AddAttribute("onpaintsurface", OnPaintSurface);
        }
    }
}