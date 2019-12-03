using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class LayoutHandler : ViewHandler
    {
        public LayoutHandler(EmblazonRenderer renderer, XF.Layout layoutControl) : base(renderer, layoutControl)
        {
            LayoutControl = layoutControl ?? throw new System.ArgumentNullException(nameof(layoutControl));
        }

        public XF.Layout LayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Layout.Padding):
                    LayoutControl.Padding = AttributeHelper.StringToThickness(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
