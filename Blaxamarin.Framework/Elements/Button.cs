using Emblazon;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Button : Element
    {
        static Button()
        {
            NativeControlRegistry<IFormsControlHandler>
                .RegisterNativeControlComponent<Button>(renderer => new ButtonHandler(renderer));
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

        protected static void ApplyAttribute(ButtonHandler buttonHandler, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (buttonHandler is null)
            {
                throw new ArgumentNullException(nameof(buttonHandler));
            }

            switch (attributeName)
            {
                case nameof(Text):
                    buttonHandler.ButtonControl.Text = (string)attributeValue;
                    break;
                case "onclick":
                    buttonHandler.Renderer.RegisterEvent(attributeEventHandlerId, () => buttonHandler.ClickEventHandlerId = 0);
                    buttonHandler.ClickEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    ApplyAttribute((XF.Element)buttonHandler.ButtonControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        protected class ButtonHandler : IFormsControlHandler
        {
            public ButtonHandler(EmblazonRenderer<IFormsControlHandler> renderer)
            {
                ButtonControl.Clicked += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, new EventArgs()));
                    }
                };
                Renderer = renderer;
            }

            public ulong ClickEventHandlerId { get; set; }
            public EmblazonRenderer<IFormsControlHandler> Renderer { get; }
            public XF.Button ButtonControl { get; } = new XF.Button();
            public object NativeControl => ButtonControl;
            public XF.Element ElementControl => ButtonControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                Button.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
