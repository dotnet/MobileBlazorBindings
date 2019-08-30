using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Button : FormsComponentBase
    {
        static Button()
        {
            NativeControlRegistry<IFormsControlHandler>
                .RegisterNativeControlComponent<Button>(renderer => new BlazorButton(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }

            builder.AddAttribute("onclick", OnClick);
        }

        class BlazorButton : Xamarin.Forms.Button, IFormsControlHandler
        {
            public BlazorButton(EmblazonRenderer<IFormsControlHandler> renderer)
            {
                Clicked += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, new UIEventArgs()));
                    }
                };
                Renderer = renderer;
            }

            public ulong ClickEventHandlerId { get; set; }
            public EmblazonRenderer<IFormsControlHandler> Renderer { get; }
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case "onclick":
                        Renderer.RegisterEvent(attributeEventHandlerId, () => ClickEventHandlerId = 0);
                        ClickEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
