using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Image : View
    {
        static Image()
        {
            ElementHandlerRegistry.RegisterElementHandler<Image>(
                renderer => new ImageHandler(renderer, new XF.Image()));
        }

        [Parameter] public XF.Aspect? Aspect { get; set; }
        [Parameter] public bool? IsOpaque { get; set; }
        [Parameter] public XF.ImageSource Source { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Aspect != null)
            {
                builder.AddAttribute(nameof(Aspect), (int)Aspect.Value);
            }
            if (IsOpaque != null)
            {
                builder.AddAttribute(nameof(IsOpaque), IsOpaque.Value);
            }
            if (Source != null)
            {
                builder.AddAttribute(nameof(Source), AttributeHelper.ImageSourceToString(Source));
            }
        }
    }
}
