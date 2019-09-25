using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class ButtonHandler : ViewHandler
    {
        public ButtonHandler(EmblazonRenderer<IXamarinFormsElementHandler> renderer, XF.Button buttonControl) : base(renderer, buttonControl)
        {
            ButtonControl = buttonControl ?? throw new ArgumentNullException(nameof(buttonControl));
            ButtonControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };
        }

        public XF.Button ButtonControl { get; }
        public ulong ClickEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Button.Text):
                    ButtonControl.Text = (string)attributeValue;
                    break;
                case "onclick":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => ClickEventHandlerId = 0);
                    ClickEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
