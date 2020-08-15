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
            ConfigureEvent(
                eventName: "onpaintsurface",
                setId: id => PaintEventHandlerId = id,
                clearId: id => { if (PaintEventHandlerId == id) { PaintEventHandlerId = 0; } });
            SKGLViewControl.PaintSurface += (s, e) =>
            {
                if (PaintEventHandlerId != default)
                {
                    //This works well on iOS but has issues on Android
                    //Sometimes it throws "System.InvalidOperationException: 'The current thread is not associated with the Dispatcher. Use InvokeAsync() to switch execution to the Dispatcher when triggering rendering or component state.'"
                    //renderer.DispatchEventAsync(PaintEventHandlerId, null, e);

                    //Putting it inside an InvokeAsync prevents "The current thread is not associated with the Dispatcher" but causes other weird problems on android
                    //This intermittently causes an abrt with no human readable crash on iOS
                    //Adding break points makes everything go strange and crash
                    //To see these issues run the Control Gallery, click Skia Playground, click Skia GL Paths MBB Events
                    //To see the GLCanvas working correctly by bypassing dispatcher run the Control Gallery, click Skia Playground, click Skia GL Native Control Events
                    renderer.Dispatcher.InvokeAsync(() =>
                        renderer.DispatchEventAsync(PaintEventHandlerId, null, e)
                    );
                }
            };
        }

        public ulong PaintEventHandlerId { get; set; }
    }
}