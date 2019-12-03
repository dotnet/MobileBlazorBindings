using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Button : View
    {
        static Button()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<Button>(renderer => new ButtonHandler(renderer, new XF.Button()));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public XF.Color? TextColor { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }

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

            builder.AddAttribute("onclick", OnClick);
        }
    }
}
