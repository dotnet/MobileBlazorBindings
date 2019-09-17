using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Label : FormsComponentBase
    {
        static Label()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<Label, BlazorLabel>();
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public Color? TextColor { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public TextAlignment? VerticalTextAlignment { get; set; }
        [Parameter] public FontAttributes? FontAttributes { get; set; }
        [Parameter] public TextDecorations? TextDecorations { get; set; }

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

        private class BlazorLabel : Xamarin.Forms.Label, IFormsControlHandler
        {
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case nameof(TextColor):
                        TextColor = AttributeHelper.StringToColor((string)attributeValue);
                        break;
                    case nameof(FontSize):
                        FontSize = AttributeHelper.StringToDouble((string)attributeValue);
                        break;
                    case nameof(HorizontalTextAlignment):
                        HorizontalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(VerticalTextAlignment):
                        VerticalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(FontAttributes):
                        FontAttributes = (FontAttributes)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(TextDecorations):
                        TextDecorations = (TextDecorations)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
