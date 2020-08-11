using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ControlGallery.Views
{
    public partial class SkiaSharpPlayground
    {
        private void PaintGLSurface(SkiaSharp.Views.Forms.SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Red);
        }

        private void PaintSurface(SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Blue);
            
        }
    }
}
