using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Label : View
    {
        static Label()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<Label>(renderer => new LabelHandler(renderer, new XF.Label()));
        }

        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }

        [Parameter] public RenderFragment FormattedText { get; set; }

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

        protected override RenderFragment GetChildContent() => FormattedText;
    }
}
