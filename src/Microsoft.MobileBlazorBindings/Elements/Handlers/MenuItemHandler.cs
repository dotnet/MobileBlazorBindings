using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class MenuItemHandler : BaseMenuItemHandler
    {
        public MenuItemHandler(EmblazonRenderer renderer, XF.MenuItem menuItemControl) : base(renderer, menuItemControl)
        {
            MenuItemControl = menuItemControl ?? throw new ArgumentNullException(nameof(menuItemControl));
            MenuItemControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };
        }

        public XF.MenuItem MenuItemControl { get; }
        public ulong ClickEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.MenuItem.IconImageSource):
                    MenuItemControl.IconImageSource = AttributeHelper.StringToImageSource((string)attributeValue);
                    break;
                case nameof(XF.MenuItem.IsDestructive):
                    MenuItemControl.IsDestructive = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.MenuItem.Text):
                    MenuItemControl.Text = (string)attributeValue;
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
