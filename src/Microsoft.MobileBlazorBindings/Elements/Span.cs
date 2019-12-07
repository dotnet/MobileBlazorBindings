using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Span : GestureElement
    {
        static Span()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<Span>(renderer => new SpanHandler(renderer, new XF.Span()));
        }

        [Parameter] public XF.Color? BackgroundColor { get; set; }
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }
        [Parameter] public XF.TextDecorations? TextDecorations { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundColor != null)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor.Value));
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), AttributeHelper.DoubleToString(FontSize.Value));
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (TextColor != null)
            {
                builder.AddAttribute(nameof(TextColor), AttributeHelper.ColorToString(TextColor.Value));
            }
            if (TextDecorations != null)
            {
                builder.AddAttribute(nameof(TextDecorations), (int)TextDecorations.Value);
            }
        }
    }
}
