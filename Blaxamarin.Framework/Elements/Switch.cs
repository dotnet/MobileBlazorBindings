using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Switch : Element
    {
        static Switch()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<Switch>(
                renderer => new SwitchHandler(renderer));
        }

        [Parameter] public bool? IsToggled { get; set; }

        [Parameter] public EventCallback<bool> IsToggledChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsToggled != null)
            {
                builder.AddAttribute(nameof(IsToggled), IsToggled.Value);
            }

            builder.AddAttribute("onistoggledchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleIsToggledChanged));
        }

        private Task HandleIsToggledChanged(ChangeEventArgs evt)
        {
            return IsToggledChanged.InvokeAsync((bool)evt.Value);
        }

        private class SwitchHandler : IFormsControlHandler
        {
            public SwitchHandler(EmblazonRenderer<IFormsControlHandler> renderer)
            {
                SwitchControl.Toggled += (s, e) =>
                {
                    if (IsToggledChangedEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsToggledChangedEventHandlerId, null, new ChangeEventArgs { Value = IsToggled }));
                    }
                };
                Renderer = renderer;
            }

            public ulong IsToggledChangedEventHandlerId { get; set; }
            public EmblazonRenderer<IFormsControlHandler> Renderer { get; }
            public XF.Switch SwitchControl { get; set; } = new XF.Switch();
            public object NativeControl => SwitchControl;
            public XF.Element ElementControl => SwitchControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(IsToggled):
                        SwitchControl.IsToggled = AttributeHelper.GetBool(attributeValue);
                        break;
                    case "onistoggledchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, () => IsToggledChangedEventHandlerId = 0);
                        IsToggledChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        Element.ApplyAttribute(SwitchControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
