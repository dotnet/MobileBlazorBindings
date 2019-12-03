using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Frame : ContentView
    {
        static Frame()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<Frame>(renderer => new FrameHandler(renderer, new XF.Frame()));
        }

        [Parameter] public bool? HasShadow { get; set; }
        [Parameter] public XF.Color? BorderColor { get; set; }
        [Parameter] public float? CornerRadius { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HasShadow != null)
            {
                builder.AddAttribute(nameof(HasShadow), HasShadow.Value);
            }
            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.SingleToString(CornerRadius.Value));
            }
        }
    }
}
