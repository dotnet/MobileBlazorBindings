using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class MasterDetailMasterPage : ContentPage
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        static MasterDetailMasterPage()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<MasterDetailMasterPage>(renderer => new MasterPageHandler(renderer, new XF.ContentPage()));
        }
    }
}
