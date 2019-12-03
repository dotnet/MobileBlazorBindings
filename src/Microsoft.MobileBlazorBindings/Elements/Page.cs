using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Blazor.Native.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class Page : VisualElement
    {
        static Page()
        {
            ElementHandlerRegistry.RegisterElementHandler<Page>(
                renderer => new PageHandler(renderer, new XF.Page()));
        }

        [Parameter] public XF.ImageSource IconImageSource { get; set; }
        [Parameter] public string Title { get; set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IconImageSource != null)
            {
                builder.AddAttribute(nameof(IconImageSource), AttributeHelper.ImageSourceToString(IconImageSource));
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
