using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using SK = SkiaSharp.Views.Forms;
using SkiaSharp.Views.MobileBlazorBindings.Elements.Handlers;

namespace SkiaSharp.Views.MobileBlazorBindings.Elements
{
    public class SKCanvasView : View
    {
        static SKCanvasView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<SKCanvasView>(renderer => new SKCanvasViewHandler(renderer, new SK.SKCanvasView()));
        }
    }
}