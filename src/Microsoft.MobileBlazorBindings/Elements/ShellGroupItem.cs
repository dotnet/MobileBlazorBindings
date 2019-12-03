using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ShellGroupItem : BaseShellItem
    {
        static ShellGroupItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellGroupItem>(
                renderer => new ShellGroupItemHandler(renderer, new XF.ShellGroupItem()));
        }

        [Parameter] public XF.FlyoutDisplayOptions? FlyoutDisplayOptions { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FlyoutDisplayOptions != null)
            {
                builder.AddAttribute(nameof(FlyoutDisplayOptions), (int)FlyoutDisplayOptions.Value);
            }
        }
    }
}
