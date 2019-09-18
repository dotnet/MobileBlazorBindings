using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Label : Element
    {
        static Label()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<Label, LabelHandler>();
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor.Value));
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), AttributeHelper.DoubleToString(FontSize.Value));
            }
            if (HorizontalTextAlignment != null)
            {
                builder.AddAttribute(nameof(HorizontalTextAlignment), (int)HorizontalTextAlignment.Value);
            }
            if (VerticalTextAlignment != null)
            {
                builder.AddAttribute(nameof(VerticalTextAlignment), (int)VerticalTextAlignment.Value);
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (TextDecorations != null)
            {
                builder.AddAttribute(nameof(TextDecorations), (int)TextDecorations.Value);
            }
        }

        private class LabelHandler : IFormsControlHandler
        {
            public XF.Label LabelControl { get; set; } = new XF.Label();
            public object NativeControl => LabelControl;
            public XF.Element ElementControl => LabelControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        LabelControl.Text = (string)attributeValue;
                        break;
                    case nameof(TextColor):
                        LabelControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                        break;
                    case nameof(FontSize):
                        LabelControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue);
                        break;
                    case nameof(HorizontalTextAlignment):
                        LabelControl.HorizontalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(VerticalTextAlignment):
                        LabelControl.VerticalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(FontAttributes):
                        LabelControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(TextDecorations):
                        LabelControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        Element.ApplyAttribute(LabelControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
