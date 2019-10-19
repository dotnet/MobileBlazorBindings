using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class SpanHandler : GestureElementHandler
    {
        public SpanHandler(EmblazonRenderer renderer, XF.Span spanControl) : base(renderer, spanControl)
        {
            SpanControl = spanControl ?? throw new ArgumentNullException(nameof(spanControl));
        }

        public XF.Span SpanControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(Span.BackgroundColor):
                    SpanControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(Span.FontAttributes):
                    SpanControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(Span.FontSize):
                    SpanControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(Span.ForegroundColor):
                    SpanControl.ForegroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(Span.Text):
                    SpanControl.Text = (string)attributeValue;
                    break;
                case nameof(Span.TextDecorations):
                    SpanControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
