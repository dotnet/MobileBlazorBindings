using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class TabbedPage : Page
    {
        static TabbedPage()
        {
            ElementHandlerRegistry<IXamarinFormsElementHandler>
                .RegisterElementHandler<TabbedPage>(renderer => new TabbedPageHandler(renderer, new XF.TabbedPage()));
        }
    }
}
