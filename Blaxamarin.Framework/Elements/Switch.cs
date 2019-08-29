using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Switch : FormsComponentBase
    {
        static Switch()
        {
            NativeControlRegistry<Element>.RegisterNativeControlComponent<Switch>(
                renderer => new BlazorSwitch(renderer));
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

            builder.AddAttribute("onistoggledchanged", EventCallback.Factory.Create<UIChangeEventArgs>(this, HandleIsToggledChanged));
        }

        private Task HandleIsToggledChanged(UIChangeEventArgs evt)
        {
            return IsToggledChanged.InvokeAsync((bool)evt.Value);
        }

        class BlazorSwitch : Xamarin.Forms.Switch, IBlazorNativeControl
        {
            public BlazorSwitch(EmblazonRenderer<Element> renderer)
            {
                Toggled += (s, e) =>
                {
                    if (IsToggledChangedEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsToggledChangedEventHandlerId, null, new UIChangeEventArgs { Value = IsToggled }));
                    }
                };
                Renderer = renderer;
            }

            public ulong IsToggledChangedEventHandlerId { get; set; }
            public EmblazonRenderer<Element> Renderer { get; }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(IsToggled):
                        IsToggled = AttributeHelper.GetBool(attributeValue);
                        break;
                    case "onistoggledchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, () => IsToggledChangedEventHandlerId = 0);
                        IsToggledChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
