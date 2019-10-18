using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
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
