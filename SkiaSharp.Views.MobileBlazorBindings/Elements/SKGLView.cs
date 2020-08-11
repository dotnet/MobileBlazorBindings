using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using SK = SkiaSharp.Views.Forms;
using Microsoft.MobileBlazorBindings.SkiaSharp.Elements.Handlers;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.SkiaSharp.Elements
{
    public class SKGLView : View
    {
        static SKGLView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<SKGLView>(renderer => new SKGLViewHandler(renderer, new SK.SKGLView()));
        }

        [Parameter] public EventCallback<SK.SKPaintGLSurfaceEventArgs> OnPaintSurface { get; set; }

        public void InvalidateSurface()
        {
            (NativeControl as SK.SKGLView).InvalidateSurface();
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);
            builder.AddAttribute("onpaintsurface", OnPaintSurface);
        }
    }
}