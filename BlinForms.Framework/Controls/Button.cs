using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    public class Button : FormsComponentBase
    {
        static Button()
        {
            BlontrolAdapter.KnownElements.Add(typeof(Button).FullName, new ComponentControlFactoryFunc((renderer, _) => new BlazorButton(renderer)));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(1, nameof(Text), Text);
            builder.AddAttribute(2, "onclick", OnClick);
        }

        class BlazorButton : System.Windows.Forms.Button, IBlazorNativeControl
        {
            public BlazorButton(BlinFormsRenderer renderer)
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
