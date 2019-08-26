using Emblazon;
using Microsoft.AspNetCore.Components;

namespace BlinForms.Framework.Controls
{
    public class Button : FormsComponentBase
    {
        static Button()
        {
            NativeControlRegistry<System.Windows.Forms.Control>.RegisterNativeControlComponent<Button>(
                renderer => new BlazorButton(renderer));
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

        class BlazorButton : System.Windows.Forms.Button, IBlazorNativeControl
        {
            public BlazorButton(EmblazonRenderer<System.Windows.Forms.Control> renderer)
            {
                Click += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(ClickEventHandlerId, null, new UIEventArgs());
                    }
                };
            }

            public ulong ClickEventHandlerId { get; set; }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case "onclick":
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
