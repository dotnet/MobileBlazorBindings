using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ContentPage : TemplatedPage
    {
        static ContentPage()
        {
            ElementHandlerRegistry
               .RegisterElementHandler<ContentPage>(renderer => new ContentPageHandler(renderer, new XF.ContentPage()));
        }
    }
}
