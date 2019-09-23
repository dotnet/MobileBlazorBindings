using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    internal class MasterDetailMasterPage : ContentPage
    {
        static MasterDetailMasterPage()
        {
            NativeControlRegistry<IFormsControlHandler>
                .RegisterNativeControlComponent<MasterDetailMasterPage>(renderer => new MasterPageHandler(renderer, new XF.ContentPage()));
        }
    }
}
