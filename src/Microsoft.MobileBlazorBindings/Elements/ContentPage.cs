using Emblazon;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
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
