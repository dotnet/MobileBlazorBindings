using Emblazon;
using Microsoft.Blazor.Native.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class FlyoutItem : ShellItem
    {
        static FlyoutItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<FlyoutItem>(
                renderer => new FlyoutItemHandler(renderer, new XF.FlyoutItem()));
        }
    }
}
