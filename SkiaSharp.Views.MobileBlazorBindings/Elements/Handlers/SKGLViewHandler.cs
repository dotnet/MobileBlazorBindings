using System;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Xamarin.Forms;
using SK = SkiaSharp.Views.Forms;

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
                    //This works well on iOS but has issues on Android
                    renderer.DispatchEventAsync(PaintEventHandlerId, null, e);

                    //Putting it inside an InvokeAsync Prevents the error for wrong thread but causes other weird problems on android
                    //This intermittently causes an abrt with no human readable crash on both iOS and Android
                    //Adding break points makes everything go strange and crash
                    //renderer.Dispatcher.InvokeAsync(() =>
                    //    renderer.DispatchEventAsync(PaintEventHandlerId, null, e)
                    //);

                }
            };
        }

        public ulong PaintEventHandlerId { get; set; }
    }
}