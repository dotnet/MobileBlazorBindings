using Emblazon;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TabBar : ShellItem
    {
        static TabBar()
        {
            ElementHandlerRegistry.RegisterElementHandler<TabBar>(
                renderer => new TabBarHandler(renderer, new XF.TabBar()));
        }
    }
}
