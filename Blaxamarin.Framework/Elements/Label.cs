using Blaxamarin.Framework.Elements.AttributeHelpers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Label : FormsComponentBase
    {
        static Label()
        {
            NativeControlRegistry<Element>.RegisterNativeControlComponent<Label, BlazorLabel>();
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public Color? TextColor { get; set; }
        [Parameter] public double? FontSize { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), ColorAttributeHelper.ColorToString(TextColor.Value));
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), DoubleAttributeHelper.DoubleToString(FontSize.Value));
            }
        }

        class BlazorLabel : Xamarin.Forms.Label, IBlazorNativeControl
        {
            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case nameof(TextColor):
                        TextColor = ColorAttributeHelper.StringToColor((string)attributeValue);
                        break;
                    case nameof(FontSize):
                        FontSize = DoubleAttributeHelper.StringToDouble((string)attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
