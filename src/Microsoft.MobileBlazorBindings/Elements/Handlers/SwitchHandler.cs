using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class SwitchHandler : ViewHandler
    {
        public SwitchHandler(EmblazonRenderer renderer, XF.Switch switchControl) : base(renderer, switchControl)
        {
            SwitchControl = switchControl ?? throw new System.ArgumentNullException(nameof(switchControl));
            SwitchControl.Toggled += (s, e) =>
            {
                if (IsToggledChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsToggledChangedEventHandlerId, null, new ChangeEventArgs { Value = SwitchControl.IsToggled }));
                }
            };
        }

        public ulong IsToggledChangedEventHandlerId { get; set; }
        public XF.Switch SwitchControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Switch.IsToggled):
                    SwitchControl.IsToggled = AttributeHelper.GetBool(attributeValue);
                    break;
                case "onistoggledchanged":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => IsToggledChangedEventHandlerId = 0);
                    IsToggledChangedEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
