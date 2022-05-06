// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.SkiaSharp;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System;

namespace ControlGallery.Views
{
    public partial class SkiaCanvasDemo
    {
        public SKCanvasView CanvasView { get; set; }
        public SKCanvasView CanvasView2 { get; set; }

        private float rotation;
        public void RotationSliderChanged(double value)
        {
            rotation = (float)(value * 360);
            CanvasView?.InvalidateSurface();
            CanvasView2?.InvalidateSurface();
        }

        private void PaintSurface2(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Green);

            var canvasSize = e.Info.Rect;

            var paint = new SKPaint
            {
                Color = SKColors.SkyBlue,
                StrokeCap = SKStrokeCap.Round
            };

            //Path based on
            //https://docs.microsoft.com/en-us/xamarin/graphics-games/skiasharp/introduction
            var path = new SKPath();
            path.MoveTo(71.4311121f, 56f);
            path.CubicTo(68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
            path.LineTo(43.0238921f, 97.5342563f);
            path.CubicTo(41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
            path.LineTo(64.5928855f, 143.034271f);
            path.CubicTo(65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
            path.LineTo(114.568946f, 147f);
            path.CubicTo(117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
            path.LineTo(142.976161f, 105.465744f);
            path.CubicTo(144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
            path.LineTo(121.407172f, 59.965729f);
            path.CubicTo(120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
            path.Close();

            var pathSize = path.Bounds;
            canvas.Translate(canvasSize.MidX, canvasSize.MidY);
            var shortSideRatio = Math.Min(canvasSize.Width / pathSize.Width, canvasSize.Height / pathSize.Width);
            canvas.Scale(0.9f * shortSideRatio);
            canvas.RotateDegrees(rotation);
            canvas.Translate(-pathSize.MidX, -pathSize.MidY);

            canvas.DrawPath(path, paint);
            path = SKPath.ParseSvgPathData("M71.4 78.75 L76.4 78.75 L92.99 101.5 L76.4 124.24 L71.4 124.25 L87.99 101.5 Z");
            paint.Color = SKColors.White;
            paint.Style = SKPaintStyle.Fill;
            canvas.DrawPath(path, paint);

            path = SKPath.ParseSvgPathData("M114.57 78.75 L109.54 78.75 L92.99 101.5 L109.57 124.24 L114.57 124.25 L97.99 101.5 Z");
            canvas.DrawPath(path, paint);
        }

        private void PaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Crimson);
            var canvasSize = e.Info.Rect;

            var path = SKPath.ParseSvgPathData("M 926 878 c -46 -79 -113 -137 -186 -163 c -24 -8 -45 -14 -47 -12 c -2 2 5 24 16 48 c 10 24 21 59 25 77 l 6 33 l -38 -30 c -73 -59 -124 -74 -277 -82 c -88 -4 -156 -12 -183 -22 c -165 -62 -267 -249 -232 -423 c 44 -214 218 -328 453 -296 c 95 14 223 71 272 122 l 30 31 l -45 -25 c -85 -47 -185 -70 -300 -71 c -95 0 -111 3 -166 28 c -114 53 -176 143 -182 264 c -5 97 16 158 79 227 c 66 73 130 101 229 100 c 66 0 83 -4 138 -34 c 105 -56 165 -154 166 -270 c 1 -75 -22 -118 -74 -140 c -35 -15 -98 -1 -120 25 c -11 13 -16 12 -42 -10 c -63 -53 -178 -24 -218 56 c -25 47 -25 82 -1 135 c 28 63 71 84 168 84 c 110 0 116 -5 122 -119 c 6 -94 14 -110 53 -111 c 31 0 48 29 48 83 c -1 177 -194 291 -351 209 c -143 -75 -180 -250 -77 -367 c 59 -67 106 -88 208 -93 c 291 -14 551 199 592 485 c 14 94 2 198 -30 259 l -18 34 l -18 -32 Z");

            var pathSize = path.Bounds;

            canvas.Translate(canvasSize.MidX, canvasSize.MidY);
            var shortSideRatio = Math.Min(canvasSize.Width / pathSize.Width, canvasSize.Height / pathSize.Width);
            canvas.Scale(0.7f * shortSideRatio);
            canvas.RotateDegrees(rotation);
            canvas.Translate(-pathSize.MidX, -pathSize.MidY);

            canvas.Scale(-1);

            var paint = new SKPaint
            {
                Color = SKColors.Purple,
                Style = SKPaintStyle.Fill,
            };

            var matrix = SKMatrix.Identity;
            matrix.ScaleX = -1;
            matrix.TransY = -canvasSize.Height * 1.5f;
            path.Transform(matrix);
            canvas.DrawPath(path, paint);

            path = SKPath.ParseSvgPathData("M 317 459 c -11 -6 -25 -26 -33 -45 c -38 -91 78 -165 146 -92 c 16 17 20 35 20 85 l 0 63 l -57 0 c -32 0 -66 -5 -76 -11 Z");
            path.Transform(matrix);

            canvas.DrawPath(path, paint);
        }
    }
}