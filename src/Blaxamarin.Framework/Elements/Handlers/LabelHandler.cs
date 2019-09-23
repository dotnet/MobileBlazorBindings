using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class LabelHandler : ViewHandler
    {
        public LabelHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.Label labelControl) : base(renderer, labelControl)
        {
            LabelControl = labelControl ?? throw new System.ArgumentNullException(nameof(labelControl));
        }

        public XF.Label LabelControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Label.Text):
                    LabelControl.Text = (string)attributeValue;
                    break;
                case nameof(XF.Label.TextColor):
                    LabelControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Label.FontSize):
                    LabelControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Label.HorizontalTextAlignment):
                    LabelControl.HorizontalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.VerticalTextAlignment):
                    LabelControl.VerticalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.FontAttributes):
                    LabelControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Label.TextDecorations):
                    LabelControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
