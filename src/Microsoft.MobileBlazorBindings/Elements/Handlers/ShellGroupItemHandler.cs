using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ShellGroupItemHandler : BaseShellItemHandler
    {
        public ShellGroupItemHandler(EmblazonRenderer renderer, XF.ShellGroupItem shellGroupItemControl) : base(renderer, shellGroupItemControl)
        {
            ShellGroupItemControl = shellGroupItemControl ?? throw new ArgumentNullException(nameof(shellGroupItemControl));
        }

        public XF.ShellGroupItem ShellGroupItemControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.ShellGroupItem.FlyoutDisplayOptions):
                    ShellGroupItemControl.FlyoutDisplayOptions = (XF.FlyoutDisplayOptions)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
