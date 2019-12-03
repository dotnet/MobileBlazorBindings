using Emblazon;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class EntryHandler : InputViewHandler
    {
        public EntryHandler(EmblazonRenderer renderer, XF.Entry entryControl) : base(renderer, entryControl)
        {
            EntryControl = entryControl ?? throw new ArgumentNullException(nameof(entryControl));
            EntryControl.Completed += (s, e) =>
            {
                if (CompletedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(CompletedEventHandlerId, null, e));
                }
            };
            EntryControl.TextChanged += (s, e) =>
            {
                if (TextChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = EntryControl.Text }));
                }
            };
        }

        public ulong CompletedEventHandlerId { get; set; }
        public ulong TextChangedEventHandlerId { get; set; }
        public XF.Entry EntryControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Entry.Text):
                    EntryControl.Text = (string)attributeValue;
                    break;
                case nameof(XF.Entry.Placeholder):
                    EntryControl.Placeholder = (string)attributeValue;
                    break;
                case "oncompleted":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => CompletedEventHandlerId = 0);
                    CompletedEventHandlerId = attributeEventHandlerId;
                    break;
                case "ontextchanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => TextChangedEventHandlerId = 0);
                    TextChangedEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
