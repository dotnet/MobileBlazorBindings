using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using SK = SkiaSharp.Views.Forms;
using XF = Xamarin.Forms;

namespace SkiaSharp.Views.MobileBlazorBindings.Elements.Handlers
{
    public class SKCanvasViewHandler : ViewHandler
    {
        public SKCanvasViewHandler(NativeComponentRenderer renderer, SK.SKCanvasView sKCanvasViewControl) : base(renderer, sKCanvasViewControl)
        {

        }
        public SK.SKCanvasView SKCanvasViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}