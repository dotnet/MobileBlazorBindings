using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using SK = SkiaSharp.Views.Forms;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.SkiaSharp.Elements.Handlers
{
    public class SKGLViewHandler : ViewHandler
    {
        public SKGLViewHandler(NativeComponentRenderer renderer, SK.SKGLView sKGLViewControl) : base(renderer, sKGLViewControl)
        {
            SKGLViewControl = sKGLViewControl ?? throw new ArgumentNullException(nameof(sKGLViewControl));

            Initialize(renderer);
        }
        public SK.SKGLView SKGLViewControl { get; }

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
            RegisterEvent(
                eventName: "onpaintsurface",
                setId: id => PaintEventHandlerId = id,
                clearId: id => { if (PaintEventHandlerId == id) { PaintEventHandlerId = 0; } });
            SKGLViewControl.PaintSurface += (s, e) =>
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