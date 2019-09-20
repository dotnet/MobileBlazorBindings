using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class ViewHandler : VisualElementHandler
    {
        public ViewHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.View viewControl) : base(renderer, viewControl)
        {
            ViewControl = viewControl ?? throw new ArgumentNullException(nameof(viewControl));
        }

        public XF.View ViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.View.HorizontalOptions):
                    // TODO: Create helper for this
                    var horizontalOptionsStringParts = ((string)attributeValue).Split(',');
                    ViewControl.HorizontalOptions =
                        new XF.LayoutOptions(
                            (XF.LayoutAlignment)AttributeHelper.GetInt(horizontalOptionsStringParts[0]),
                            bool.Parse(horizontalOptionsStringParts[1]));
                    break;
                case nameof(XF.View.Margin):
                    // TODO: Create helper for this
                    var marginStringParts = ((string)attributeValue).Split(',');
                    ViewControl.Margin = new XF.Thickness(
                        left: AttributeHelper.StringToDouble(marginStringParts[0]),
                        top: AttributeHelper.StringToDouble(marginStringParts[1]),
                        right: AttributeHelper.StringToDouble(marginStringParts[2]),
                        bottom: AttributeHelper.StringToDouble(marginStringParts[3]));
                    break;
                case nameof(XF.View.VerticalOptions):
                    // TODO: Create helper for this
                    var verticalOptionsStringParts = ((string)attributeValue).Split(',');
                    ViewControl.VerticalOptions =
                        new XF.LayoutOptions(
                            (XF.LayoutAlignment)AttributeHelper.GetInt(verticalOptionsStringParts[0]),
                            bool.Parse(verticalOptionsStringParts[1]));
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
