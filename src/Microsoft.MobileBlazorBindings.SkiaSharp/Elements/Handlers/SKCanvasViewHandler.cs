using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Xamarin.Forms;
using SK = SkiaSharp.Views.Forms;

namespace Microsoft.MobileBlazorBindings.SkiaSharp.Elements.Handlers
{
    public class SKCanvasViewHandler : ViewHandler
    {
        public SKCanvasViewHandler(NativeComponentRenderer renderer, SK.SKCanvasView sKCanvasViewControl) : base(renderer, sKCanvasViewControl)
        {
            SKCanvasViewControl = sKCanvasViewControl ?? throw new ArgumentNullException(nameof(sKCanvasViewControl));

            Initialize(renderer);
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

        void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onpaintsurface",
                setId: id => PaintEventHandlerId = id,
                clearId: id => { if (PaintEventHandlerId == id) { PaintEventHandlerId = 0; } });
            SKCanvasViewControl.PaintSurface += (s, e) =>
            {
                if (PaintEventHandlerId != default)
                {
                    renderer.DispatchEventAsync(PaintEventHandlerId, null, e);
                }
            };
        }

        public ulong PaintEventHandlerId { get; set; }

    }
}
