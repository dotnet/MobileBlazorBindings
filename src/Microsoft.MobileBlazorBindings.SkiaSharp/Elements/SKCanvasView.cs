// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using Microsoft.MobileBlazorBindings.SkiaSharp.Elements.Handlers;
using SK = SkiaSharp.Views.Forms;

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
