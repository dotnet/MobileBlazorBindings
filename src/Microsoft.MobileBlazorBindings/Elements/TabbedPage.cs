using Emblazon;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TabbedPage : Page
    {
        static TabbedPage()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<TabbedPage>(renderer => new TabbedPageHandler(renderer, new XF.TabbedPage()));
        }
    }
}
