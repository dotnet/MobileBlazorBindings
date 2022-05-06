// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System;
using SK = SkiaSharp.Views.Maui.Controls;

namespace BlazorBindings.Maui.SkiaSharp.Elements.Handlers
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

        private void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onpaintsurface",
                setId: id => PaintEventHandlerId = id,
                clearId: id => { if (PaintEventHandlerId == id) { PaintEventHandlerId = 0; } });
            SKCanvasViewControl.PaintSurface += (s, e) =>
            {
                if (PaintEventHandlerId != default)
                {
#pragma warning disable BL0006 // Do not use RenderTree types
                    renderer.DispatchEventAsync(PaintEventHandlerId, null, e);
#pragma warning restore BL0006 // Do not use RenderTree types
                }
            };
        }

        public ulong PaintEventHandlerId { get; set; }

    }
}
