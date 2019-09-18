using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Entry : Element
    {
        static Entry()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<Entry>(
                renderer => new EntryHandler(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public string Placeholder { get; set; }

        [Parameter] public EventCallback<string> TextChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Placeholder != null)
            {
                builder.AddAttribute(nameof(Placeholder), Placeholder);
            }

            builder.AddAttribute("ontextchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleTextChanged));
        }

        private Task HandleTextChanged(ChangeEventArgs evt)
        {
            return TextChanged.InvokeAsync((string)evt.Value);
        }

        private class EntryHandler : IFormsControlHandler
        {
            public EntryHandler(EmblazonRenderer<IFormsControlHandler> renderer)
            {
                EntryControl.TextChanged += (s, e) =>
                {
                    if (TextChangedEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = Text }));
                    }
                };
                Renderer = renderer;
            }

            public ulong TextChangedEventHandlerId { get; set; }
            public EmblazonRenderer<IFormsControlHandler> Renderer { get; }
            public XF.Entry EntryControl { get; set; } = new XF.Entry();
            public object NativeControl => EntryControl;
            public XF.Element ElementControl => EntryControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        EntryControl.Text = (string)attributeValue;
                        break;
                    case nameof(Placeholder):
                        EntryControl.Placeholder = (string)attributeValue;
                        break;
                    case "ontextchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, () => TextChangedEventHandlerId = 0);
                        TextChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        Element.ApplyAttribute(EntryControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
