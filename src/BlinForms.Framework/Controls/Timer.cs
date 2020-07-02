// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlinForms.Framework.Controls
{
    //public class Timer : FormsComponentBase
    //{
    //    static Timer()
    //    {
    //        BlontrolAdapter.KnownElements.Add(typeof(Timer).FullName, renderer => new BlazorTimer(renderer));
    //    }

    //    [Parameter] public EventCallback OnTick { get; set; }

    //    protected override void RenderAttributes(RenderTreeBuilder builder)
    //    {
    //        builder.AddAttribute(1, "ontick", OnTick);
    //    }

    //    class BlazorTimer : System.Windows.Forms.Timer, INativeControl
    //    {
    //        public ulong TickEventHandlerId { get; set; }

    //        public BlazorTimer(BlinFormsRenderer renderer)
    //        {
    //            Interval = 1000;
    //            Tick += (s, e) =>
    //            {
    //                if (TickEventHandlerId != default)
    //                {
    //                    renderer.DispatchEventAsync(TickEventHandlerId, null, new EventArgs());
    //                }
    //            };
    //            Enabled = true;
    //        }

    //        public void ApplyAttribute(ref RenderTreeFrame attribute)
    //        {
    //            switch (attribute.AttributeName)
    //            {
    //                case "ontick":
    //                    TickEventHandlerId = attribute.AttributeEventHandlerId;
    //                    break;
    //                default:
    //                    //FormsComponentBase.ApplyAttribute(this, ref attribute);
    //                    break;
    //            }
    //        }
    //    }
    //}
}
