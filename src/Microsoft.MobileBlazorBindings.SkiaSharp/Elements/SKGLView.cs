using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using SK = SkiaSharp.Views.Forms;
using Microsoft.MobileBlazorBindings.SkiaSharp.Elements.Handlers;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.SkiaSharp
{
    public class SKGLView : View
    {
        static SKGLView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<SKGLView>(renderer => new SKGLViewHandler(renderer, new SK.SKGLView()));
        }

        [Parameter] public EventCallback<SK.SKPaintGLSurfaceEventArgs> OnPaintSurface { get; set; }
        public new SK.SKGLView NativeControl => ((SKGLViewHandler)ElementHandler).SKGLViewControl;

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
