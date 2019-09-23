using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    internal class MasterDetailDetailPage : ContentPage
    {
        static MasterDetailDetailPage()
        {
            NativeControlRegistry<IFormsControlHandler>
                .RegisterNativeControlComponent<MasterDetailDetailPage>(renderer => new DetailPageHandler(renderer, new XF.ContentPage()));
        }
    }
}
