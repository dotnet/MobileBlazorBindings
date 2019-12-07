using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class StackLayoutHandler : LayoutHandler
    {
        public StackLayoutHandler(NativeComponentRenderer renderer, XF.StackLayout stackLayoutControl) : base(renderer, stackLayoutControl)
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
                case nameof(XF.StackLayout.Spacing):
                    StackLayoutControl.Spacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
