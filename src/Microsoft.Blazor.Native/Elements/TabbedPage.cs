using Microsoft.Blazor.Native.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
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
