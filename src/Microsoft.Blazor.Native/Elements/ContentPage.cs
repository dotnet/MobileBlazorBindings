using Microsoft.Blazor.Native.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
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
