using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Entry : FormsComponentBase
    {
        static Entry()
        {
            NativeControlRegistry<Element>.RegisterNativeControlComponent<Entry>(
                renderer => new BlazorEntry(renderer));
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

            builder.AddAttribute("ontextchanged", EventCallback.Factory.Create<UIChangeEventArgs>(this, HandleTextChanged));
        }

        private Task HandleTextChanged(UIChangeEventArgs evt)
        {
            return TextChanged.InvokeAsync((string)evt.Value);
        }

        class BlazorEntry : Xamarin.Forms.Entry, IBlazorNativeControl<Xamarin.Forms.Entry>
        {
            public BlazorEntry(EmblazonRenderer<Element> renderer)
            {
                TextChanged += (s, e) =>
                {
                    if (TextChangedEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new UIChangeEventArgs { Value = Text }));
                    }
                };
                Renderer = renderer;
            }

            public ulong TextChangedEventHandlerId { get; set; }
            public EmblazonRenderer<Element> Renderer { get; }
            public Xamarin.Forms.Entry NativeControl => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case nameof(Placeholder):
                        Placeholder = (string)attributeValue;
                        break;
                    case "ontextchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, () => TextChangedEventHandlerId = 0);
                        TextChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
