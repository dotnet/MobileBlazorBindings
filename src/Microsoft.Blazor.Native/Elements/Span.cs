using Microsoft.Blazor.Native.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
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
        [Parameter] public XF.Color? ForegroundColor { get; set; }
        [Parameter] public string Text { get; set; }
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
            if (ForegroundColor != null)
            {
                builder.AddAttribute(nameof(ForegroundColor), AttributeHelper.ColorToString(ForegroundColor.Value));
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (TextDecorations != null)
            {
                builder.AddAttribute(nameof(TextDecorations), (int)TextDecorations.Value);
            }
        }
    }
}
