using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ContentPage : TemplatedPage
    {
        static ContentPage()
        {
            NativeControlRegistry<IFormsControlHandler>
               .RegisterNativeControlComponent<ContentPage>(renderer => new ContentPageHandler(renderer, new XF.ContentPage()));
        }
    }
}
