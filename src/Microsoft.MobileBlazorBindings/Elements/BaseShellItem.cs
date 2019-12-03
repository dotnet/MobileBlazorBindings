using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Blazor.Native.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class BaseShellItem : NavigableElement
    {
        static BaseShellItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<BaseShellItem>(
                renderer => new BaseShellItemHandler(renderer, new XF.BaseShellItem()));
        }

        [Parameter] public XF.ImageSource FlyoutIcon { get; set; }
        [Parameter] public XF.ImageSource Icon { get; set; }
        [Parameter] public bool? IsEnabled { get; set; }
        [Parameter] public bool? IsTabStop { get; set; }
        [Parameter] public string Route { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public int? TabIndex { get; set; }

        [Parameter] public EventCallback OnAppearing { get; set; }
        [Parameter] public EventCallback OnDisappearing { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutIcon != null)
            {
                builder.AddAttribute(nameof(FlyoutIcon), AttributeHelper.ImageSourceToString(FlyoutIcon));
            }
            if (Icon != null)
            {
                builder.AddAttribute(nameof(Icon), AttributeHelper.ImageSourceToString(Icon));
            }
            if (IsEnabled != null)
            {
                builder.AddAttribute(nameof(IsEnabled), IsEnabled.Value);
            }
            if (IsTabStop != null)
            {
                builder.AddAttribute(nameof(IsTabStop), IsTabStop.Value);
            }
            if (Route != null)
            {
                builder.AddAttribute(nameof(Route), Route);
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }
            if (TabIndex != null)
            {
                builder.AddAttribute(nameof(TabIndex), TabIndex.Value);
            }

            builder.AddAttribute("onappearing", OnAppearing);
            builder.AddAttribute("ondisappearing", OnDisappearing);
        }
    }
}
