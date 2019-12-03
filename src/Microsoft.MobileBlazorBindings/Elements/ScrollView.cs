using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ScrollView : Layout
    {
        static ScrollView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ScrollView>(
                renderer => new ScrollViewHandler(renderer, new XF.ScrollView()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
        [Parameter] public XF.ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public XF.ScrollOrientation? Orientation { get; set; }
        [Parameter] public XF.ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(HorizontalScrollBarVisibility), (int)HorizontalScrollBarVisibility.Value);
            }
            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (VerticalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(VerticalScrollBarVisibility), (int)VerticalScrollBarVisibility.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
