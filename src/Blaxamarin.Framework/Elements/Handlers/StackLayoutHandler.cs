using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class StackLayoutHandler : LayoutHandler
    {
        public StackLayoutHandler(EmblazonRenderer renderer, XF.StackLayout stackLayoutControl) : base(renderer, stackLayoutControl)
        {
            StackLayoutControl = stackLayoutControl ?? throw new System.ArgumentNullException(nameof(stackLayoutControl));
        }

        public XF.StackLayout StackLayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.StackLayout.Orientation):
                    StackLayoutControl.Orientation = (XF.StackOrientation)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
